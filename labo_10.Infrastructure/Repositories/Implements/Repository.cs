using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace labo_10.Infrastructure.Repositories.Implements;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
    
    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
    
    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
    
    public void Update(T entity) => _context.Set<T>().Update(entity);
    
    public void Delete(T entity) => _context.Set<T>().Remove(entity);
    
    public async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
    {
        // Esto permite buscar por CUALQUIER propiedad dinámicamente
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
    
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}