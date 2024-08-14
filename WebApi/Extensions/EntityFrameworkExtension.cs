using DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

public static class EntityFrameworkExtension
{
    public static void AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));
    }
}
