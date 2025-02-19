using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class AnonymatNote
{
    [Key]
    public int IdAnonymatNote { get; set; }

    [ForeignKey("AnonymatNom")]
    [MaxLength(20)]
    public string? NumAnonymat { get; set; }
    public AnonymatNom? AnonymatNom { get; set; }

    [ForeignKey("Evaluation")]
    public int IdEvaluation { get; set; }
    public Evaluation? Evaluation { get; set; }

    [Range(0.00, 100.00)]
    public decimal Note { get; set; }

    public string? Observation { get; set; }
}
