using FastFlat.Models;

namespace FastFlat.ViewModels
{
    public class RentalListViewModel
    {
        public IEnumerable<ListningModel> Rentals;
        public IEnumerable<AmenityModel> Amenities;
        public List<String> SelectedAmenities { get; set; } // For å holde IDene til valgte amenities
        public string? CurrentViewName;

        public string? Location;
        public int Guests;
        public DateTime? ToDate;
        public DateTime? FromDate;

        public RentalListViewModel()
        {
            // Parameterless constructor for model binding
        }

        public RentalListViewModel(IEnumerable<ListningModel> rentals, IEnumerable<AmenityModel> amenities, List<string>? selectedAmenities, string? location, int? guests, DateTime? fromDate, DateTime? toDate, string? currentViewName)
        {
            Rentals = rentals;
            Amenities = amenities;
            SelectedAmenities = selectedAmenities;
            Location = location;
            Guests = guests ?? 1;

            // Handle fromDate
            if (!fromDate.HasValue || fromDate.Value.Year == 1)
            {
                FromDate = DateTime.Today;
            }
            else
            {
                FromDate = fromDate.Value.Date;
            }

            // Handle toDate
            if (!toDate.HasValue || toDate.Value.Date < FromDate || toDate.Value.Year == 1)
            {
                ToDate = FromDate.Value.AddDays(7);
            }
            else
            {
                ToDate = toDate.Value.Date;
            }

            CurrentViewName = currentViewName;
        }
    }
}
