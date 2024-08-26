using Models.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces.IRepository;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null);
    Task AddAsync(T entity);
    void Update(T entity);
    Task DeleteAsync(int id);
    Task<T?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}
