using System.Reflection.Metadata.Ecma335;
using ExamITPE3200.Models;
using ExamITPE3200.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace ExamITPE3200.Controllers;

public class ListingController : Controller
{
    private readonly FastFlatDbContext _FastFlatDbContext;

   
    public ListingController(FastFlatDbContext FastFlatDbContext)
    {
        _FastFlatDbContext = FastFlatDbContext; 
    }
    
    
    //if we got time, before saving 
    [HttpPost]
    public void validateListing(ListingModel Listing)
    {
       /* List<ListingModel> all = FastFlatDbContext.Listing.ToList();
        FastFlatDbContext*/ 
    }
    [HttpGet]
    public List<ListingModel> GetAllListings()
    {
        var all = _FastFlatDbContext.Listings.ToList(); //find all listings
        return all; 
    }
    [HttpPost]
    public void CreateListing(ListingModel listing)
    {
        //create
        if (ModelState.IsValid)
        {
            _FastFlatDbContext.Listings.Add(listing);
            _FastFlatDbContext.SaveChanges();
        }
       
    }
    

    public void UpdateListing(int id)
    {
        var listingUpdate = _FastFlatDbContext.Listings.Find(id); 
        //reference to the update form for the listing
    }
    [HttpPost] //can be changed to post
    public void DeleteListing(int id)
    {
        var listingdelete = _FastFlatDbContext.Listings.Find(id);
        _FastFlatDbContext.Remove(listingdelete); 
    }
}