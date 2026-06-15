using labo_10.Domain.Interfaces;
using labo_10.Domain.Interfaces.Repositories;
using labo_10.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace labo_10.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<User>> GetAllWithRolesAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<User>()
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync(cancellationToken);
    }
}