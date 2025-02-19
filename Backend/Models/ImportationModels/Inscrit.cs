using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class Inscrit
{
    [Key]
    public int IdInscrit { get; set; }

    [Required]
    [ForeignKey("Etudiant")]
    public string? Matricule { get; set; }
    public Etudiant? Etudiant { get; set; }

    [ForeignKey("Classe")]
    public int IdClasse { get; set; }
    public Classe? Classe { get; set; }

    [Required]
    [MaxLength(10)]
    public string? Annee { get; set; }
}
