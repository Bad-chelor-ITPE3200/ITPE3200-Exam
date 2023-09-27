using FastFlat.Models;



namespace FastFlat.ViewModels
{
    public class RentalListViewModel
    {
        public IEnumerable<ListningModel> Rentals;
        public IEnumerable<BookingModel> Bookings;
        public string? CurrentViewName;

        public RentalListViewModel(IEnumerable<ListningModel> rentals, string? currentViewName) {
            Rentals = rentals;
            CurrentViewName = currentViewName;
        }
    }
}
