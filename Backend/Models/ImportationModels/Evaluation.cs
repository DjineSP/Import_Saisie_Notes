using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class Evaluation
{
    [Key]
    public int IdEvaluation { get; set; }

    [ForeignKey("UE")]
    public int IdUE { get; set; }
    public UE? UE { get; set; }

    [Required]
    [MaxLength(20)]
    public string? CodeEvaluation { get; set; }

    public int Credit { get; set; }
}
