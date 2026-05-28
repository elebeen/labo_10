namespace labo_10.Domain.Interfaces;

public interface IJwtService
{
    public string GenerateJwtToken(string userId, string userName, string role);
}