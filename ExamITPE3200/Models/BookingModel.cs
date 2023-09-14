using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace exam_personal.Models;

public class BookingModel
{
    private int bookingModelId { get; set; }
    private String dates { get; set; } = null;//might need to change, json object?
    private String bookingName { get; set; } 
   }
