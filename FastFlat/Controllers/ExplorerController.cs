using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Core.Types;

namespace FastFlat.Controllers
{
    public class ExplorerController : Controller
    {

        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<BookingModel> _bookingRepo;

        public ExplorerController(IRentalRepository<ListningModel> rentalRepo, IRentalRepository<AmenityModel> amenityRepo, UserManager<ApplicationUser> userManager, IRentalRepository<BookingModel> bookingRepo)
        {
            _rentalRepo = rentalRepo;
            _amenityRepo = amenityRepo;
            _userManager = userManager;
            _bookingRepo = bookingRepo;

        }
        public async Task<IActionResult> Explore()
        {

            var rentalList = _rentalRepo.GetAll();
            var amenityList = _amenityRepo.GetAll();
            var rentalListViewModel = new RentalListViewModel(rentalList, amenityList, "Card");
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

    }
}
