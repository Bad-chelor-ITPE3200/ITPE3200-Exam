using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser : IdentityUser
{
    [RegularExpression(@"^[a-zA-ZæøåÆØÅ. \-]{2,20}$",
        ErrorMessage = "invalid first name, must be between 2 and 20 letters and in the range of A-Å")]
    public string FirstName { get; set; }

    [RegularExpression(@"^[a-zA-ZæøåÆØÅ. /-]{2,20}$",
        ErrorMessage = "invalid last name, must be between 2 and 20 letters and in the range of A-Å")]
    public string LastName { get; set; }

    public int UsernameChangeLimit { get; set; } = 10;
    public byte[] ProfilePicture { get; set; }
}