using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Jeg har gjort alle atributer null for å teste CRUD

namespace FastFlat.Models
{
    public class ListningModel
    {
        [Key]
        public int ListningId { get; set; }
        [ForeignKey("User")]  // Dette indikerer at 'UserId' er fremmednøkkelen for 'User' navigeringsegenskapen
        public string? UserId { get; set; }  // Dette er selve fremmednøkkelen som peker til 'Id' feltet i 'AspNetUsers' tabellen

        public virtual ApplicationUser? User { get; set; }  // Dette er navigeringsegenskapen som lar deg navigere fra en 'Listning' til den tilknyttede 'User'

        [RegularExpression(@"^[0-9a-zæøåA-ZÆØÅ. /-]{10,60}", ErrorMessage = "The Listing Name needs to be between 4 and 20 signs and letters from a - å")]
        public string ListningName { get; set; } = string.Empty;

        [StringLength(350)]
        public string? ListningDescription { get; set; }
        [RegularExpression(@"^[0-9. /-]{1,10}", ErrorMessage = "You can only have numbers between 0 and 9, and 10 letters")]
        public int? NoOfBeds { get; set; }
        [RegularExpression(@"^[0-9. /-]{1,10}", ErrorMessage = "You can only have numbers between 0 and 9, and 10 letters")]
        public int? SquareMeter { get; set; }
        public float? Rating { get; set; }
        public string? ListningAddress { get; set; }
        public string? ListningCity { get; set; }
        public string? ListningCountry { get; set; }

        public string? ListningLat { get; set; }
        public string? ListningLng { get; set; }
        public decimal ListningPrice { get; set; }

        //public virtual CityModel City { get; set; }//legger til city
        //public virtual LocationModel? Location { get; set; } = default!;

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ListningImageURL { get; set; }

        public virtual List<BookingModel>? bookings { get; set; }


        public virtual List<ListningAmenity>? ListningAmenities { get; set; } = default!;


        //public virtual ICollection<AmenityModel> Amenities { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
