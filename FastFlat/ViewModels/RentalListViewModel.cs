using FastFlat.Models;



namespace FastFlat.ViewModels
{
    public class RentalListViewModel
    {
        public IEnumerable<ListningModel> Rentals;
        public IEnumerable<AmenityModel> Amenities;
        public List<String> SelectedAmenities { get; set; } // For å holde IDene til valgte amenities
        public string? CurrentViewName;

        public RentalListViewModel()
        {
            // Parameterless constructor for model binding
        }

        public RentalListViewModel(IEnumerable<ListningModel> rentals, IEnumerable<AmenityModel> amenities, string? currentViewName)
        {
            Rentals = rentals;
            Amenities = amenities;
            CurrentViewName = currentViewName;

        }
    }
}
