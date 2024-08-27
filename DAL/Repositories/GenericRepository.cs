using DAL.EF;
using Infrastructure.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    protected DbSet<T> Set => _context.Set<T>();

    public async Task AddAsync(T item)
    {
        await Set.AddAsync(item);
    }

    public void Delete(T item)
    {
        Set.Remove(item);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>>? filter = null)
    {
        return await Set.AnyAsync(filter);
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        if (filter is not null)
            return Set.Where(filter).AsNoTracking();
        return Set.AsNoTracking();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Set.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        Set.Update(entity);
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>>? filter = null)
    {
        return await Set.FirstOrDefaultAsync(filter);
    }
}
