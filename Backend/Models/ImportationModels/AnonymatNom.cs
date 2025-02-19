using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class AnonymatNom
{
    [Key]
    [MaxLength(20)]
    public string? NumAnonymat { get; set; }

    [Required]
    [ForeignKey("Etudiant")]
    [MaxLength(20)]
    public string? Matricule { get; set; }
    public Etudiant? Etudiant { get; set; }

}
