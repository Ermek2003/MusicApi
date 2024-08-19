using System.Linq.Expressions;

namespace Infrastructure.Interfaces.IRepository;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null);
    Task<T> AddAsync(T entity);
    T Update(T entity);
    Task<int> DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task SaveChangesAsync();
}
