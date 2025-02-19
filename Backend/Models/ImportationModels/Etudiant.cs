using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class Etudiant
{
    [Key]
    [MaxLength(20)]
    public string? Matricule { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Nom { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Prenoms { get; set; }

    [Required]
    [MaxLength(1)]
    public string? Sexe { get; set; }

    [Required]
    public DateTime DateNaissance { get; set; }

    [MaxLength(50)]
    public string? VilleNaissance { get; set; }
}
