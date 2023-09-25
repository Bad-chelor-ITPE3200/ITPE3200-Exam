
using ExamITPE3200.DAL;
using ExamITPE3200.Models;
using ExamITPE3200.Views.Home.User; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExamITPE3200.Controllers;

public class UserController
{ 
    //database
private readonly FastFlatDbContext _fastFlatDbContext;

public UserController(FastFlatDbContext db){
   db = _fastFlatDbContext;
}

/*public IActionResult login(UserModel user)
{

}*/
    /*public IActionResult CreateUser()
    {
     //   var createUsermodel = new 
        //return View(CreateUser); 
    }*/
public void newUser(UserModel user)
{
    _fastFlatDbContext.Users.Add(user);
    _fastFlatDbContext.SaveChanges(); 
}
    
    public void validateUser()
    {
        //to validate the user as a whole ok? -> send session 
    }

    public bool validateUsername(String Useersame)
    {
        //validate username
        return false; 
    }
    public bool validatePassword(String Useersame)
    {
        //takes the unhashed pw -> hash it and checks it in the database
        return false; 
    }
    
}