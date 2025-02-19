using Backend.Data;
using Backend.Models.Auth;
using Backend.Models.BonitaModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly BonitaDbContext? _context;
    private readonly IConfiguration _configuration;

    public AuthController(BonitaDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel userLogin)
    {
        // Rechercher l'utilisateur dans la base de données
        var user = await _context.Users
                                  .FirstOrDefaultAsync(u => u.Username == userLogin.Username);

        // Si l'utilisateur n'existe pas ou si le mot de passe est incorrect
        if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
        {
            return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
        }

        // Récupérer le rôle de l'utilisateur
        var userRole = await _context.UserMemberships
                                      .Where(um => um.UserId == user.Id)
                                      .Join(_context.Roles,
                                            um => um.RoleId,
                                            r => r.Id,
                                            (um, r) => r.Name)
                                      .FirstOrDefaultAsync();

        if (userRole == null)
        {
            return Unauthorized("L'utilisateur n'a pas de rôle attribué.");
        }

        // Générer le token JWT avec le rôle
        var token = GenerateJwtToken(user, userRole);

        // Retourner le token et le rôle
        return Ok(new { Token = token, Role = userRole });
    }

    private string GenerateJwtToken(User user, string role)
    {
        var keyValue = _configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(keyValue))
        {
            // Gérer l'erreur, par exemple en lançant une exception ou en attribuant une valeur par défaut.
            throw new InvalidOperationException("La clé JWT est manquante dans la configuration.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        Claim[] claims;

        if (user?.Username != null && role != null)
        {
            claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, role)
    };
        }
        else
        {
            // Gérer le cas où Username ou Role sont null, par exemple en lançant une exception ou en attribuant des valeurs par défaut.
            throw new ArgumentException("Le nom d'utilisateur ou le rôle est manquant.");
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Définir l'expiration à 30 secondes
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Vérifier si l'username existe déjà
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
        if (existingUser != null)
        {
            return BadRequest("Nom d'utilisateur déjà utilisé.");
        }

        // Vérifier la correspondance des mots de passe
        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest("Les mots de passe ne correspondent pas.");
        }

        // Vérifier si le rôle existe
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == model.Role);
        if (role == null)
        {
            return BadRequest("Le rôle spécifié n'existe pas.");
        }

        // Hacher le mot de passe
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

        // Création de l'utilisateur
        var user = new User
        {
            TenantId = 1,
            Firstname = model.Firstname,
            Lastname = model.Lastname,
            Username = model.Username,
            Password = hashedPassword,
            Enable = true,
            CreationDate = DateTime.UtcNow,
            LastUpdate = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Ajouter l'utilisateur au rôle
        var userMembership = new UserMembership
        {
            TenantId = 1,
            UserId = user.Id,
            RoleId = role.Id,
            AssignedBy = 1, // Optionnel : Qui assigne ce rôle ?
            AssignDate = DateTime.UtcNow
        };

        _context.UserMemberships.Add(userMembership);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Utilisateur enregistré avec succès.", userId = user.Id, role = model.Role });
    }

    [HttpGet("validateToken")]
    public IActionResult ValidateToken()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Unauthorized("Token manquant");
        }

        var authHeader = Request.Headers["Authorization"].ToString();
        if (!authHeader.StartsWith("Bearer "))
        {
            return Unauthorized("Format du token invalide");
        }

        var token = authHeader.Replace("Bearer ", "").Trim();

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyValue = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(keyValue))
            {
                // Gérer l'erreur, par exemple en lançant une exception
                throw new InvalidOperationException("La clé JWT est manquante dans la configuration.");
            }
            var key = Encoding.UTF8.GetBytes(keyValue);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"]
            }, out SecurityToken validatedToken);

            return Ok(new { message = "Token valide" });
        }
        catch (SecurityTokenExpiredException)
        {
            return Unauthorized("Token expiré");
        }
        catch (SecurityTokenException)
        {
            return Unauthorized("Token invalide");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erreur interne: {ex.Message}");
        }
    }
}