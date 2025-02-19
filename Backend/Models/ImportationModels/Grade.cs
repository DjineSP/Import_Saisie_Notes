using System.ComponentModel.DataAnnotations;

namespace Backend.Models.ImportationModels;

public class Grade
{
    [Key]
    public int IdGrade { get; set; }

    [Required]
    [MaxLength(10)]
    public string? CodeGrade { get; set; }
}
