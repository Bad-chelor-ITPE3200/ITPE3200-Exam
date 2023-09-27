using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class ExplorerController : Controller
    {

        private readonly IRentalRepository _rentalRepo;
        public ExplorerController(IRentalRepository rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }
        public async Task<IActionResult> Explore()
        {

            var rentalList =await _rentalRepo.getAll();
            var rentalListViewModel = new RentalListViewModel(rentalList, "Explore");
            return View(rentalListViewModel);
        }
    }
}
