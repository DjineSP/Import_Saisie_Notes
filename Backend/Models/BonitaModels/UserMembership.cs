using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.BonitaModels;

public class UserMembership
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Tenant")]
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public Role? Role { get; set; }

    public int? AssignedBy { get; set; }
    public DateTime AssignDate { get; set; } = DateTime.UtcNow;
}
