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
    public string? renterId { get; set; }
    public string? landlordId { get; set; }
    
}