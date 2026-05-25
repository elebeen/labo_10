using labo_10.Dto;

namespace labo_10.Interfaces;

public interface IAuthService
{
    // Recibe credenciales y retorna el JWT string
    public string GenerateJwtToken(string userId, string userName, string role);
    public Task<bool> RegisterAsync(RegisterRequest user);
    public bool ValidateUser(LoginRequest user);
    public Task<string?> GetUserByEmail(string email);
}