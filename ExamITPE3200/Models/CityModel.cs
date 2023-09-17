using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace exam_personal.Models;

public class CityModel
{
    [Key]
    public int CityModelId { get; set; }
    
    public string? cityName { get; set; }
    public string country { get; set; } = null;  //may have to change this one due to DB management, FK
   


}