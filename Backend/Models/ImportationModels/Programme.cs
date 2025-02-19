using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class Programme
{
    [Key]
    public int IdProgramme { get; set; }

    [ForeignKey("Classe")]
    public int IdClasse { get; set; }
    public Classe? Classe { get; set; }

    [ForeignKey("UE")]
    public int IdUE { get; set; }
    public UE? UE { get; set; }

    [StringLength(50)]
    public string? Professeur { get; set; }
}
