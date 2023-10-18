using FastFlat.Controllers;
using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FastFlat.Areas.Identity.Pages.Account.Manage
{
    public class RentalsModel : PageModel
    {

        private readonly IRentalRepository<ListningModel> _listningRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRentalRepository<AmenityModel> _amenityRepository;
        private readonly IRentalRepository<ListningAmenity> _listningAmenityRepository;
        private readonly ILogger<ProfileController> _logger;

        public RentalsModel(IRentalRepository<ListningModel> listningRepository, 
            UserManager<ApplicationUser> userManager,
            IRentalRepository<AmenityModel> amenityRepository,
            IRentalRepository<ListningAmenity> listningAmenityRepository, ILogger<ProfileController> logger)
        {
            _listningRepository = listningRepository;
            _userManager = userManager;
            _amenityRepository = amenityRepository;
            _listningAmenityRepository = listningAmenityRepository;
            _logger = logger;
        }

        //--------------------------


        public IEnumerable<ListningModel> Rentals { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var userRentalsWithAmenities = _listningRepository.GetAll()
                .Include(l => l.ListningAmenities)
                .ThenInclude(la => la.Amenity)
                .Where(r => r.UserId == user.Id)
                .ToList();
            Rentals = userRentalsWithAmenities;
        }

        
    }
}