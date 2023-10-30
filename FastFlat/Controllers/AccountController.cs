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
using Serilog.Data;
using Index = System.Index;


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
            IRentalRepository<ApplicationUser> applicationUserRepostiory, IRentalRepository<ListningModel>listingRepository,ILogger<AccountController> logger)
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
                var bookings = (await _bookingrepository.GetAll()).ToList(); //finds all boooking
                _logger.LogInformation("[AccountController ManageAllBookings()] Bookings retrieved successfully.");
                return View(bookings);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"[AccountController ManageAllBookings()] Error retrieving bookings: {e.Message}");
                return NotFound();
            }
        }

        // Displays a view with all admin accounts for admins
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> _AdminAccounts() //todo: async?
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                _logger.LogInformation("[AccountController _AdminAccounts()] Users retrieved successfully");
                return View(users);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"[AccountController _AdminAccounts()] Error retrieving users: {e.Message}");
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
                if (user.LastName != null && user.FirstName != null && user.ProfilePicture != null)
                {
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
                            _logger.LogInformation($"[AccountController AdminUpdateAuser()] User with email {user.Email} updated successfully.");
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

            return null!; 
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
            return View((await _listingRepository.GetAll()).ToList());
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
                _logger.LogInformation($"[AccountController DeleteAlisting()] Listing with ID {id} deleted successfully.");
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
                _logger.LogInformation(booked.ToString());
                await _listingRepository.Delete(id);
                _logger.LogInformation($"[AccountController DeleteAlistingNormalUser()] Listing with ID {id} deleted by normal user.");
                return LocalRedirect("~/Identity/Account/Manage/Rentals");
            }
            catch (Exception e)
            {
                _logger.LogError($"[AccountController DeleteAlistingNormalUser()] Error deleting listing with ID {id} by normal user: {e.Message}");
                return NotFound(e);
            }
        }

        [Authorize]
        public async Task<IActionResult> _UpdateListing(int id)
        {
            var findListing = await _listingRepository.GetById(id);

            NewListningViewModel listings = new NewListningViewModel();
            listings.Listning = findListing;
            return View(listings);
        }

       
        [HttpPost]
        [Authorize(Roles = "Admin")]
       public async Task<IActionResult> UpdateListingAdmin(NewListningViewModel LMM)
        {
            //id 0, should be 1, right id after routing 
            
            try
            {
                //modelstate not here, so we dont need to create a seperate validation, alts
                
                var upDatedUser = _listingRepository.GetById(LMM.Listning.ListningId).Result; 

            

                _logger.LogInformation("URLen til bilde er: " + LMM.Listning.ListningImageURL);
                var fileName = Path.GetFileName(LMM.ListningImage.FileName);

                _logger.LogInformation("Ayy Lmao " + fileName);
                if (fileName != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/listnings", fileName);


                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        _logger.LogInformation("a " + fileStream.Name + " " + fileName.Length);
                        _logger.LogInformation(
                            $"The filStream object itself is {(fileStream == null ? "null" : "not null")}");
                          if (fileStream != null)
                        {
                            _logger.LogInformation(
                                $"The LMM.ListningImage object is {(LMM.ListningImage == null ? "null" : "not null")}");
                            await LMM.ListningImage!.CopyToAsync(fileStream); 
                        }
                    } 

                    LMM.Listning.ListningImageURL = "/images/listnings/" + fileName;
                }
                
                NewListningViewModel listingVeiwModel =
                    new NewListningViewModel(LMM.Listning, "_UpdateListing", LMM.ListningImage);
                upDatedUser.ListningName = listingVeiwModel.Listning.ListningName;
                upDatedUser.ListningDescription = listingVeiwModel.Listning.ListningDescription;
                upDatedUser.NoOfBeds = listingVeiwModel.Listning.NoOfBeds;
                upDatedUser.ListningImageURL = LMM.Listning.ListningImageURL;

                // if it is updated


                if (LMM.Listning != null)
                {
                    _logger.LogInformation(LMM.Listning
                        .ListningImageURL); //URL to image == null -> maybe  with the form


                    // upDatedUser.ListningImageURL = listingVeiwModel.Listning.ListningImageURL;
                    // Change this directory to the appropriate location where you want to save your images


                    // Save the path to your database

                    //image: 


                    upDatedUser.FromDate = listingVeiwModel.Listning.FromDate;
                    upDatedUser.ToDate = listingVeiwModel.Listning.ToDate;
                    upDatedUser.ListningAddress = listingVeiwModel.Listning.ListningAddress;
                    upDatedUser.ListningLat = listingVeiwModel.Listning.ListningLat;
                    upDatedUser.ListningLng = listingVeiwModel.Listning.ListningLng;
                }

                _logger.LogInformation("LOGGGING OK");
                var result = _listingRepository.Update(upDatedUser);
                //  _logger.LogInformation(restult.ToString());


                return LocalRedirect("~/Identity/Account/Manage/Rentals");
            }
            catch (Exception e)
            {
                _logger.LogCritical("error in updating user:  \n  " + e);
                return NotFound(e);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateListing(NewListningViewModel LMM)
        {
            //id 0, should be 1, right id after routing 
            //todo: update the listing, we use standard account
            try
            {
                //modelstate not here, so we dont need to create a seperate validation, alts
                //todo: the veiw model to update the user
                var upDatedUser = _listingRepository.GetById(LMM.Listning.ListningId).Result; //becomes null
                

                _logger.LogInformation("URLen til bilde er: " + LMM.Listning.ListningImageURL);
                var fileName = Path.GetFileName(LMM.ListningImage.FileName);

                _logger.LogInformation("Ayy Lmao " + fileName);
                if (fileName != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/listnings", fileName);


                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        _logger.LogInformation("a " + fileStream.Name + " " + fileName.Length);
                        _logger.LogInformation(
                            $"The filStream object itself is {(fileStream == null ? "null" : "not null")}");
                        //becomes null, cant be overwritten
                        //  listingVeiwModel.ListningImage.OpenReadStream();
                        //_logger.LogInformation("c"+  LMM.ListningImage.ToString());
                        if (fileStream != null)
                        {
                            _logger.LogInformation(
                                $"The LMM.ListningImage object is {(LMM.ListningImage == null ? "null" : "not null")}");
                            await LMM.ListningImage!.CopyToAsync(fileStream); //still crashes here tb
                        }
                    } //why

                    LMM.Listning.ListningImageURL = "/images/listnings/" + fileName;
                }

                _logger.LogInformation("Yo mama" + LMM.Listning.ListningImageURL);
                NewListningViewModel listingVeiwModel =
                    new NewListningViewModel(LMM.Listning, "_UpdateListing", LMM.ListningImage);
                upDatedUser.ListningName = listingVeiwModel.Listning.ListningName;
                upDatedUser.ListningDescription = listingVeiwModel.Listning.ListningDescription;
                upDatedUser.NoOfBeds = listingVeiwModel.Listning.NoOfBeds;
                upDatedUser.ListningImageURL = LMM.Listning.ListningImageURL;

                // if it is updated


                _logger.LogInformation(LMM.Listning
                    .ListningImageURL); //URL to image == null -> maybe  with the form


                // upDatedUser.ListningImageURL = listingVeiwModel.Listning.ListningImageURL;
                // Change this directory to the appropriate location where you want to save your images


                // Save the path to your database

                //image: 


                upDatedUser.FromDate = listingVeiwModel.Listning.FromDate;
                upDatedUser.ToDate = listingVeiwModel.Listning.ToDate;
                upDatedUser.ListningAddress = listingVeiwModel.Listning.ListningAddress;
                upDatedUser.ListningLat = listingVeiwModel.Listning.ListningLat;
                upDatedUser.ListningLng = listingVeiwModel.Listning.ListningLng; 
                _logger.LogInformation("LOGGGING OK");
                var result = _listingRepository.Update(upDatedUser);
                //  _logger.LogInformation(restult.ToString());

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction(nameof(_ManageListings));
                }
                else
                {  return LocalRedirect("~/Identity/Account/Manage/Rentals");
                }
              
            }
            catch (Exception e)
            {
                _logger.LogCritical("error in updating user:  \n  " + e);
                return NotFound(e);
            }
        }
    }
}