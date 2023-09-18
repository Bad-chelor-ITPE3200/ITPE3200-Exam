using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace exam_personal.Models;

public class BookingModel
{
    [Key]
    public int bookingModelId { get; set; }
    public string dates { get; set; } //might need to change, json object?
    public int UserId { get; set; }
    public string? bookingName { get; set; } 
   }
