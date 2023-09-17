using System.Reflection.Metadata.Ecma335;
using exam_personal.Models;
using ExamITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExamITPE3200.Controllers;

public class ListingController : Controller
{
    private readonly FastFlatDbContext _fastFlatDbContext;

   
    public ListingController(FastFlatDbContext fastFlatDbContext)
    {
        fastFlatDbContext = _fastFlatDbContext; 
    }
   /* public IActionResult Index()
    {
       //return View(Index); 
    }*/
    //if we got time, before saving 
    [HttpPost]
    public void validateListing(ListingModel Listing)
    {
       /* List<ListingModel> all = FastFlatDbContext.Listing.ToList();
        FastFlatDbContext*/ 
    }

    public List<ListingModel> getAllListings()
    {
        var all = _fastFlatDbContext.Listings.ToList();
        return all; 
    }
    public void createListing(ListingModel listing)
    {
        
    }
}