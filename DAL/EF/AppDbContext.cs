using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DAL.EF;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Playlist> Playlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Track>()
            .HasOne(p => p.Playlist)
            .WithMany(p => p.Tracks);
        base.OnModelCreating(modelBuilder);
    }
}
