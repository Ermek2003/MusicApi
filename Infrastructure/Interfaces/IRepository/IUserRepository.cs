using Models.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces.IRepository;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByNameAsync(string name);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
}
