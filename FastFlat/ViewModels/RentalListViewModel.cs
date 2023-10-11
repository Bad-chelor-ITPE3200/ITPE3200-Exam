using FastFlat.Models;



namespace FastFlat.ViewModels
{
    public class RentalListViewModel
    {
        public IEnumerable<ListningModel> Rentals;
        public IEnumerable<AmenityModel> Amenities;
        public string? CurrentViewName;

        public RentalListViewModel(IEnumerable<ListningModel> rentals, IEnumerable<AmenityModel> amenities, string? currentViewName) {
            Rentals = rentals;
            Amenities = amenities;
            CurrentViewName = currentViewName;

        }
    }
}
