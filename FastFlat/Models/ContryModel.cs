using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FastFlat.Models;

public class ContryModel
{
    [Key]
    public int ContryId { get; set; }
    
    [RegularExpression("@[a-zA-Z] -/ {3, 30}$", ErrorMessage = "Not a vaid country, must be between 3 to 30 letters, only latin letters")]
    public string? Contryname { get; set; }

    /*public ContryModel(int id, string name)
    {
        this.id = id;
        this.name = name;
    }*/
}