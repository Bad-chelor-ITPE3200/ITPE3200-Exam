using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace FastFlat.Controllers
{
    public class ExplorerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<BookingModel> _bookingRepo;
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


        [HttpGet]
        public async Task<IActionResult> ViewListing(int listingId)
        {
            var listing = await _rentalRepo.GetById(listingId);
            var userId = listing.UserId;
            var user = await _userManager.FindByIdAsync(userId);

            var rentalListViewModel = new ListingViewModel(listing, user, "View rental property");
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
                foreach (var modelStateKey in ModelState.Keys)
                {
                  
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        // Du kan logge feilen, skrive den ut i konsollen eller bare se den i debug-modus.
                        System.Diagnostics.Debug.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                        
                        
                    }
                }
                
            }
            
                if (ModelState.IsValid)
                {
                    DateTime fromDate = Convert.ToDateTime(input.Booking.FromDate);
                    DateTime toDate = Convert.ToDateTime(input.Booking.ToDate);
                    var numberOfDays = (decimal)(toDate - fromDate).TotalDays + 1;
                    var totalPrice = input.Listing.ListningPrice * numberOfDays;

                    input.Booking.TotalPrice = totalPrice;

                    //var totalDays = (input.Listing.ToDate - input.Listing.FromDate).Days;

                    input.Booking.ListningId = input.Listing.ListningId;
                    _logger.LogInformation("Account is created");
                    await _bookingRepo.Create(input.Booking);
                    return RedirectToAction(nameof(Explore));
                }
                else
            {
                // Hvis ModelState er ugyldig, bygg ListingViewModel på nytt
                var listing = await _rentalRepo.GetById(input.Listing.ListningId); // Anta at ListingId er tilgjengelig fra input
                var ownerUserId = listing.UserId;
                var user = await _userManager.FindByIdAsync(ownerUserId);

                var rentalListViewModel = new ListingViewModel(listing, user, "View rental property");
                rentalListViewModel.Booking = input.Booking; // Behold bookinginformasjonen som brukeren har sendt

                return View(rentalListViewModel);
            }
        }

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
            var dates = await _rentalRepo.GetAvailableDatesForListning(listningId);
            return Ok(new { fromDate = dates.StartDate, toDate = dates.EndDate });
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
