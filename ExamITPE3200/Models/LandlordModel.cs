using System.ComponentModel.DataAnnotations;

namespace ExamITPE3200.Models;

public class LandlordModel  /*: UserModel *///refactor, landlord != utleier??
{
    [Key]
    public int landlordModelId { get; set;  }
    
   public float rating { get; set; }
    

 /*   public landlordModel(int id, String username ,String password, String firstName, String lastName, String phone, String email,float rating) : base(id, username , password, firstName, lastName, phone, email)
    {
        this.rating = rating; 
    }*/
}