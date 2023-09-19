using System.ComponentModel.DataAnnotations;

namespace ExamITPE3200.Models;

public class UserModel
{
    [Key]
    public int UserModelId { get; set; }
    //regexp username
    public string? username { get; set; }
    //regexp password
    public string? password { get; set; } //of course this will be hashed
    //regexp firstname
    public string? firstName { get; set;  }
    //regexp lastname
    public string? lastName { get; set; }
    //regexp phone
    public string? phone { get; set; }
    //regexp email
    public string? email { get; set;  }

    public string? profilePicture { get; set; } = "wwwroot/UserImages/standard.jpg";

    /* public UserModel(int id, String username, String password, String firstName, String lastName, String phone, String email )
     {
         this.id = id;
         this.username = username;
         this.password = password;
         this.firstName = firstName;
         this.lastName = lastName;
         this.phone = phone;
         this.email = email;
     }*/
}