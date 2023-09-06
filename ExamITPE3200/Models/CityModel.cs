namespace exam_personal.Models;

public class CityModel
{
    private int id { get; set; }
    private string name { get; set; }
    private ContryModel country { get; set; } //may have to change this one due to DB management, FK 

    public CityModel(int id, string name, ContryModel country)
    {
        this.id = id;
        this.name = name;
        this.country = country; 
    }
}