using System.ComponentModel.DataAnnotations;

namespace ExamITPE3200.Models;

public class RenterModel /*: UserModel */
{
    [Key] public int renterModelId { get; set; }
    public List<BookingModel>? bookings { get; set; }

}