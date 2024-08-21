using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Name).HasMaxLength(64).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(64).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(256).IsRequired();
        builder.Property(u => u.Role).HasMaxLength(32).IsRequired();
    }
}
