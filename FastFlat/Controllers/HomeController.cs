using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}