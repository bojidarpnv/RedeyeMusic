using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations
{
    public class ArtistEntityConfigurations : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasData(this.SeedArtists());
        }
        public Artist SeedArtists()
        {
            Artist artist = new Artist()
            {
                Id = -1,
                Name = "Artist",
                ApplicationUserId = Guid.Parse("3CDFC504-E0E3-41CD-BD90-7C711143FE69"),
            };
            return artist;
        }
    }
}
