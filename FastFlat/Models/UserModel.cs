using System.ComponentModel.DataAnnotations;
namespace FastFlat.Models
{
    public class UserModel
    {

        [Key]
        public int UserModelId { get; set; }

        public string? Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; } = string.Empty;
        public int? Phone { get; set; }

        public string? ProfilePicture { get; set; } = "wwwroot/images/profilepicture/standard.jpg";


        public virtual List<ListningModel> Rentals { get; set; } = default!;
        public virtual List<BookingModel> Bookings { get; set; } = default!;



        //public string Name { get; set; } = string.Empty;
    }
}
