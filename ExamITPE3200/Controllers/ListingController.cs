using exam_personal.Models;
using ExamITPE3200.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamITPE3200.Controllers;

public class ListingController
{
    private FastFlatDbContext _flatDbContext;

    public ListingController(FastFlatDbContext fastFlatDbContext)
    {
        fastFlatDbContext = _flatDbContext; 
    }
    //if we got time, before saving 
    [HttpPost]
    public void validateListing(ListingModel Listing)
    {
        List<ListingModel> all = FastFlatDbContext.Listing.ToList();
        FastFlatDbContext 
    }

    public void createListing(ListingModel listing)
    {
        
    }
}