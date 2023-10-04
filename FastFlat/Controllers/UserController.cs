using FastFlat.DAL;
using FastFlat.Models;
using FastFlat.Views.shared;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers;

public class UserController : Controller
{
    private readonly IRentalRepository<AspNetUsers> _UserRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(IRentalRepository<AspNetUsers> UserRepo, ILogger<UserController> logger)
    {
        _UserRepository = UserRepo;
        _logger = logger; 
    }
    

    public async Task<IActionResult> landing(AspNetUsers aspNetUsers)
    {
        var account = await _UserRepository.GetById(aspNetUsers.UserModelId);
        if (account != null)
        {
            return View(landing);
        }

        return null; 
    }
}