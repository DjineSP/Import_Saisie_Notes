using System.ComponentModel.DataAnnotations;
namespace Backend.Models.BonitaModels;

public class Tenant
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public int? CreatedBy { get; set; }

    public int? DefaultTenantId { get; set; }

    [Required]
    public bool Status { get; set; } = false;
}
