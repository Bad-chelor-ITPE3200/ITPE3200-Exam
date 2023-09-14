using System.ComponentModel.DataAnnotations;

namespace exam_personal.Models;

public class ListingModel
{
    [Key]
    private int Id { get; set; }
 // private String imgurl { get; set; }
    private string listingName { get; set; }
    private string username { get; set; }
    private int noOfBeds { get; set; }
    private string city { get; set; }
    private int area { get; set; }
    private float rating { get; set; }
    private List<BookingModel> bookings { get; set; }
    private bool available { get; set; }


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