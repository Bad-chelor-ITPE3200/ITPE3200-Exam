using System.ComponentModel.DataAnnotations;
namespace FastFlat.Models

{
    public class ListningAmenity
    {

        [Key]
        public int ListningAmenityId { get; set; }

        public int ListningId { get; set; }
        public virtual ListningModel? Listning { get; set; }

        public int AmenityId { get; set; }
        public virtual AmenityModel? Amenity { get; set; }
    }
}
