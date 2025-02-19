using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.BonitaModels;

public class Role
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Tenant")]
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    [Required, MaxLength(50)]
    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
}
