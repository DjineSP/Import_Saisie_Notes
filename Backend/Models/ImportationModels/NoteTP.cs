using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class NoteTP
{
    [Key]
    public int IdNoteTP { get; set; }

    [ForeignKey("Evaluation")]
    public int IdEvaluation { get; set; }
    public Evaluation Evaluation { get; set; }

    [Required]
    [ForeignKey("Etudiant")]
    [MaxLength(20)]
    public string? Matricule { get; set; }
    public Etudiant? Etudiant { get; set; }

    [Range(0.00, 100.00)]
    public decimal Note { get; set; }

    public string? Observation { get; set; }
}
