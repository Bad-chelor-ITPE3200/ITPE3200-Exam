using System.Reflection.Metadata.Ecma335;
using exam_personal.Models;
using ExamITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExamITPE3200.Controllers;

public class ListingController : Controller
{
    private readonly FastFlatDbContext _FastFlatDbContext;

   
    public ListingController(FastFlatDbContext FastFlatDbContext)
    {
        FastFlatDbContext = _FastFlatDbContext; 
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
    [HttpDelete] //can be changed to post
    public void DeleteListing(int id)
    {
        var listingdelete = _FastFlatDbContext.Listings.Find(id);
        _FastFlatDbContext.Remove(listingdelete); 
    }
}