using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace exam_personal.Models;

public class ContryModel
{
    [Key]
    private int Id { get; set; }
    
    private string name { get; set; }

    /*public ContryModel(int id, string name)
    {
        this.id = id;
        this.name = name;
    }*/
}