using labo_10.Dto;

namespace labo_10.Interfaces;

public interface IAuthService
{
    // Recibe credenciales y retorna el JWT string
    string GenerateJwtToken(string userId, string userName);
    Task<bool> RegisterAsync(RegisterRequest user);
    public bool ValidateUser(LoginRequest user);
}