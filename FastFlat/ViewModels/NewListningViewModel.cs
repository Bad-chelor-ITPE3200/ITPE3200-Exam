using FastFlat.Models;

namespace FastFlat.ViewModels
{
    public class NewListningViewModel
    {        

        public ListningModel Listning { get; set; }

        public List<AmenityModel>? Amenities { get; set; }
        public List<int> SelectedAmenities { get; set; } // For å holde IDene til valgte amenities

        // Parameterløs konstruktør
        public NewListningViewModel()
        {
        }

        // Konstruktør for NewListningViewModel.
        public NewListningViewModel (List<AmenityModel> amenities)
        {
            // Setter Amenities-property med den mottatte listen eller en ny tom liste hvis den mottatte listen er null.
            Amenities = amenities ?? new List<AmenityModel>();


            // Initialiserer SelectedAmenities som en tom liste.
            // Denne vil bli fylt ut basert på brukerens valg i brukergrensesnittet.
            SelectedAmenities = new List<int>();
        }

    }
}
