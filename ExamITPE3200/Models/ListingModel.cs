namespace exam_personal.Models;

public class ListingModel
{
    private int id { get; set; }
    private int noOfBeds { get; set; }
    private String City { get; set; }
    private int area { get; set; }
    private float rating { get; set; }
    private List<BookingModel> bookings { get; set; }
    private bool available { get; set; } 
    
    
    
}