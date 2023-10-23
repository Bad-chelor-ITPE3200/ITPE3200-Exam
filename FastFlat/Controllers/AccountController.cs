using Castle.Components.DictionaryAdapter.Xml;
using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace FastFlat.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRentalRepository<BookingModel> _bookingrepository;
        private readonly IRentalRepository<ApplicationUser> _ApplicationUserRepostiory;
        private readonly ILogger<AccountController> _logger;
        private IRentalRepository<ListningModel> _listingRepository; 

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IRentalRepository<BookingModel> bookingrepository,
            IRentalRepository<ApplicationUser> applicationUserRepostiory, IRentalRepository<ListningModel>listingRepository,ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _bookingrepository = bookingrepository;
            _ApplicationUserRepostiory = applicationUserRepostiory;
            _listingRepository = listingRepository; 
            _logger = logger;
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
        public async Task<IActionResult> AdminUpdateAuser(ApplicationUser user)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("regexp invalid");
                return View(user);
            }else
            {


                var newuser = _userManager.FindByEmailAsync(user.Email).Result;
                var applicationUserVeiwModel = new ApplicationUserVeiwModel(user.UserName, user.FirstName,
                    user.LastName, user.Email, user.PhoneNumber, user.ProfilePicture, "UpdateAdminAccount");
                try
                {
                    newuser.FirstName = applicationUserVeiwModel.FirstName;
                    newuser.LastName = applicationUserVeiwModel.LastName;
                    newuser.PhoneNumber = applicationUserVeiwModel.PhoneNumber;
                    newuser.Email = applicationUserVeiwModel.Email;
                    newuser.ProfilePicture = applicationUserVeiwModel.ProfilePicture;
                    var ok = await _userManager.UpdateAsync(newuser);
                    if (ok.Succeeded)
                    {
                        _logger.LogInformation("change OK");
                        return RedirectToAction(nameof(_AdminAccounts));
                    }
                    else
                    {
                        _logger.LogWarning("CHANGE BAD");
                        _logger.LogCritical(ok.ToString());
                        return NotFound();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e.ToString());
                    return RedirectToAction("_AdminAccounts");
                }
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateRoles(ApplicationUser user, string newrole)
        {
            if (!_userManager.GetRolesAsync(user).Equals(newrole))
            {
                _logger.LogInformation("New role has been added");
                await _userManager.AddToRoleAsync(user, newrole);
                return RedirectToAction(nameof(_AdminAccounts));
            }
            else
            {
                _logger.LogWarning("User already had the role");
                return RedirectToAction(nameof(_AdminAccounts));
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateAuser(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                _logger.LogError("user not found" + user);
                return NotFound();
            }
            else
            {
                return View(user);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> _ManageListings()
        {
            return View(_listingRepository.GetAll().ToList());
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAlisting(int id)
        {
           var booked = _listingRepository.GetById(id).Result;
           await _bookingrepository.Delete(id);
           _logger.LogInformation("Account deleted!");
           return RedirectToAction(nameof(ManageAllBookings));
        }
    }
}