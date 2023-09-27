using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class ExplorerController : Controller
    {

        private readonly IRentalRepository<ListningModel> _rentalRepo;
        public ExplorerController(IRentalRepository<ListningModel> rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }
        public async Task<IActionResult> Explore()
        {

            var rentalList =await _rentalRepo.GetAll();
            var rentalListViewModel = new RentalListViewModel(rentalList, "Card");
            return View(rentalListViewModel);
        }
    }
}
