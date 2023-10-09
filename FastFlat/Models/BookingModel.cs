using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFlat.Models

{
    public class BookingModel
    {
        [Key] 
        public int BookingId { get; set; }
        public string UserId { get; set; }  // Dette er selve fremmednøkkelen som peker til 'Id' feltet i 'AspNetUsers' tabellen
        
        public virtual ApplicationUser User { get; set; }  // Dette er navigeringsegenskapen som lar deg navigere fra en 'Listning' til den tilknyttede 'User'

        public virtual UserModel Renter { get; set; }= default!; 
        public virtual ListningModel Property { get; set; } = default!;

        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal TotalPrice { get; set; } //endret fra price til TotalPrice
        public string? SpecialRequests { get; set; } = string.Empty;
    }
}
