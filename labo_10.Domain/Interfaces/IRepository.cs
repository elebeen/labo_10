using System.Linq.Expressions;

namespace labo_10.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate);
    Task SaveChangesAsync();
}