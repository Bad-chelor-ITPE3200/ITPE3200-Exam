using System.ComponentModel.DataAnnotations;
namespace FastFlat.Models
{
    public class AmenityModel
    {
        [Key]
        public int AmenityId { get; set; }

        public string? AmenityName { get; set; }
        public string? AmenityDescription { get; set; }

        public string? AmenityLogo { get; set; }

        public virtual ICollection<ListningAmenity> ListningAmenities { get; set; } = new List<ListningAmenity>();



    }
}
