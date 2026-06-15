using labo_10.Domain.Interfaces;
using labo_10.Domain.Interfaces.Repositories;
using labo_10.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace labo_10.Infrastructure.Repositories;

public class TicketRepository : Repository<Ticket>, ITicketRepository
{
    public TicketRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Ticket>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Ticket>()
            .Include(t => t.User)
            .Include(t => t.Responses)
            .ThenInclude(r => r.Responder)
            .ToListAsync(cancellationToken);
    }
}