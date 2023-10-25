using System.Composition;
using System.Reflection.Metadata.Ecma335;
using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Core.Types;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;

namespace FastFlat.Controllers
{
    public class ExplorerController : Controller
    {

        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<BookingModel> _bookingRepo;
        private readonly ILogger<ExplorerController> _logger;

        public ExplorerController(IRentalRepository<ListningModel> rentalRepo, IRentalRepository<AmenityModel> amenityRepo, UserManager<ApplicationUser> userManager, IRentalRepository<BookingModel> bookingRepo, ILogger<ExplorerController>logger)
        {
            _rentalRepo = rentalRepo;
            _amenityRepo = amenityRepo;
            _userManager = userManager;
            _bookingRepo = bookingRepo;
            _logger = logger; 
        }
        [HttpGet]
        public async Task<IActionResult> Explore()
        {

            var rentalList = _rentalRepo.GetAll();
            var amenityList = _amenityRepo.GetAll();
            var rentalListViewModel = new RentalListViewModel(rentalList, amenityList, "Card");
            return View(rentalListViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Explore(RentalListViewModel input, SearchViewModel inputFilter)
        {

            var allRentals = _rentalRepo.GetAll().ToList();

            // Convert each rental to a string representation that includes all its attributes
            var rentalDescriptions1 = allRentals.Select(rental =>
                $"Rental ID: {rental.ListningId}, " +
                $"Address: {rental.ListningAddress}, " +
                $"FromDate: {rental.FromDate}, " +
                $"ToDate: {rental.ToDate}, " +
                $"NoOfBeds: {rental.NoOfBeds}, "
            );

            // Join these string representations together
            var allRentalsString1 = string.Join("\n", rentalDescriptions1);

            // Log the combined string
            _logger.LogInformation($"All Rentals Nardwuar:\n{allRentalsString1}");

            // Check if the search bar was used by examining the inputFilter object
            if (inputFilter.Contry != null || inputFilter.FromDate != default || inputFilter.ToDate != default || inputFilter.NoOfBeds != 0)
            {
                // Filter rentals based on the input
                allRentals = allRentals
                .Where(rental => rental.ListningAddress == inputFilter.Contry)
                .Where(rental => rental.FromDate <= inputFilter.FromDate && rental.ToDate >= inputFilter.ToDate)
                .Where(rental => rental.NoOfBeds >= inputFilter.NoOfBeds)
                .ToList();
            }

            _logger.LogInformation("Bruh Yo mama \n" + "Prøvd Land!!!!" + inputFilter.Contry + "\n" + inputFilter.NoOfBeds + " har så mange senger \n"
                + inputFilter.FromDate + " er fraDatoen \n" + "Landet som er valgt er: " + inputFilter.Contry);

            // Convert each rental to a string representation
            var rentalDescriptions = allRentals.Select(rental =>
                $"Rental ID: {rental.ListningId}, Address: {rental.ListningAddress}, FromDate: {rental.FromDate}, ToDate: {rental.ToDate}, NoOfBeds: {rental.NoOfBeds}"
            );

            // Join these string representations together
            var allRentalsString = string.Join("\n", rentalDescriptions);

            // Log the combined string
            _logger.LogInformation($"Filtered Rentals:\n{allRentalsString}");


            if (input != null && input.SelectedAmenities != null)
            {   // Code filters allRentals to only include rentals that have at least one amenity that matches the amenities in the input.
                // If a rental doesn't have any of the selected amenities, it won't be included in the updated allRentals 
                allRentals = allRentals
                    .Where(rental => rental.ListningAmenities.Any(am => input.SelectedAmenities.Contains(am.Amenity.AmenityName)))
                    .ToList();
            }

            var amenityList = _amenityRepo.GetAll();
            var rentalListViewModel = new RentalListViewModel(allRentals, amenityList, "Card");

            if (input?.SelectedAmenities != null)
            {
                // Lagre den valgte amenity i ViewBag for å bruke den i visningen
                ViewBag.SelectedAmenity = input.SelectedAmenities.FirstOrDefault();
            }
            else
            {
                ViewBag.SelectedAmenity = null;
            }
            
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

        [HttpGet("api/available-countries")]
        public IActionResult GetAvailableCountries()
        {
            var countries = _rentalRepo.GetAvailableCountries(); // Assuming _rentalRepository is an instance of RentalRepository
            return Ok(countries);
        }

    }
}
