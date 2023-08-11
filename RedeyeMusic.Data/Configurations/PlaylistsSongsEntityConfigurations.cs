using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations
{
    public class PlaylistsSongsEntityConfigurations : IEntityTypeConfiguration<PlaylistsSongs>
    {
        public void Configure(EntityTypeBuilder<PlaylistsSongs> builder)
        {
            builder
            .HasKey(ps => new { ps.PlaylistId, ps.SongId });

            builder
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistsSongs)
                .HasForeignKey(ps => ps.PlaylistId);

            builder
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistsSongs)
                .HasForeignKey(ps => ps.SongId);
        }
    }
}
