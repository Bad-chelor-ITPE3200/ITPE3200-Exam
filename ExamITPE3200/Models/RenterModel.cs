using System.ComponentModel.DataAnnotations;

namespace exam_personal.Models;

public class RenterModel /*: UserModel*/
{
    [Key]
    private int renterModelId { get; set; }
    private List<BookingModel>bookings { get; set; }

    /*public RenterModel(int id, String username, String password, String firstName, String lastName, String phone, String email, List<BookingModel> bookings) : base(id, username, password, firstName, lastName, phone, email)
    {
        this.bookings = bookings;
    }*/
}