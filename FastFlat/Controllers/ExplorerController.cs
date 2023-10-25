using System.Composition;
using System.Reflection.Metadata.Ecma335;
using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace FastFlat.Controllers
{
    public class ExplorerController : Controller
    {

        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<BookingModel> _bookingRepo;
        private readonly IRentalRepository<ContryModel> _contryRepo;
        private readonly ILogger<ExplorerController> _logger;

        public ExplorerController(IRentalRepository<ListningModel> rentalRepo, IRentalRepository<AmenityModel> amenityRepo, UserManager<ApplicationUser> userManager,IRentalRepository<ContryModel> contryRepo, IRentalRepository<BookingModel> bookingRepo, ILogger<ExplorerController>logger)
        {
            _rentalRepo = rentalRepo;
            _amenityRepo = amenityRepo;
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _contryRepo = contryRepo;
            _logger = logger; 
        }
        [HttpGet]
        public async Task<IActionResult> Explore()
        {

            var rentalList = _rentalRepo.GetAll();
            if (rentalList == null)
            {
                _logger.LogError("[ExplorerController Expore() GET] rentalList list not found while executing _rentalRepo.GetAll()");
            }
            var amenityList = _amenityRepo.GetAll();
            if (amenityList == null)
            {
                _logger.LogError("[ExplorerController] amenityList list not found while executing _amenityRepo.GetAll()");
            }

            var rentalListViewModel = new RentalListViewModel(rentalList, amenityList, "Card");
            return View(rentalListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Explore(RentalListViewModel input)
        {

            var allRentals = _rentalRepo.GetAll();
            if(allRentals == null)
            {
                _logger.LogError("[ExplorerController Expore() POST] allRentals list not found while executing _rentalRepo.GetAll()");
            }

            if (input != null)
            {
                allRentals = allRentals.Where(rental => rental.ListningAmenities.Any(am => input.SelectedAmenities.Contains(am.Amenity.AmenityName)));
            }

            var amenityList = _amenityRepo.GetAll();
            if (amenityList == null)
            {
                _logger.LogError("[ExplorerController Expore() POST] amenityList list not found while executing _amenityRepo.GetAll()");
            }
            var rentalListViewModel = new RentalListViewModel(allRentals, amenityList, "Card");

            ViewBag.SelectedAmenity = input.SelectedAmenities.FirstOrDefault(); // Lagre den valgte amenity i ViewBag for å bruke den i visningen
            if (ViewBag.SelectedAmenity == null)
            {
                _logger.LogWarning("[ExplorerController Explore() POST] No selected amenity found in input.SelectedAmenities.");
            }
            return View(rentalListViewModel);
        }




        [HttpGet]
        public async Task<IActionResult> ViewListing(int listingId)
        {
            var listing = await _rentalRepo.GetById(listingId);
            if (listing == null)
            {
                _logger.LogError($"[ExplorerController ViewListing() GET] Listing not found with id: {listingId}");
                return NotFound("Listing could not be found.");
            }

            var userId = listing.UserId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError($"[ExplorerController ViewListing() GET] User not found with id: {userId}");
                return NotFound("User could not be found.");
            }

            var rentalListViewModel = new ListingViewModel(listing, user, "View rental property");
            if (rentalListViewModel == null)
            {
                _logger.LogError("[ExplorerController ViewListing() GET] Failed to create ListingViewModel.");
            }

            rentalListViewModel.Booking = new BookingModel();

            return View(rentalListViewModel);
        }


        //legger til booking 
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ViewListing(ListingViewModel input)
        {

            var userId = _userManager.GetUserId(User);
            ModelState.Remove("Booking.UserId");
            input.Booking.UserId = userId;
            if (!ModelState.IsValid)
            {
                _logger.LogError("[ExplorerController ViewListing() POST] Modelstate is not valid in VeiwListing");
            }
            
                if (ModelState.IsValid)
                {
                    DateTime fromDate = Convert.ToDateTime(input.Booking.FromDate);
                    DateTime toDate = Convert.ToDateTime(input.Booking.ToDate);
                    var numberOfDays = (decimal)(toDate - fromDate).TotalDays + 1;
                    var totalPrice = input.Listing.ListningPrice * numberOfDays;
                    input.Booking.TotalPrice = totalPrice;
                    input.Booking.ListningId = input.Listing.ListningId;

                try
                {
                    await _bookingRepo.Create(input.Booking);
                    _logger.LogInformation("Creation of new booking succeeded. Booking details: {input.Booking}");
                }
                catch (Exception e)
                {
                    _logger.LogError("[ExplorerController ViewListing() POST] Error occurred while creating a booking: {e.Message}");
                }
                return RedirectToAction(nameof(Explore));
                }

                else
                {
                // Hvis ModelState er ugyldig, bygg ListingViewModel på nytt
                var listing = await _rentalRepo.GetById(input.Listing.ListningId);
                var ownerUserId = listing.UserId;
                var user = await _userManager.FindByIdAsync(ownerUserId);
                var rentalListViewModel = new ListingViewModel(listing, user, "View rental property");
                rentalListViewModel.Booking = input.Booking; 
                return View(rentalListViewModel);
            }
        }

        /*

        //henter booked dato
        [HttpGet]
        public async Task<IActionResult> GetBookedDates(int listningId)
        {
            // Hent alle booking datoer for denne listningen fra databasen
            var bookedDates = await _bookingRepo.GetBookedDatesForListning(listningId);
            return Json(bookedDates);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableDatesForListning(int listningId)
        {
            //Gets all the dates that are available for booking
            var dates = await _rentalRepo.GetAvailableDatesForListning(listningId);
            _logger.LogInformation("available dates have been gained" + dates);
            return Ok(new { fromDate = dates.StartDate, toDate = dates.EndDate });
        }

        [HttpGet("api/available-countries")]
        public IActionResult GetAvailableCountries()
        {
            var countries = _rentalRepo.GetAvailableCountries(); // Assuming _rentalRepository is an instance of RentalRepository
            return Ok(countries);
        }
        */

        public async Task<ActionResult> GetBookedDates(int listningId)
        {
            try
            {
                var bookings = _rentalRepo.GetAll().OfType<BookingModel>()
                                              .Where(b => b.ListningId == listningId).ToList();

                var bookedDates = new List<DateTime>();
                foreach (var booking in bookings)
                {
                    for (var date = booking.FromDate; date <= booking.ToDate; date = date.AddDays(1))
                    {
                        bookedDates.Add(date);
                    }
                }

                return View(bookedDates);
            }
            catch (Exception e)
            {
                _logger.LogError("[ExplorerController GetBookedDates()] Error occurred while fetching booked dates: {e.Message}");
                throw;
            }
        }

        public async Task<ActionResult> GetAvailableDates(int listningId)
        {
            try
            {
                var listning = await _rentalRepo.GetById(listningId);
                if (listning == null)
                {
                    _logger.LogWarning("[ExplorerController GetAvailableDates()] Listing not found with id: {listningId}");
                }

                var availableDates = (listning?.FromDate, listning?.ToDate);

                return View(availableDates);
            }
            catch (Exception e)
            {
                _logger.LogError("[ExplorerController GetAvailableDates()] Error occurred while fetching available dates: {e.Message}");
                throw;
            }
        }

        [HttpGet("api/available-countries")]
        public async Task<IActionResult> GetAvailableCountries()
        {
            try
            {
                var allCountries = _contryRepo.GetAll();
                if (allCountries == null)
                {
                    _logger.LogWarning("[ExplorerController GetAvailableCountries()] No countries found in the repository.");
                }

                var countryNames = await allCountries.Select(c => c.Contryname).ToListAsync();

                return Ok(countryNames);
            }
            catch (Exception e)
            {
                _logger.LogError("[ExplorerController GetAvailableCountries()] Error occurred while fetching available countries: {e.Message}");
                throw;
            }
        }



    }
}
