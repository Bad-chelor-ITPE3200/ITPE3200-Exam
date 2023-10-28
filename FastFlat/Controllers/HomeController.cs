using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;

        public HomeController(IRentalRepository<ListningModel> rentalRepo, IRentalRepository<AmenityModel> amenityRepo)
        {
            _rentalRepo = rentalRepo;
            _amenityRepo = amenityRepo;
        }

        public async Task<IActionResult> Index()
        {
            var rentalList = await _rentalRepo.GetAll();
            var amenityList = await _amenityRepo.GetAll();
            var rentalListViewModel = new RentalListViewModel(rentalList.Take(5), amenityList, null, null, null, null, null, "Card");
            return View(rentalListViewModel);
        }

    }
}