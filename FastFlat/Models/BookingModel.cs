using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFlat.Models

{
    public class BookingModel
    {
        [Key]
        public int BookingId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }  // Fremmednøkkel for User
        public int ListningId { get; set; } // Fremmednøkkel for Listning
        public virtual ListningModel? Listning { get; set; } // Navigasjonsegenskap for Listning

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? SpecialRequests { get; set; } = string.Empty;
    }
}
