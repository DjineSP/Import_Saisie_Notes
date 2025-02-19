using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models.BonitaModels;

public class UserContactInfo
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Tenant")]
    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required, EmailAddress]
    public string? Email { get; set; }

    public string? Phone { get; set; }
    public string? Whatsapp { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}
