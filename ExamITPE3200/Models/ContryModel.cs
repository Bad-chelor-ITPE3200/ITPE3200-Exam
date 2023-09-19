using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExamITPE3200.Models;

public class ContryModel
{
    [Key]
    public int ContryId { get; set; }

    public string? Contryname { get; set; }

    /*public ContryModel(int id, string name)
    {
        this.id = id;
        this.name = name;
    }*/
}