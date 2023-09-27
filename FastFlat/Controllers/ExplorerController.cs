using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class ExplorerController : Controller
    {

        private readonly IRentalRepository<ListningModel> _rentalRepo;
        private readonly IRentalRepository<AmenityModel> _amenityRepo;
        public ExplorerController(IRentalRepository<ListningModel> rentalRepo, IRentalRepository<AmenityModel> amenityRepo)
        {
            _rentalRepo = rentalRepo;
            _amenityRepo = amenityRepo;
        }
        public async Task<IActionResult> Explore()
        {

            var rentalList =await _rentalRepo.GetAll();
            var amenityList = await _amenityRepo.GetAll();
            var rentalListViewModel = new RentalListViewModel(rentalList, amenityList, "Card");
            return View(rentalListViewModel);
        }
    }
}
