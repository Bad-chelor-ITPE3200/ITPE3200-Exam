using System.ComponentModel.DataAnnotations;

namespace exam_personal.Models;

public class UserModel
{
    [Key]
    private int UserModelId { get; set; }
    private String? username { get; set; }
    private String? password { get; set; } //of course this will be hashed
    private String? firstName { get; set;  }
    private String? lastName { get; set; }
    private String phone { get; set; }
    private String email { get; set;  }

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