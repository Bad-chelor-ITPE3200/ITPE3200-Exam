using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace exam_personal.Models;

public class BookingModel
{
    [Key]
    private int Id { get; set; }
    
    private String dates { get; set; } //might need to change, json object?
    private String bookingName { get; set; }

   }
