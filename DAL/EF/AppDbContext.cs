using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.EF;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    DbSet<User> Users { get; set; }
    DbSet<Track> Tracks { get; set; }
    DbSet<Playlist> Playlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Track>()
            .HasOne(p => p.Playlist)
            .WithMany(p => p.Tracks);
        base.OnModelCreating(modelBuilder);
    }
}
