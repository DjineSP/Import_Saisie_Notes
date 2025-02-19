using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.ImportationModels;

public class Filiere
{
    [Key]
    public int IdFiliere { get; set; }

    [Required]
    [MaxLength(20)]
    public string? CodeFiliere { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Intitule { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Title { get; set; }

}
