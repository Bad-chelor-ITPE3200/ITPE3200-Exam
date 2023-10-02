using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.Views.shared;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers;

public class UserController : Controller
{
    private readonly IRentalRepository<UserModel> _UserRepository;

    public UserController(IRentalRepository<UserModel> UserRepo)
    {
        _UserRepository = UserRepo; 
    }
    

    public async Task<IActionResult> landing(UserModel user)
    {
        if (user.UserModelId > 0) 
        {
            
        }
        var account = await _UserRepository.GetById(user.UserModelId);
        if ()
        {
            
        }
    }
}