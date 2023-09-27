using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FastFlat.Models
{
    public class UserModel : IdentityUser
    {

        [Key] 
        public int UserModelId {  get; set; }

        public string? UserName { get; set; } = string.Empty; //already a part of Areas

        public string PassWord { get; set; } = string.Empty;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; } = string.Empty; 
        public string Phone { get; set; }

        public string? ProfilePicture { get; set; } = "wwwroot/images/profilepicture/standard.jpg";


        public virtual List<ListningModel> Rentals { get; set; } = default!;
        public virtual List<BookingModel> Bookings { get; set; } = default!;



        //public string Name { get; set; } = string.Empty;
    }
}
