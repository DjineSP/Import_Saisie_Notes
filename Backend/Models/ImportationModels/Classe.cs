using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class Classe
{
    [Key]
    public int IdClasse { get; set; }

    [Required]
    [MaxLength(20)]
    public string? CodeClasse { get; set; }

    // Foreign Keys
    [ForeignKey("Niveau")]
    public int IdNiveau { get; set; }
    public Niveau? Niveau { get; set; }

    [ForeignKey("Grade")]
    public int IdGrade { get; set; }
    public Grade? Grade { get; set; }

    [ForeignKey("Filiere")]
    public int IdFiliere { get; set; }
    public Filiere? Filiere { get; set; }

    [ForeignKey("Specialite")]
    public int IdSpecialite { get; set; }
    public Specialite? Specialite { get; set; }
}
