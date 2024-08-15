using BLL.Services;
using Infrastructure.Interfaces.IServices;

namespace WebApi.Extensions;

public static class BllServicesServiceExtension
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
    }
}
