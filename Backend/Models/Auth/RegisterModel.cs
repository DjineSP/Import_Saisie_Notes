namespace Backend.Models.Auth;

public class RegisterModel
{
    public int TenantId { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Role { get; set; } // Nom du rôle
    public int? AssignedBy { get; set; } // Optionnel : ID de l'admin qui assigne le rôle
}
