using System.Collections;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FastFlat.Attributes;

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

        public string ListningName { get; set; } = string.Empty;
        public string? ListningDescription { get; set; }
        public int? NoOfBeds { get; set; }
        public int? SquareMeter { get; set; }
        public float? Rating { get; set; }
        public string? ListningAddress { get; set; } = string.Empty;
        public decimal ListningPrice { get; set; }

        //public virtual CityModel City { get; set; }//legger til city
        public virtual LocationModel? Location { get; set; } = default!;

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
