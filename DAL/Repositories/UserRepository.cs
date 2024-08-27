using DAL.EF;
using Infrastructure.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
        => _context = context;

    public async Task<User?> GetByNameAsync(string name)
        => await _context.Users
        .Include(u => u.RefreshTokens)
        .FirstOrDefaultAsync(u => u.Name == name);

    public async Task<User> GetByRefreshTokenAsync(string refreshToken)
    {
        var user = await _context.Users
        .Include(u => u.RefreshTokens)
        .FirstOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken));

        if (user != null && user.RefreshTokens.Any(r => r.Token == refreshToken && r.IsActive))
        {
            return user;
        }

        return null;
    }
}
