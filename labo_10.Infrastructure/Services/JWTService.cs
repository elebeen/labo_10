using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using labo_10.Domain.Interfaces;

namespace labo_10.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(string userId, string userName, string role)
    {
        // 1. Crear los Claims (datos dentro del token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
        };

        // 2. Obtener la llave secreta que usaste en ServiceRegistrationExtensions.cs
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3. Crear el token con los validadores (Issuer y Audience)
        var token = new JwtSecurityToken(
            issuer: "languagebridgesolutions.com",
            audience: "languagebridgesolutions.com",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2), // Tiempo de expiración
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /*public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        // 1. Validar por Email en lugar de Nombre (es más seguro que sea único)
        var existingUser = await _userRepository.FindFirstAsync(u => u.Email == request.Email);
        if (existingUser != null) return false; 

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var newUser = new User()
        {
            UserId = Guid.NewGuid(),
            Username = request.Nombre,
            // 🔴 DEBES agregar la propiedad Email a tu clase User y mapearla aquí:
            Email = request.Email, 
            PasswordHash = hashedPassword 
        };
    
        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync();
    
        return true;
    }

    public bool ValidateUser(LoginRequest request)
    {
        // 1. Buscar al usuario por nombre usando tu repositorio
        var existingUser = _userRepository.FindFirstAsync(u => u.Email == request.Email).Result; // O request.Name según tus propiedades
        
        // Corregido: Validamos si el usuario de la base de datos realmente existe
        if (existingUser == null)
        {
            return false;
        }
        
        // 2. Verificar la contraseña usando la propiedad de tu entidad (ej. PasswordHash)
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash);
        
        return isPasswordValid;
    }

    public Task<string?> GetUserByEmail(string email)
    {
        var user = _userRepository.FindFirstAsync(u => u.Email == email).Result;
        if (user == null)
        {
            return null;
        }
        
        return Task.FromResult(user.Email);
    }*/
}