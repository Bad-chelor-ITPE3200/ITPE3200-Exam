using System.ComponentModel.DataAnnotations;

namespace FastFlat.Models
{
    public class LocationModel
    {
        [Key]
        public int LocationID { get; set; }
        public String Address { get; set; } = String.Empty;
        public String City { get; set; } = String.Empty;
        public String Country { get; set; } = String.Empty;


    }
}
