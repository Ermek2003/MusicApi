using Models.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces.IRepository;

public interface IUserRepository
{
    IQueryable<User> GetAll(Expression<Func<User, bool>>? filter = null);
    Task<User?> GetByNameAsync(string name);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task<bool> UserExistAsync(string name);
    bool IsTokenActive(RefreshToken token);
    Task SaveChangesAsync();
}
