using System.ComponentModel.DataAnnotations;
namespace FastFlat.Models
{
    public class ListningModel
    {
        [Key]
        public int ListningId { get; set; }
        public virtual UserModel user { get; set; } = default!;
        public string ListningName { get; set; } = string.Empty;
        public string? ListningDescription { get; set; }
        public int NoOfBeds { get; set; }
        public int SquareMeter { get; set; }
        public float Rating { get; set; }
        public string ListningAddress { get; set; } = string.Empty;
        public double ListningPrice { get; set; }

        //public virtual CityModel City { get; set; }//legger til city
        public virtual LocationModel? Location { get; set; } = default!;

        public DateOnly fromDate { get; set; }
        public DateOnly toDate { get; set; }
        public string? ListningImageURL { get; set; }

        public virtual List<BookingModel>? bookings { get; set; }

        public virtual List<AmenityModel>? Amenities { get; set; }

        //public virtual ICollection<AmenityModel> Amenities { get; set; }
    }
}
