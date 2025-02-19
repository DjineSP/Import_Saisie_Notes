using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class Specialite
{
    [Key]
    public int IdSpecialite { get; set; }

    [Required]
    [MaxLength(20)]
    public string? CodeSpecialite { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Intitule { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Title { get; set; }
}
