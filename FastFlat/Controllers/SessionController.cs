using Castle.Core.Resource;
using FastFlat.DAL;
using FastFlat.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class SessionController : Controller
    {
        /*
        private readonly IRentalRepository<AspNetUsers> _aspNetUsers;

        public SessionController(IRentalRepository<AspNetUsers> aspNetUsersRepository)
        {
            _aspNetUsers = aspNetUsersRepository;
        }

        public async Task<IActionResult> Create(AspNetUsers user)
        {
            if (ModelState.IsValid)
            {
                await _aspNetUsers.Create(user);
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        */


        public IActionResult Login()
        {
            return View();
        }

        
    }

}

