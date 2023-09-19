
using ExamITPE3200.DAL;
using ExamITPE3200.Models;

namespace ExamITPE3200.Controllers;

public class UserController
{ 
    //database
private readonly FastFlatDbContext _fastFlatDbContext;

public UserController(FastFlatDbContext db){
   db = _fastFlatDbContext;
}

public void createuser(UserModel user)
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