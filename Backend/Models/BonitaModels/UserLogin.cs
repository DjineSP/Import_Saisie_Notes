using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.BonitaModels;

public class UserLogin
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Tenant")]
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

    public DateTime LastConnection { get; set; } = DateTime.UtcNow;
}
