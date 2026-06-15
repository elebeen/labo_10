using labo_10.Domain.Models;

namespace labo_10.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetAllWithRolesAsync(CancellationToken cancellationToken = default);
}