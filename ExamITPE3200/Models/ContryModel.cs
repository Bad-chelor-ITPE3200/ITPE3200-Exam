namespace exam_personal.Models;

public class ContryModel
{
    private int id { get; set; }
    private string name { get; set; }

    public ContryModel(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}