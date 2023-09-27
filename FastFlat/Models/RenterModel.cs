using System.ComponentModel.DataAnnotations;

namespace FastFlat.Models;

public class RenterModel : UserModel
{
    [Key]
    public int renterModelId { get; set; }
    public virtual List<BookingModel>?bookings { get; set; }

    /*public RenterModel(int id, String username, String password, String firstName, String lastName, String phone, String email, List<BookingModel> bookings) : base(id, username, password, firstName, lastName, phone, email)
    {
        this.bookings = bookings;
    }*/
}