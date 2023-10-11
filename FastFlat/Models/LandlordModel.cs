using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FastFlat.Models;

public class LandlordModel
{
    //use the landlordID -> standard userID
    public string landlordModelId { get; set;  } //userManager
    
   public float rating { get; set; }
   
   public virtual List<ApplicationUser>? users { get; set; }




   /*   public landlordModel(int id, String username ,String password, String firstName, String lastName, String phone, String email,float rating) : base(id, username , password, firstName, lastName, phone, email)
      {
          this.rating = rating;
      }*/
}