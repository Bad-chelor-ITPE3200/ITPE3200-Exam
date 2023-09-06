namespace exam_personal.Models;

public class BookingModel
{
    
    private int id { get; set; }
    private String dates { get; set; } //might need to change, json object?
    private String bookingName { get; set; }

    public BookingModel(int id, string dates, string bookingName)
    {
        this.id = id;
        this.dates = dates;
        this.bookingName = bookingName;
    }
}
