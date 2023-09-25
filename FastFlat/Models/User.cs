namespace FastFlat.Models
{
    public class User
    {
        public int Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;  
        public int? Phone { get; set; }
        public string Password { get; set; } = string.Empty;
        public virtual List<Rental> Rentals { get; set; } = default!;
        public virtual List<Booking> Bookings { get; set; } = default!;
        public string? ImageUrl { get; set; } = string.Empty;
    }
}
