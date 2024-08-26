using DAL.EF;
using Infrastructure.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Linq.Expressions;

namespace DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
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

    public async Task DeleteAsync(int id)
    {
        var entity = await Set.FindAsync(id);
        if (entity is not null)
            Set.Remove(entity);
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
}
