using FastFlat.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class RentalController : Controller
    {
        public IActionResult RentalCards()
        {
            var rentals = new List<Rental>();
            var rental1 = new Rental
            {
                rentalId = 1,
                name = "Fin leilighet i Oslo sentrum",
                address = "Osloveien 21B",
                price = 1800,
                fromDate = new DateOnly(2023, 9, 15),
                toDate = new DateOnly(2023, 9, 18),
            };
            var rental2 = new Rental
            {
                rentalId = 2,
                name = "Stor familievilla i sentrum",
                address = "Solveien 23, Trondheim",
                price = 2750,
                fromDate = new DateOnly(2023, 9, 18),
                toDate = new DateOnly(2023, 9, 26),
            };
            rentals.Add(rental1);
            rentals.Add(rental2);

            ViewBag.CurrentViewName = "List of rentals.";
            return View(rentals);
        }
    }
}
