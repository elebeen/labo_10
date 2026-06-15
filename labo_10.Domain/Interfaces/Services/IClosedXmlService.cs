using labo_10.Domain.Models;

namespace labo_10.Domain.Interfaces.Services;

public interface IClosedXmlService
{
    byte[] GenerateTicketsReport(IEnumerable<Ticket> tickets);
    byte[] GenerateUsersRolesReport(IEnumerable<User> users);
    public void FirstExample();
    public void ModifyArchive();
    public void ThirdExample();
    public void FourthExample();
}