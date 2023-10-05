using System.ComponentModel.DataAnnotations;
namespace FastFlat.Models
{
    public class AmenityModel
    {
        [Key]
        public int AmenitylId { get; set; }

        public string? AmenityName { get; set; }
        public string? AmenityDescription { get; set; }

        public string? AmenityLogo { get; set; }

        //public virtual List<ListningModel>? Listnings { get; set; } = default!;

    }
}
