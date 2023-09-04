namespace exam_personal.Models;

public class CityModel
{
    private int id { get; set; }
    private String name { get; set; }
    private ContryModel country { get; set; } //may have to change this one due to DB managementgi
}