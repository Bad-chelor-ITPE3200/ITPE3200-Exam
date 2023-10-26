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
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FastFlat.Controllers
{
    // This controller handles operations related to exploring listings on the platform.
    public class ExplorerController : Controller
    {
        // ... (variables and constructor) ...

        private readonly HttpClient _httpClient;
        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<BookingModel> _bookingRepo;
        private readonly IRentalRepository<ContryModel> _contryRepo;
        private readonly ILogger<ExplorerController> _logger;

        public ExplorerController(IHttpClientFactory httpClientFactory, IRentalRepository<ListningModel> rentalRepo, IRentalRepository<AmenityModel> amenityRepo, UserManager<ApplicationUser> userManager, IRentalRepository<BookingModel> bookingRepo, ILogger<ExplorerController>logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _rentalRepo = rentalRepo;
            _amenityRepo = amenityRepo;
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _logger = logger; 
        }

        private async Task<string> GetAmadeusAccessTokenAsync()
        {
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://test.api.amadeus.com/v1/security/oauth2/token");

            var client_id = "AtXWGpJxGJHSdIFtL1LOASA6kzGcWAFS"; // Read from a secure place, NOT hardcoded
            var client_secret = "AGPvwdO8OF1Fhjao"; // Read from a secure place, NOT hardcoded

            tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"},
                {"client_id", client_id},
                {"client_secret", client_secret}
            });

            var tokenResponse = await _httpClient.SendAsync(tokenRequest);
            var tokenData = await tokenResponse.Content.ReadFromJsonAsync<AmadeusTokenResponse>();

            return tokenData?.AccessToken;
        }

        private class AmadeusTokenResponse
        {
            [JsonPropertyName("access_token")]
            public string? AccessToken { get; set; }

           
        }

        [HttpGet]
        public async Task<IActionResult> Explore([FromQuery] ExploreRequest request)
        {
            var rentalList = _rentalRepo.GetAll();

            // Search by city
            if (!string.IsNullOrEmpty(request.City))
            {
                rentalList = rentalList.Where(listing => listing.ListningCity.ToLower() == request.City.ToLower());
            }

            // Filter by fromDate. Assuming listings that are available "from" a date are available for bookings from that date.
            if (request.FromDate != default(DateTime))
            {
                rentalList = rentalList.Where(listing => listing.FromDate.HasValue && listing.FromDate.Value <= request.FromDate);
            }

            // Filter by toDate. Assuming listings that are available "to" a date are available for bookings until that date.
            if (request.ToDate != default(DateTime))
            {
                rentalList = rentalList.Where(listing => listing.ToDate.HasValue && listing.ToDate.Value >= request.ToDate);
            }

            // Filter by number of guests (compared with number of beds in the listing)
            if (request.Guests > 0)
            {
                rentalList = rentalList.Where(listing => listing.NoOfBeds.HasValue && listing.NoOfBeds.Value >= request.Guests);
            }
            var requestedAmenities = new List<string>();
            // Assuming Amenities is a JSON string list. Deserialize it and filter listings based on amenities.
            if (!string.IsNullOrEmpty(request.Amenities))
            {
                requestedAmenities = JsonConvert.DeserializeObject<List<string>>(request.Amenities);
                if (requestedAmenities.Count > 0)
                {
                    var allRentals = rentalList.ToList();

                    // Then filter these in-memory listings based on the requested amenities.
                    allRentals = allRentals.Where(listing =>
                        listing.ListningAmenities != null &&
                        requestedAmenities.All(requestedAmenity =>
                            listing.ListningAmenities.Any(listingAmenity =>
                                listingAmenity.Amenity.AmenityName == requestedAmenity))).ToList();

                    rentalList = allRentals.AsQueryable();
                }
            }

            var amenityList = _amenityRepo.GetAll();
            var rentalListViewModel = new RentalListViewModel(rentalList.ToList(), amenityList, requestedAmenities, request.City, request.Guests, request.FromDate, request.ToDate, "Card");
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


        // Allows users to book a listing by providing booking details.
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

                    // Calculate the total number of days between the 'FromDate' and 'ToDate'.
                    DateTime fromDate = Convert.ToDateTime(input.Booking.FromDate);
                    DateTime toDate = Convert.ToDateTime(input.Booking.ToDate);
                    var numberOfDays = (decimal)(toDate - fromDate).TotalDays + 1;


                // Calculate the total price for the booking based on the daily rate and the total number of days.
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

        // AJAX endpoint: Retrieves a list of dates when a specific listing is already booked.
        // This method is designed to work in conjunction with frontend AJAX calls to fetch booked dates and update the datepickers.
        public async Task<ActionResult> GetBookedDates(int listningId)
        {
            try
            {
                // Fetch all bookings for the specified listing
                var bookings = _rentalRepo.GetAll().OfType<BookingModel>()
                                              .Where(b => b.ListningId == listningId).ToList();

                var bookedDates = new List<DateTime>();
                foreach (var booking in bookings)

                {   // Populate the 'bookedDates' list with every individual date from each booking's 'FromDate' to 'ToDate'.
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

        // AJAX endpoint: Retrieves the start and end dates when a specific listing is available for booking.
        // This method is designed to work in conjunction with frontend AJAX calls to fetch available date ranges and update the datepickers.
        public async Task<ActionResult> GetAvailableDates(int listningId)
        {
            try
            {
                // Fetch the specified listing
                var listning = await _rentalRepo.GetById(listningId);
                if (listning == null)
                {
                    _logger.LogWarning("[ExplorerController GetAvailableDates()] Listing not found with id: {listningId}");
                }

                // Get the date range when the listing is available
                var availableDates = (listning?.FromDate, listning?.ToDate);

                return View(availableDates);
            }
            catch (Exception e)
            {
                _logger.LogError("[ExplorerController GetAvailableDates()] Error occurred while fetching available dates: {e.Message}");
                throw;
            }
        }
        [HttpGet("api/city-search")]
        public async Task<IActionResult> GetAvailableCities([FromQuery] string keyword)
        {
            if (string.IsNullOrEmpty(keyword) || keyword.Length <= 2)
            {
                return NoContent();
            }
            else
            {
                try
                {
                    var accessToken = await GetAmadeusAccessTokenAsync();
                    if (string.IsNullOrEmpty(accessToken))
                    {
                        return BadRequest("Unable to obtain Amadeus access token.");
                    }

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Add the keyword to the query parameter
                    var endpointUrl = $"https://test.api.amadeus.com/v1/reference-data/locations/cities?keyword={Uri.EscapeDataString(keyword)}";

                    var response = await _httpClient.GetAsync(endpointUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Kunne ikke hente byer, feil i responsen.");
                        return BadRequest($"Error fetching cfewknfewknities: {response}");
                    }

                    var cities = await response.Content.ReadAsStringAsync();
                    return Ok(cities);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the API call
                    Console.WriteLine($"Kunne ikke hente byer: ${ex}");
                    return BadRequest($"An error occurred: {ex.Message}");
                }
            }

        }



    }
}
