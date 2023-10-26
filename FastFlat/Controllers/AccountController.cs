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
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Serilog.Data;
using Index = System.Index;


namespace FastFlat.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<BookingModel> _bookingrepository;
        private readonly ILogger<AccountController> _logger;
        private IRentalRepository<ListningModel> _listingRepository;
        private readonly IRentalRepository<ContryModel> _countryReposity;
        private readonly IRentalRepository<AmenityModel> _amenityModelRepository; 

        public AccountController(UserManager<ApplicationUser> userManager,
            IRentalRepository<BookingModel> bookingrepository, 
            IRentalRepository<ListningModel> listingRepository, IRentalRepository<ContryModel>countryReposity, IRentalRepository<AmenityModel>amenityModelRepository,ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _bookingrepository = bookingrepository;
            _listingRepository = listingRepository;
            _countryReposity = countryReposity;
            _amenityModelRepository = amenityModelRepository; 
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Account()
        {
            var users = await _userManager.Users.ToListAsync();
            _logger.LogInformation("Users are found");
            return View(users);
        }


        //uses the admin role to make sure that there are only admin that have acess to the veiws
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageAllBookings()
        {
            try
            {
                var bookings = _bookingrepository.GetAll().ToList(); //finds all boooking
                _logger.LogInformation("Bookings were found");
                return View(bookings);
            }
            catch (Exception e)
            {
                _logger.LogWarning("error found in finding users");
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> _AdminAccounts()
        {
            try
            {
                var users = _userManager.Users;
                _logger.LogInformation("users were found");
                return View(users);
            }
            catch (Exception e)
            {
                _logger.LogCritical("users were not found, error: " + e);
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            try
            {
                if (id > 0) throw new Exception("invalid id, lower than 0");
                var okDelete = await _bookingrepository.Delete(id);
                if (!okDelete)
                {
                    throw new Exception("invalid deletion");
                }
                else
                {
                    return RedirectToAction(nameof(ManageAllBookings));
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning("account could not be deleted due to errror: " + e);
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAdminAccount(string userId)
        {
            try
            {

                var user = _userManager.FindByIdAsync(userId).Result;
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(_AdminAccounts));

            }
            catch (Exception e)
            {
                _logger.LogWarning("Error in deleting user: " + e);
                return NotFound(e);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminUpdateAuser(ApplicationUser user)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("regexp invalid");
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
            try
            {
                var booked = _listingRepository.GetById(id).Result;
                await _listingRepository.Delete(id);
                _logger.LogInformation("listing deleted!");
                return RedirectToAction(nameof(ManageAllBookings));
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        [Authorize]
        public async Task<IActionResult> DeleteAlistingNormalUser(int id)
        {
            try
            {
                var booked = _listingRepository.GetById(id).Result;
                _logger.LogInformation(booked.ToString());
                await _listingRepository.Delete(id);
                _logger.LogInformation("listing deleted!");
                return LocalRedirect("~/Identity/Account/Manage/Rentals");
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
     
        }
        [Authorize]
        public async Task<IActionResult> _UpdateListing(int id)
        { 
            var findListing = _listingRepository.GetById(id).Result;

            NewListningViewModel listings = new NewListningViewModel();
            listings.Listning = findListing;
            listings.AvailableCountries = _countryReposity.GetAll().ToList();
            listings.Amenities =_amenityModelRepository.GetAll().ToList();
            return View(listings);
        }
        [Authorize]
        [HttpPost]
       public async Task<IActionResult> UpdateListing(NewListningViewModel LMM){ //id 0, should be 1, right id after routing 
            //todo: update the listing, we use standard account
            try
            {
                
                //modelstate not here, so we dont need to create a seperate validation
                //todo: the veiw model to update the user
                var upDatedUser = _listingRepository.GetById(LMM.Listning.ListningId).Result; //becomes null
                List<AmenityModel> amendities = _amenityModelRepository.GetAll().ToList();
                List<ContryModel> contries = _countryReposity.GetAll().ToList(); 
                NewListningViewModel listingVeiwModel = new NewListningViewModel(LMM.Listning,"_UpdateListing", LMM.ListningImage);
                upDatedUser.ListningName = listingVeiwModel.Listning.ListningName;
                upDatedUser.ListningDescription = listingVeiwModel.Listning.ListningDescription;
                upDatedUser.NoOfBeds = listingVeiwModel.Listning.NoOfBeds;
                //upDatedUser.ListningImageURL = listingVeiwModel.Listning.ListningImageURL;
                
                 // if it is updated
                
                
                     
                
                _logger.LogInformation(LMM.Listning.ListningImageURL); //URL to image == null -> maybe  with the form
                var fileName = Path.GetFileName(LMM.Listning.ListningImageURL);
               
                    if (fileName != null)
                    {
                      var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/listnings", fileName);
                        
                   
                   
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            _logger.LogInformation("image saved!");
                          //becomes null, cant be overwritten
                           await listingVeiwModel.ListningImage.CopyToAsync(fileStream);
                        } //why
                        LMM.Listning.ListningImageURL ="/images/listnings/" + fileName;
                       
                    }

                    upDatedUser.ListningImageURL = LMM.Listning.ListningImageURL; 
                     // Change this directory to the appropriate location where you want to save your images
                    
                     
                     // Save the path to your database
                    
                    //image: 
                   

                
                upDatedUser.FromDate = listingVeiwModel.Listning.FromDate; 
                upDatedUser.ToDate = listingVeiwModel.Listning.ToDate;
                upDatedUser.Location = listingVeiwModel.Listning.Location;
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
    }
}