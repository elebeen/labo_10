using labo_10.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using labo_10.Dto;
using labo_10.Infrastructure.Models;
using labo_10.Infrastructure.Repositories;

namespace labo_10.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IRepository<User> _userRepository;

    public AuthService(IConfiguration configuration, IRepository<User> userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public string GenerateJwtToken(string userId, string userName)
    {
        // 1. Crear los Claims (datos dentro del token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        // 1. Validar si el usuario ya existe usando el método FindByName de tu repositorio
        // Nota: Como FindByName no es asíncrono en tu interfaz actual, se llama de forma síncrona
        var existingUser = _userRepository.FindByName(request.Nombre);
        if (existingUser != null) 
        {
            return false; // El usuario ya está registrado
        }

        // 2. Encriptar la contraseña
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // 3. Mapear el Request a la Entidad de Dominio
        var newUser = new User()
        {
            Username = request.Nombre,
            PasswordHash = hashedPassword // Asegúrate de usar el campo correcto de tu entidad
        };
        
        // 4. Guardar utilizando los métodos asíncronos de tu repositorio
        await _userRepository.AddAsync(newUser);
        
        // NOTA: Al remover UnitOfWork, el guardado final dependerá de si manejas el SaveChanges 
        // dentro del repositorio, o si inyectas el DbContext aquí. Si tu Repositorio no guarda automáticamente,
        // puedes añadir un método Task SaveChangesAsync() en tu IRepository o ejecutar _context.SaveChangesAsync()
        
        return true;
    }

    public bool ValidateUser(LoginRequest request)
    {
        // 1. Buscar al usuario por nombre usando tu repositorio
        var existingUser = _userRepository.FindByName(request.Email); // O request.Name según tus propiedades
        
        // Corregido: Validamos si el usuario de la base de datos realmente existe
        if (existingUser == null)
        {
            return false;
        }
        
        // 2. Verificar la contraseña usando la propiedad de tu entidad (ej. PasswordHash)
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash);
        
        return isPasswordValid;
    }
}