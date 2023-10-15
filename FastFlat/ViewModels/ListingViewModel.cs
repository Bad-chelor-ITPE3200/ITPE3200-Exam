using FastFlat.Models;

namespace FastFlat.ViewModels
{
    public class ListingViewModel
    {
        public ListningModel Listing { get; set; } = default!;
        public ApplicationUser? User { get; set; }

        public BookingModel Booking { get; set; }

       
        public string? _currentViewName;

        public ListingViewModel()
        {
        }

        public ListingViewModel(ListningModel listing, ApplicationUser user, string? currentViewName)
        {
            Listing = listing;
            User = user;

            _currentViewName = currentViewName;
        }
    }
}
