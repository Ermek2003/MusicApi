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
    

    public async Task<T> AddAsync(T item)
    {
        await Set.AddAsync(item);
        return item;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var entity = await Set.FindAsync(id);
        if (entity is null)
            return 0;
        Set.Remove(entity);
        return id;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        if (filter is not null)
            return Set.Where(filter).AsNoTracking();
        return Set.AsNoTracking();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Set.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public T Update(T entity)
    {
        Set.Update(entity);
        return entity;
    }
}
