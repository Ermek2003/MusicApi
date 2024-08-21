using DAL.Repositories;
using Infrastructure.Interfaces.IRepository;

namespace WebApi.Extensions;

public static class RepositoryServiceExtension
{
    public static void AddRepositoryService(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();  
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
