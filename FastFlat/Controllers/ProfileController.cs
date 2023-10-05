using FastFlat.DAL;
using FastFlat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IRentalRepository<ListningModel> _listningRepository;
        private readonly IRentalRepository<BookingModel> _bookingRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(IRentalRepository<ListningModel> listningRepository, IRentalRepository<BookingModel> bookingRepository, UserManager<IdentityUser> userManager)
        {
            _listningRepository = listningRepository;
            _bookingRepository = bookingRepository;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            //var users = await _userManager.Users.;
            return View(user);
        }

    }
}
