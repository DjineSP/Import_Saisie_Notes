using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class Niveau
{
    [Key]
    public int IdNiveau { get; set; }

    [Required]
    [MaxLength(10)]
    public string? CodeNiveau { get; set; }

}
