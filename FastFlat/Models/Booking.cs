namespace FastFlat.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public virtual User Renter { get; set; }= default!; 
        public virtual Rental Property { get; set; } = default!;
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public decimal Price { get; set; }
        public string? SpecialRequests { get; set; }
    }
}
