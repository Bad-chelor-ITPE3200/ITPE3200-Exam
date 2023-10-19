using Microsoft.AspNetCore.Identity;

namespace FastFlat.ViewModels;

public class ApplicationUserVeiwModel : IdentityUser
{
    public string UserName { get; set; }
   public string FirstName { get; set; }
public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set;  }
     public byte[]   ProfilePicture { get; set; }
     public string? _currentViewName;

     public ApplicationUserVeiwModel(string userName, string firstName, string lastName, string email, string phoneNumber, byte [] profilePicture, string currentViewName)
     {
         UserName = userName;
         FirstName = firstName;
         LastName = lastName;
         Email = email;
         PhoneNumber = phoneNumber;
         ProfilePicture = profilePicture;
         _currentViewName = currentViewName;
     }
}