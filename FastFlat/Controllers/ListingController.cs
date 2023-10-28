
using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers
{
    public class ListingController : Controller
    {
        private readonly IRentalRepository<ListningModel> _listningRepository;
        private readonly IRentalRepository<BookingModel> _bookingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<AmenityModel> _amenityRepository;
        private readonly IRentalRepository<ListningAmenity> _listningAmenityRepository;
        private readonly ILogger<ListingController> _logger;

        public ListingController(IRentalRepository<ListningModel> listningRepository,
            IRentalRepository<BookingModel> bookingRepository, UserManager<ApplicationUser> userManager,
            IRentalRepository<AmenityModel> amenityRepository,
            IRentalRepository<ListningAmenity> listningAmenityRepository, ILogger<ListingController> logger)
        {
            _listningRepository = listningRepository;
            _bookingRepository = bookingRepository;
            _userManager = userManager;
            _amenityRepository = amenityRepository;
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
                var amenities = await _amenityRepository.GetAll();
                // Oppretter en ny instans av NewListningViewModel med fasilitetene vi nettopp hentet.

                // Vi konverterer amenities fra IEnumerable til List fordi NewListningViewModel forventer en List.
                var viewModel = new NewListningViewModel(amenities.ToList());
                _logger.LogInformation("[NewListningController NewListning() GET] Successfully retrieved amenities and available countries.");
                // Sender viewModel til View for å bli rendret til brukeren.
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"[NewListningController NewListning() GET] Error retrieving amenities and available countries: {e.Message}");
                return NotFound(e);
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewListning(NewListningViewModel viewModel)
        {
            var userId = _userManager.GetUserId(User);
           
            _logger.LogInformation($"ListningLat: {viewModel.Listning.ListningLat}, Type: {viewModel.Listning.ListningLat?.GetType()}");
            _logger.LogInformation($"ListningLng: {viewModel.Listning.ListningLng}, Type: {viewModel.Listning.ListningLng?.GetType()}");
            _logger.LogInformation($"Received lat and lng: {viewModel.Listning.ListningLat}, {viewModel.Listning.ListningLng}");
            if (ModelState.IsValid)
            {
                if (viewModel.ListningImage != null && viewModel.ListningImage.Length > 0)
                {
                    var fileName = Path.GetFileName(viewModel.ListningImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/listnings", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ListningImage.CopyToAsync(fileStream);
                    }

                    // Save the path to your database
                    viewModel.Listning.ListningImageURL = "/images/listnings/" + fileName;
                }
                viewModel.Listning.UserId = userId;
                await _listningRepository.Create(viewModel.Listning);

                // Sjekk at viewModel.Listning.ListningId nå har en gyldig verdi.
                if (viewModel.Listning.ListningId > 0)
                {
                    if (viewModel.SelectedAmenities != null)
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
                    }
                    else
                    {
                        _logger.LogWarning("[NewListningController NewListning() POST] SelectedAmenities is null.");
                    }

                    _logger.LogInformation($"[NewListningController NewListning() POST] Successfully created new listing with ID {viewModel.Listning.ListningId} by user ID {userId}.");
                    return Redirect("/Identity/Account/Manage/Rentals");
                }
                else
                {
                    _logger.LogError($"[NewListningController NewListning() POST] Failed to create a new Listning in the database.User ID: { userId}.");
                    return StatusCode(500, "Failed to create new listing.");
                }
            }
            else
            {
                _logger.LogWarning("[NewListningController NewListning() POST] ModelState is invalid.\"",
                    ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

                // Logge ugyldige nøkler og feilmeldinger
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        var errors = string.Join(", ", state.Errors.Select(e => e.ErrorMessage));
                        _logger.LogWarning($"Key: {key}, Errors: {errors}");
                    }
                }
            }

            // Hvis ModelState er ugyldig eller en annen feil oppstår, hent fasilitetene på nytt.
            var amenityList = await _amenityRepository.GetAll();
            viewModel.Amenities = amenityList.ToList();

            return View(viewModel);
        }
    }
}

