using FastFlat.Models;
using Microsoft.AspNetCore.Identity;

namespace FastFlat.ViewModels
{
    public class ListingViewModel
    {
        public ListningModel Listing { get; set; } = default!;
        public ApplicationUser User { get; set; }

        public string? _currentViewName;

        public ListingViewModel(ListningModel listing, ApplicationUser user, string? currentViewName)
        {
            Listing = listing;
            User = user;
            _currentViewName = currentViewName;
        }
    }
}
