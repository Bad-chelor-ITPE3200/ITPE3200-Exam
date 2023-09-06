namespace exam_personal.Models;

public class ListingModel
{
    private int id { get; set; }
 // private String imgurl { get; set; }
    private string listingName { get; set; }
    private int noOfBeds { get; set; }
    private string city { get; set; }
    private int area { get; set; }
    private float rating { get; set; }
    private List<BookingModel> bookings { get; set; }
    private bool available { get; set; }


    public ListingModel(int id ,string listingName,int noOfBeds, string city, int area, float rating, List<BookingModel> bookings,
        bool available)
    {
        this.id = id;
        //this.imgurl = imgurl; 
        this.listingName = listingName; 
        this.noOfBeds = noOfBeds;
        this.city = city;
        this.area = area;
        this.rating = rating;
        this.bookings = bookings;
        this.available = available; 
    }
}