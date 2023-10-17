
using FastFlat.DAL;
using FastFlat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRentalRepository<BookingModel> _bookingrepository;
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IRentalRepository<BookingModel>bookingrepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _bookingrepository = bookingrepository; 
        }

        [Authorize]
        public async Task<IActionResult> Account()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        
        //todo:  figure out how to use the rolemanager, as admin to have an "uniqe" page
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageAllBookings()
        {
            //return View(AdminPage); 
            var bookings = _bookingrepository.GetAll();
            return View(bookings);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> _AdminAccounts()
        {
            var users = _userManager.Users.ToListAsync();
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
          await _bookingrepository.Delete(id);
          return RedirectToAction(nameof(ManageAllBookings)); 
            
        }
        
    }
}
