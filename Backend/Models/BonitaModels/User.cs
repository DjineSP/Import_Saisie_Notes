using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.BonitaModels;

public class User
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Tenant")]
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    public bool Enable { get; set; } = true;

    [Required, MaxLength(50)]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Title { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
}
