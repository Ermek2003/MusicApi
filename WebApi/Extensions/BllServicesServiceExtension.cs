using BLL.Services;
using Common.Helpers;
using Infrastructure.Interfaces.IServices;

namespace WebApi.Extensions;

public static class BllServicesServiceExtension
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<PasswordHasher>();
    }
}
