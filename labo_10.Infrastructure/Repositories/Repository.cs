using System.Linq.Expressions;
using labo_10.Domain.Interfaces;
using labo_10.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace labo_10.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext Context;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<T?> GetByIdAsync(int id) => await Context.Set<T>().FindAsync(id);
    
    public async Task<IEnumerable<T>> GetAllAsync() => await Context.Set<T>().ToListAsync();
    
    public async Task AddAsync(T entity) => await Context.Set<T>().AddAsync(entity);
    
    public void Update(T entity) => Context.Set<T>().Update(entity);
    
    public void Delete(T entity) => Context.Set<T>().Remove(entity);
    
    public async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
    {
        // Esto permite buscar por CUALQUIER propiedad dinámicamente
        return await Context.Set<T>().FirstOrDefaultAsync(predicate);
    }
    
    public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
}