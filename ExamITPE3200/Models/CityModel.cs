using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExamITPE3200.Models;

public class CityModel
{
    [Key]
    public int CityModelId { get; set; }
    
    public string? cityName { get; set; }
    public string? country { get; set; }  //may have to change this one due to DB management, FK
   


}