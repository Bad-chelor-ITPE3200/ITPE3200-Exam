using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            var userRentals = (await _listningRepository.GetAll()).Where(r => r.UserId == user.Id).ToList();

            var userBookings = (await _bookingRepository.GetAll()).Where(b => b.UserId == user.Id).ToList();

            var profileViewModel = new ProfileViewModel(userRentals, userBookings, user, "Profile");
            return View(profileViewModel);

        }


    }

}
