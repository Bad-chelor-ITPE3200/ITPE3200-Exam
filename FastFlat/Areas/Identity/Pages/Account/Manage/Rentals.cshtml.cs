using FastFlat.Controllers;
using FastFlat.DAL;
using FastFlat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.Areas.Identity.Pages.Account.Manage
{
    public class RentalsModel : PageModel
    {

        private readonly IRentalRepository<ListningModel> _listningRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<AmenityModel> _amenityRepository;
        private readonly IRentalRepository<ListningAmenity> _listningAmenityRepository;
        private readonly ILogger<ListingController> _logger;

        public RentalsModel(IRentalRepository<ListningModel> listningRepository,
            UserManager<ApplicationUser> userManager,
            IRentalRepository<AmenityModel> amenityRepository,
            IRentalRepository<ListningAmenity> listningAmenityRepository, ILogger<ListingController> logger, IEnumerable<ListningModel> rentals)
        {
            _listningRepository = listningRepository;
            _userManager = userManager;
            _amenityRepository = amenityRepository;
            _listningAmenityRepository = listningAmenityRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Rentals = rentals;
        }

        //--------------------------


        public IEnumerable<ListningModel> Rentals { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var userRentalsWithAmenities = (await _listningRepository.GetAll())
                .AsQueryable() // Konverterer til IQueryable<T>
                .Include(l => l.ListningAmenities)!
    .ThenInclude(la => la.Amenity)
    .Where(r => r.UserId == user.Id)
    .ToList();
            Rentals = userRentalsWithAmenities;
        }

    }
}