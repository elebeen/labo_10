using labo_10.Domain.Models;

namespace labo_10.Domain.Interfaces.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}