using System.ComponentModel.DataAnnotations;

namespace exam_personal.Models;

public class landlordModel  /*: UserModel*/ //refactor, landlord != utleier??
{
    
    private int landlordModelId { get; set;  }
    
    private float rating { get; set; }
    

 /*   public landlordModel(int id, String username ,String password, String firstName, String lastName, String phone, String email,float rating) : base(id, username , password, firstName, lastName, phone, email)
    {
        this.rating = rating; 
    }*/
}