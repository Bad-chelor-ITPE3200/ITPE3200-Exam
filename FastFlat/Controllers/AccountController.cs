
using FastFlat.DAL;
using FastFlat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace FastFlat.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRentalRepository<BookingModel> _bookingrepository;
        private readonly IRentalRepository<ApplicationUser> _ApplicationUserRepostiory; 
        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IRentalRepository<BookingModel>bookingrepository, IRentalRepository<ApplicationUser> applicationUserRepostiory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _bookingrepository = bookingrepository;
            _ApplicationUserRepostiory = applicationUserRepostiory; 
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
            var bookings = _bookingrepository.GetAll().ToList();
            return View(bookings);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> _AdminAccounts()
        {
            var users = _userManager.Users;
            return View(users);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
          await _bookingrepository.Delete(id);
          return RedirectToAction(nameof(ManageAllBookings)); 
          
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdminAccount(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(_AdminAccounts)); 
        }

       [HttpPost]
       [Authorize(Roles = "Admin")]
       public async Task<IActionResult> UpdateAdminAccount(ApplicationUser userupdated)
       {
        await _userManager.UpdateAsync(userupdated);
        return RedirectToAction(nameof(_AdminAccounts)); 
       }
    }
}
