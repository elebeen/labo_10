namespace labo_10.Domain.Interfaces;

public interface IClosedXmlService
{
    public string GenerateJwtToken(string userId, string userName, string role);
}