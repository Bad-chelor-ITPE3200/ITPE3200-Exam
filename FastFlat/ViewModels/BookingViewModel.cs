using FastFlat.Models;

namespace FastFlat.ViewModels
{
    public class BookingViewModel
    {
        public ListningModel Listing { get; set; } = default!;
        public ApplicationUser User { get; set; }

        /*

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
        */

        public BookingModel Booking { get; set; }

        public string? _currentViewName;

        public BookingViewModel(ListningModel listing, ApplicationUser user, BookingModel booking, string? currentViewName)
        {
            Listing = listing;
            User = user;
            Booking = booking;
            _currentViewName = currentViewName;
        }
    }
}
