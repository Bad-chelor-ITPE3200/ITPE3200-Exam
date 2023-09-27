using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

    }
}
