
using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace FastFlat.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IRentalRepository<ListningModel> _listningRepository;
        private readonly IRentalRepository<BookingModel> _bookingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<AmenityModel> _amenityRepository;
        private readonly IRentalRepository<ListningAmenity> _listningAmenityRepository;
        private readonly IRentalRepository<ContryModel> _contryRepository;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IRentalRepository<ListningModel> listningRepository,
            IRentalRepository<BookingModel> bookingRepository, UserManager<ApplicationUser> userManager,
            IRentalRepository<AmenityModel> amenityRepository,
            IRentalRepository<ListningAmenity> listningAmenityRepository,IRentalRepository<ContryModel>contryRepository,ILogger<ProfileController> logger)
        {
            _listningRepository = listningRepository;
            _bookingRepository = bookingRepository;
            _userManager = userManager;
            _amenityRepository = amenityRepository;
            _contryRepository = contryRepository; 
            _listningAmenityRepository = listningAmenityRepository;
            _logger = logger;
        }

     
        

        [HttpGet]
        [Authorize]

        //henter alle Amenities fra DB og sender det til NewListningViewModel 
        public async Task<IActionResult> NewListning()
        {
            try
            {
                // Henter alle fasiliteter fra databasen ved å bruke _amenityRepository.
                var amenities = _amenityRepository.GetAll().ToList();
                // Oppretter en ny instans av NewListningViewModel med fasilitetene vi nettopp hentet.
                var availableCountries = _contryRepository.GetAll().ToList();
                // Vi konverterer amenities fra IEnumerable til List fordi NewListningViewModel forventer en List.
                var viewModel = new NewListningViewModel(amenities.ToList(), availableCountries.ToList());
                // Sender viewModel til View for å bli rendret til brukeren.
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Amedities have problems " + e);
                return NotFound(e); 
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewListning(NewListningViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        // Du kan logge feilen, skrive den ut i konsollen eller bare se den i debug-modus.
                        System.Diagnostics.Debug.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
                // Resten av koden din...
            }
            //ModelState.Remove("ListningAmenities");

            var userId = _userManager.GetUserId(User);


            if (ModelState.IsValid)
            {
                if (viewModel.ListningImage != null && viewModel.ListningImage.Length > 0)
                {
                    var fileName = Path.GetFileName(viewModel.ListningImage.FileName);

                    // Change this directory to the appropriate location where you want to save your images
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/listnings", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ListningImage.CopyToAsync(fileStream);
                    }

                    // Save the path to your database
                    viewModel.Listning.ListningImageURL = "/images/listnings/" + fileName;
                }



                viewModel.Listning.UserId = userId;
                //System.Diagnostics.Debug.WriteLine($"FromDate: {viewModel.fromDate}, ToDate: {viewModel.toDate}");



                await _listningRepository.Create(viewModel.Listning);

                // Sjekk at viewModel.Listning.ListningId nå har en gyldig verdi.
                if (viewModel.Listning.ListningId > 0)
                {
                    foreach (var amenityId in viewModel.SelectedAmenities)
                    {
                        var listningAmenity = new ListningAmenity
                        {
                            ListningId = viewModel.Listning.ListningId,
                            AmenityId = amenityId
                        };

                        await _listningAmenityRepository.Create(listningAmenity);


                    }

                    return Redirect("/Identity/Account/Manage/Rentals");
                }
                else
                {
                    _logger.LogError("Failed to create a new Listning in the database. User ID: {UserId}", userId);
                }
            }
            else
            {
                _logger.LogWarning("ModelState is invalid. Errors: {ModelStateErrors}",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            }

            // Hvis ModelState er ugyldig eller en annen feil oppstår, hent fasilitetene på nytt.
            var amenities = _amenityRepository.GetAll().ToList();
            viewModel.Amenities = amenities.ToList();

            return View(viewModel);
        }
        
        
    }
}

