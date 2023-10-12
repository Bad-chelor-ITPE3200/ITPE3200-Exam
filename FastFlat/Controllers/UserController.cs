using FastFlat.DAL;
using FastFlat.Models;
//using FastFlat.Views.shared;
using Microsoft.AspNetCore.Mvc;

namespace FastFlat.Controllers;

public class UserController : Controller
{
    private readonly IRentalRepository<ApplicationUser> _UserRepository;
    private readonly ILogger<UserController> _logger;

    public UserController(IRentalRepository<ApplicationUser> UserRepo, ILogger<UserController> logger)
    {
        _UserRepository = UserRepo;
        _logger = logger; 
    }
    


}