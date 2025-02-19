using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class UE
{
    [Key]
    public int IdUE { get; set; }

    [Required]
    [MaxLength(100)]
    public string? CodeUE { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Intitule { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Title { get; set; }

    [Required]
    [MaxLength(10)]
    public string? Semestre { get; set; }

}
