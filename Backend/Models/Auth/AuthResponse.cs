namespace Backend.Models.Auth;

public class AuthResponse
{
    public required string Token { get; set; }
    public required string Role { get; set; }
}