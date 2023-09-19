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


    /*public ListingModel(int Id ,string listingName,int noOfBeds, string city, int area, float rating, List<BookingModel> bookings,
        bool available)
    {
        this.Id = Id;
        //this.imgurl = imgurl;
        this.username = username;
        this.listingName = listingName;
        this.noOfBeds = noOfBeds;
        this.city = city;
        this.area = area;
        this.rating = rating;
        this.bookings = bookings;
        this.available = available;
    }*/
}