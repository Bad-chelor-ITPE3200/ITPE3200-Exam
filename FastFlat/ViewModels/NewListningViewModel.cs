using FastFlat.Attributes;
using FastFlat.Models;

namespace FastFlat.ViewModels
{
    public class NewListningViewModel
    {
        public IFormFile ListningImage { get; set; }
        public ListningModel Listning { get; set; }

        public List<AmenityModel>? Amenities { get; set; }
        public String? CurrentVeiwName { get; set;  }
        public IEnumerable<ContryModel>? AvailableCountries { get; set; }

        [MinItems(1, ErrorMessage = "Vennligst velg minst en amenity.")]
        public List<int>? SelectedAmenities { get; set; } // For å holde IDene til valgte amenities


        // Parameterløs konstruktør
        public NewListningViewModel()
        {
        }

        // Konstruktør for NewListningViewModel.
        public NewListningViewModel(List<AmenityModel> amenities, List<ContryModel> contries)
        {
            // Setter Amenities-property med den mottatte listen eller en ny tom liste hvis den mottatte listen er null.
            Amenities = amenities ?? new List<AmenityModel>();
            AvailableCountries = contries ?? new List<ContryModel>();


            // Initialiserer SelectedAmenities som en tom liste.
            // Denne vil bli fylt ut basert på brukerens valg i brukergrensesnittet.
            SelectedAmenities = new List<int>();
        }

        public NewListningViewModel(List<AmenityModel> amenities, List<ContryModel> contries, 
            String?  _currentVeiwName)
        {
            amenities = amenities ?? new List<AmenityModel>();
            AvailableCountries = contries ?? new List<ContryModel>();
            CurrentVeiwName = _currentVeiwName;
        }
    }
}