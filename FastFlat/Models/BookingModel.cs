using System.ComponentModel.DataAnnotations;
namespace FastFlat.Models

{
    public class BookingModel
    {
        [Key] 
        public int BookingId { get; set; }
        public virtual AspNetUsers Renter { get; set; }= default!; 
        public virtual ListningModel Property { get; set; } = default!;

        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal TotalPrice { get; set; } //endret fra price til TotalPrice
        public string? SpecialRequests { get; set; } = string.Empty;
    }
}
