using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(32).IsRequired();
        builder.Property(t => t.Description).HasMaxLength(256).IsRequired();
        builder.Property(t => t.Ganre).HasMaxLength(32).IsRequired();

        builder.HasOne(t => t.Album)
            .WithMany(u => u.Tracks)
            .HasForeignKey(t => t.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
