using Common.Helpers;
using DAL.EF;
using Models.Entities;

namespace DAL.Seed;

public static class DatabaseMigrator
{
    public static void SeedAdminUser(AppDbContext context, PasswordHasher passwordHesher)
    {
        if (!context.Users.Any(u => u.Email == "admin@exemple.com"))
        {
            var user = new User
            {
                Name = "admin",
                Role = "SuperAdmin",
                Email = "admin@exemple.com",
                Password = passwordHesher.HashPassword("12qw!@QW")
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
