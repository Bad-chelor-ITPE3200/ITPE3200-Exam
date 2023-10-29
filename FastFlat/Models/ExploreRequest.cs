namespace FastFlat.Models
{
    public class ExploreRequest
    {
        public string? Location { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Guests { get; set; }
        public string Amenities { get; set; } = default!;
    }
}
