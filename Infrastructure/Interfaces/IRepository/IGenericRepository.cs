using System.Linq.Expressions;

namespace Infrastructure.Interfaces.IRepository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null);
    Task<T> FindAsync(Expression<Func<T, bool>>? filter = null);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}
