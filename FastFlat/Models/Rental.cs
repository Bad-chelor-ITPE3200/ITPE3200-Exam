namespace FastFlat.Models
{
    public class Rental
    {
        public int rentalId { get; set; }
        public virtual User user { get; set; } = default!;
        public string name { get; set; } = string.Empty;
        public string? description { get; set; }
        public string address { get; set; } = string.Empty;
        public double price { get; set; }
        public DateOnly fromDate { get; set; }
        public DateOnly toDate { get; set; }
        public string? imageURL { get; set; }
    }
}
