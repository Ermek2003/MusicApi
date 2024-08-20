using DAL.EF;
using Infrastructure.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Linq.Expressions;

namespace DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
        => _context = context;

    public async Task AddAsync(User user)
        => await _context.Users.AddAsync(user);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<User?> GetByIdAsync(int id)
        => await _context.Users.FindAsync(id);

    public async Task<User?> GetByNameAsync(string name)
        => await _context.Users
        .Include(u => u.RefreshTokens)
        .FirstOrDefaultAsync(u => u.Name == name);

    private async Task<IQueryable<User?>> GetWithFilters(IQueryable<User> query,
        bool? checkExpireDate = null,
        int? take = null)
    {
        if (checkExpireDate.HasValue)
            query = query.Where(x => x.RefreshTokens.Any(y => y.Expires >= DateTime.UtcNow));

        if (take.HasValue)
            query = query.Take(take.Value);

        return query;
    }
    //private Task<IQueryable> sasa(Expression<Func<User, bool>> predicate)
    //{

    //}
    public async Task<User> GetByRefreshTokenAsync(string refreshToken)
    {
        var user = await _context.Users
        .Include(u => u.RefreshTokens)
        .FirstOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken));

        if (user != null && user.RefreshTokens.Any(r => r.Token == refreshToken && IsTokenActive(r)))
        {
            return user;
        }

        return null;
        //return await _context.Users
        //    .Include(u => u.RefreshTokens)
        //    .FirstOrDefaultAsync(u => u.RefreshTokens
        //        .Any(r => IsTokenActive(r) && r.Token == refreshToken));
        //return await (await GetWithFilters(
        //    _context.Users.Include(u => u.RefreshTokens.Where(x => x.Expires >= DateTime.UtcNow),
        //    take: 1
        //    ))
        //    .FirstOrDefaultAsync();
        //u => u.RefreshTokens.Any(r => r.Token == refreshToken && r.IsActive)
    }

    private bool IsTokenExpired(RefreshToken token)
    {
        return DateTime.UtcNow >= token.Expires;
    }

    public bool IsTokenActive(RefreshToken token)
    {
        return token.Revoked == null && !IsTokenExpired(token);
    }

    public async Task<bool> UserExistAsync(string name) 
        => await _context.Users.AnyAsync(u => u.Name == name);

    public async Task SaveChangesAsync() 
        => await _context.SaveChangesAsync();

    public IQueryable<User> GetAll(Expression<Func<User, bool>>? filter = null)
    {
        if (filter is not null)
            return _context.Users.Where(filter).AsNoTracking();
        return _context.Users.AsNoTracking();
    }
}
