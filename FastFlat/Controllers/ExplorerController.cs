using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.Controllers
{
    public class ExplorerController : Controller
    {

        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExplorerController(IRentalRepository<ListningModel> rentalRepo, IRentalRepository<AmenityModel> amenityRepo, UserManager<ApplicationUser> userManager)
        {
            _rentalRepo = rentalRepo;
            _amenityRepo = amenityRepo;
            _userManager = userManager;
        }
        public async Task<IActionResult> Explore()
        {

            var rentalList = _rentalRepo.GetAll();
            var amenityList =  _amenityRepo.GetAll();
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
            return View(rentalListViewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ViewListing()
        {
           return View();
        }
    }
}
