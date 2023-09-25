using System.ComponentModel.DataAnnotations;

namespace ExamITPE3200.Models;

public class ListingModel
{
    [Key] public int listingId { get; set; }

    // public String imgurl { get; set; }
    public string? listingName { get; set; }
    public string? username { get; set; }
    public int noOfBeds { get; set; }
    public string? city { get; set; }
    public int area { get; set; }
    public float rating { get; set; }
    public List<BookingModel>? bookings { get; set; }
    public bool available { get; set; }


}