using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace exam_personal.Models;

public class CityModel
{
    [Key]
    private int Id { get; set; }
    
    private string name { get; set; }
    private ContryModel country { get; set; } = null;  //may have to change this one due to DB management, FK 



}