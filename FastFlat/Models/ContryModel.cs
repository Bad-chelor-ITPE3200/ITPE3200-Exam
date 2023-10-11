using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FastFlat.Models;

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