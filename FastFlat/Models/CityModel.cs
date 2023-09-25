using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FastFlat.Models;

public class CityModel
{
    [Key]
    public int CityModelId { get; set; }
    
    public string? CityName { get; set; }
    public string? Country { get; set; }  //may have to change this one due to DB management, FK
   

}