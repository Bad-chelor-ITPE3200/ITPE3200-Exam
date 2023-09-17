using System.ComponentModel.DataAnnotations;

namespace exam_personal.Models;

public class UserModel
{
    [Key]
    public int UserModelId { get; set; }
    public string? username { get; set; }
    public string? password { get; set; } //of course this will be hashed
    public string? firstName { get; set;  }
    public string? lastName { get; set; }
    public string? phone { get; set; }
    public string? email { get; set;  }

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