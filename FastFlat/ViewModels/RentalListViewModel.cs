using FastFlat.Models;



namespace FastFlat.ViewModels
{
    public class RentalListViewModel
    {
        public IEnumerable<Rental> Rentals;
        public string? CurrentViewName;

        public RentalListViewModel(IEnumerable<Rental> rentals, string? currentViewName) {
            Rentals = rentals;
            CurrentViewName = currentViewName;
        }
    }
}
