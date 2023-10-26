using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;


namespace FastFlat.Controllers
{
    // Controller for managing accounts.
    public class AccountController : Controller
    {
        // ... (variables and constructor) ...
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRentalRepository<BookingModel> _bookingrepository;
        private readonly IRentalRepository<ApplicationUser> _ApplicationUserRepostiory;
        private readonly ILogger<AccountController> _logger;
        private IRentalRepository<ListningModel> _listingRepository;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IRentalRepository<BookingModel> bookingrepository,
            IRentalRepository<ApplicationUser> applicationUserRepostiory, IRentalRepository<ListningModel> listingRepository, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _bookingrepository = bookingrepository;
            _ApplicationUserRepostiory = applicationUserRepostiory;
            _listingRepository = listingRepository;
            _logger = logger;
        }

        // Displays a view with all users for admins.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Account()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                _logger.LogInformation("[AccountController Account()] Users retrieved successfully.");
                return View(users);
            }
            catch (Exception e)
            {
                _logger.LogError($"[AccountController Account()] Error retrieving users: {e.Message}");
                throw;
            };
        }

        // Allows admins to manage all bookings.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageAllBookings()
        {
            try
            {
                var bookings = _bookingrepository.GetAll().ToList(); //finds all boooking
                _logger.LogInformation("[AccountController ManageAllBookings()] Bookings retrieved successfully.");
                return View(bookings);
            }
            catch (Exception e)
            {
                _logger.LogWarning("[AccountController ManageAllBookings()] Error retrieving bookings: {e.Message}");
                return NotFound();
            }
        }

        // Displays a view with all admin accounts for admins
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> _AdminAccounts()
        {
            try
            {
                var users = _userManager.Users;
                _logger.LogInformation("[AccountController _AdminAccounts()] Users retrieved successfully.");
                return View(users);
            }
            catch (Exception e)
            {
                _logger.LogCritical("[AccountController _AdminAccounts()] Error retrieving users: {e.Message}");
                return NotFound();
            }
        }

        // Allows admins to delete a booking using its ID.
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("[AccountController DeleteAdmin()] Invalid ID provided.");
                    throw new Exception("Invalid ID provided.");
                }
                var okDelete = await _bookingrepository.Delete(id);
                if (!okDelete)
                {
                    _logger.LogWarning("[AccountController DeleteAdmin()] Booking deletion failed.");
                    throw new Exception("Booking deletion failed.");
                }
                return RedirectToAction(nameof(ManageAllBookings));
            }
            catch (Exception e)
            {
                _logger.LogError($"[AccountController DeleteAdmin()] Error deleting booking with ID {id}: {e.Message}");
                return NotFound();
            }
        }

        // Allows admins to delete an admin account using its user ID.
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdminAccount(string userId)
        {
            try
            {

                var user = _userManager.FindByIdAsync(userId).Result;
                await _userManager.DeleteAsync(user);
                _logger.LogInformation($"[AccountController DeleteAdminAccount()] User with ID {userId} deleted successfully.");
                return RedirectToAction(nameof(_AdminAccounts));

            }
            catch (Exception e)
            {
                _logger.LogError($"[AccountController DeleteAdminAccount()] Error deleting user with ID {userId}: {e.Message}");
                return NotFound(e);
            }
        }


        // Allows admins to update a user's account details.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateAuser(ApplicationUser user)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("[AccountController AdminUpdateAuser()] Invalid ModelState.");
                return View(user);
            }
            else
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
                        _logger.LogInformation("[AccountController AdminUpdateAuser()] User with email {user.Email} updated successfully.");
                        return RedirectToAction(nameof(_AdminAccounts));
                    }
                    else
                    {
                        _logger.LogWarning("[AccountController AdminUpdateAuser()] Error updating user with email {user.Email}. Errors: {string.Join(", ", result.Errors.Select(err => err.Description))}");
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


        // Allows admins to update a user's roles.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> updateRoles(ApplicationUser user, string newrole)
        {
            if (!_userManager.GetRolesAsync(user).Equals(newrole))
            {
                _logger.LogInformation("[AccountController updateRoles()] Role {newrole} added to user with ID {user.Id}.");
                await _userManager.AddToRoleAsync(user, newrole);
                return RedirectToAction(nameof(_AdminAccounts));
            }
            else
            {
                _logger.LogWarning("[AccountController updateRoles()] User with ID {user.Id} already had the role {newrole}.");
                return RedirectToAction(nameof(_AdminAccounts));
            }
        }

        // Allows admins to update a user's account using its ID.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateAuser(string id)
        {
            try
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _logger.LogError($"[AccountController AdminUpdateAuser()] User with ID {id} not found.");
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception e)
            {
                _logger.LogError($"[AccountController AdminUpdateAuser()] Exception thrown while retrieving user with ID {id}: {e.Message}");
                return NotFound();
            }
        }

        // Displays a view for admins to manage listings.
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> _ManageListings()
        {
            return View(_listingRepository.GetAll().ToList());
        }

        // Allows admins to delete a listing using its ID.
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAlisting(int id)
        {
            try
            {
                var booked = _listingRepository.GetById(id).Result;
                await _listingRepository.Delete(id);
                _logger.LogInformation("[AccountController DeleteAlisting()] Listing with ID {id} deleted successfully.");
                return RedirectToAction(nameof(ManageAllBookings));
            }
            catch (Exception e)
            {
                _logger.LogError($"[AccountController DeleteAlisting()] Error deleting listing with ID {id}: {e.Message}");
                return NotFound(e);
            }
        }

        // Allows regular users to delete a listing using its ID.
        [Authorize]
        public async Task<IActionResult> DeleteAlistingNormalUser(int id)
        {
            try
            {
                var booked = _listingRepository.GetById(id).Result;
                await _listingRepository.Delete(id);
                _logger.LogInformation("[AccountController DeleteAlistingNormalUser()] Listing with ID {id} deleted by normal user.");
                return LocalRedirect("~/Identity/Account/Manage/Rentals");
            }
            catch (Exception e)
            {
                _logger.LogError($"[AccountController DeleteAlistingNormalUser()] Error deleting listing with ID {id} by normal user: {e.Message}");
                return NotFound(e);
            }
        }
    }
}