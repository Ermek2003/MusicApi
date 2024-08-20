using Common.Helpers;
using DAL.EF;
using DAL.Seed;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extensions;

public static class DatabaseInitializer
{
    public static void InitializeDatabase(this IServiceProvider servicesProvider)
    {
        using var scope = servicesProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHesher = scope.ServiceProvider.GetRequiredService<PasswordHasher>();

        context.Database.Migrate();
        DatabaseMigrator.SeedAdminUser(context, passwordHesher);
    }
}
