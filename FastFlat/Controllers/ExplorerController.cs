﻿using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FastFlat.Controllers
{
    // This controller handles operations related to exploring listings on the platform.
    public class ExplorerController : Controller
    {

        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<BookingModel> _bookingRepo;

        private readonly ILogger<ExplorerController> _logger;

        public ExplorerController(IRentalRepository<ListningModel> rentalRepo, IRentalRepository<AmenityModel> amenityRepo, UserManager<ApplicationUser> userManager, IRentalRepository<BookingModel> bookingRepo, ILogger<ExplorerController> logger)
        {
            _rentalRepo = rentalRepo;
            _amenityRepo = amenityRepo;
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Explore([FromQuery] ExploreRequest request)
        {
            var rentalList = await _rentalRepo.GetAll();
            // Search by city
            if (!string.IsNullOrEmpty(request.Location))
            {
                rentalList = rentalList.Where(listing => listing.ListningCity?.ToLower() == request.Location.ToLower()
                                                || listing.ListningCountry?.ToLower()== request.Location.ToLower());
            }
            // Filter by fromDate. Assuming listings that are available "from" a date are available for bookings from that date.
            if (request.FromDate != default(DateTime))
            {
                rentalList = rentalList.Where(listing => listing.FromDate.HasValue && listing.FromDate.Value <= request.FromDate);
            }
            else
            {
                _logger.LogInformation($"[ExplorerController Explore() GET] FromDate parameter not provided or is default.");
            }
            // Filter by toDate. Assuming listings that are available "to" a date are available for bookings until that date.
            if (request.ToDate != default(DateTime))
            {
                rentalList = rentalList.Where(listing => listing.ToDate.HasValue && listing.ToDate.Value >= request.ToDate);
            }
            else
            {
                _logger.LogInformation($"[ExplorerController Explore() GET] ToDate parameter not provided or is default.");
            }
            // Filter by number of guests (compared with number of beds in the listing)
            if (request.Guests > 0)
            {
                rentalList = rentalList.Where(listing => listing.NoOfBeds.HasValue && listing.NoOfBeds.Value >= request.Guests);
            }
            else
            {
                _logger.LogInformation($"[ExplorerController Explore() GET] Guests parameter not provided or is zero.");
            }
            var requestedAmenities = new List<string>();
            // Assuming Amenities is a JSON string list. Deserialize it and filter listings based on amenities.
            if (!string.IsNullOrEmpty(request.Amenities))
            {
                requestedAmenities = JsonConvert.DeserializeObject<List<string>>(request.Amenities);
                if (requestedAmenities?.Count > 0)
                {
                    var allRentals = await _rentalRepo.GetAll();

                    // Then filter these in-memory listings based on the requested amenities.
                    allRentals = allRentals.Where(listing =>
                        listing.ListningAmenities != null &&
                            requestedAmenities.All(requestedAmenity =>
                                listing.ListningAmenities.Any(listingAmenity =>
                                    listingAmenity.Amenity != null &&
                                        listingAmenity.Amenity.AmenityName == requestedAmenity))).ToList();


                    rentalList = allRentals.AsQueryable();
                }
                else
                {
                    _logger.LogWarning($"[ExplorerController Explore() GET] Deserialized amenities list is empty.");
                }
            }

            var amenityList = await _amenityRepo.GetAll();
            var rentalListViewModel = new RentalListViewModel(rentalList.ToList(), amenityList, requestedAmenities, request.Location, request.Guests, request.FromDate, request.ToDate, "Card");
            return View(rentalListViewModel);
        }



        // Displays detailed view of a single listing.
        [HttpGet]
        public async Task<IActionResult> ViewListing(int listingId)
        {
            var listing = await _rentalRepo.GetById(listingId);
            if (listing == null)
            {
                _logger.LogError($"[ExplorerController ViewListing() GET] Listing not found with id: {listingId}");
                return StatusCode(500,"Listing could not be found.");
            }

            var userId = listing.UserId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError($"[ExplorerController ViewListing() GET] User not found with id: {userId}");
                return StatusCode(500, "User could not be found.");
            }
            
            var rentalListViewModel = new ListingViewModel(listing, user, "View rental property");
            if (rentalListViewModel == null)
            {
                _logger.LogError("[ExplorerController ViewListing() GET] Failed to create ListingViewModel.");
                return StatusCode(500, "Failed to create ListingViewModel.");
            }

            rentalListViewModel.Booking = new BookingModel();

            return View(rentalListViewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ViewListing(ListingViewModel input)
        {
            var userId = _userManager.GetUserId(User);
            ModelState.Remove("Booking.UserId");
            input.Booking.UserId = userId;

            if (ModelState.IsValid)
            {
                DateTime fromDate = Convert.ToDateTime(input.Booking.FromDate);
                DateTime toDate = Convert.ToDateTime(input.Booking.ToDate);

                // Check for existing bookings that overlap with the desired date range.
                var existingBookings = await _bookingRepo.GetAll();
                bool isDateRangeAvailable = true;

                foreach (var booking in existingBookings)
                {
                    if (booking.ListningId == input.Listing.ListningId && !(toDate < booking.FromDate || fromDate > booking.ToDate))
                    {
                        // The dates overlap with an existing booking.
                        ModelState.AddModelError("Booking.FromDate", "The selected date range is already booked.");
                        isDateRangeAvailable = false;
                        break; // Exit the loop since we found an overlap.
                    }
                }

                if (isDateRangeAvailable)
                {
                    var numberOfDays = (decimal)(toDate - fromDate).TotalDays + 1;
                    var totalPrice = input.Listing.ListningPrice * numberOfDays;
                    input.Booking.TotalPrice = totalPrice * (decimal)1.05;
                    input.Booking.ListningId = input.Listing.ListningId;

                    try
                    {
                        await _bookingRepo.Create(input.Booking);
                        _logger.LogInformation($"Creation of new booking succeeded. Booking details: {input.Booking}");
                        return RedirectToAction(nameof(Explore));
                    }
                    catch (Exception e)
                    {
                        _logger.LogError($"[ExplorerController ViewListing() POST] Error occurred while creating a booking: {e.Message}");
                        return StatusCode(500, "Failed to create booking.");
                    }
                }
                return RedirectToAction(nameof(Explore));
            }

            // If ModelState is invalid or date range is not available, rebuild the ViewModel and return the view.
            var listing = await _rentalRepo.GetById(input.Listing.ListningId);
            var ownerUserId = listing.UserId;
            var user = await _userManager.FindByIdAsync(ownerUserId);
            var rentalListViewModel = new ListingViewModel(listing, user, "View rental property");
            rentalListViewModel.Booking = input.Booking;
            return View(rentalListViewModel);
        }


        // AJAX endpoint: Retrieves a list of dates when a specific listing is already booked.
        // This method is designed to work in conjunction with frontend AJAX calls to fetch booked dates and update the datepickers.
        [HttpGet("Explorer/GetBookedDates")]
        public async Task<ActionResult> GetBookedDates([FromQuery] int listingId)
        {
            try
            {
                // Fetch all bookings for the specified listing
                var bookings = await _bookingRepo.GetAll();
                bookings = bookings.Where(b => b.ListningId == listingId).ToList();

                var bookedDates = new List<DateTime>();
                foreach (var booking in bookings)

                {   // Populate the 'bookedDates' list with every individual date from each booking's 'FromDate' to 'ToDate'.
                    for (var date = booking.FromDate; date <= booking.ToDate; date = date.AddDays(1))
                    {
                        bookedDates.Add(date);
                    }
                }
                return Ok(bookedDates);
            }
            catch (Exception e)
            {
                _logger.LogError($"[ExplorerController GetBookedDates()] Error occurred while fetching booked dates: {e.Message}");
                throw;
            }
        }

        // AJAX endpoint: Retrieves the start and end dates when a specific listing is available for booking.
        // This method is designed to work in conjunction with frontend AJAX calls to fetch available date ranges and update the datepickers.
        [HttpGet("Explorer/GetAvailableDates")]
        public async Task<ActionResult> GetAvailableDates([FromQuery] int listingId)
        {
            try
            {
                // Fetch the specified listing
                var listning = await _rentalRepo.GetById(listingId);
                if (listning == null)
                {
                    _logger.LogWarning($"[ExplorerController GetAvailableDates()] Listing not found with id: {listingId}");
                }

                // Get the date range when the listing is available
                var availableDates = (listning?.FromDate, listning?.ToDate);

                return Json(availableDates);
            }
            catch (Exception e)
            {
                _logger.LogError($"[ExplorerController GetAvailableDates()] Error occurred while fetching available dates: {e.Message}");
                return StatusCode(500, "Failed to fetch available dates.");
            }
        }



    }
}
