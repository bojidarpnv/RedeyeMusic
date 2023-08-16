using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations.SeedersConfigurations
{
    public class SeedArtistEntityConfigurations : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasData(SeedArtists());
        }
        private IList<Artist> SeedArtists()
        {
            List<Artist> artists = new List<Artist>();
            Artist artist = new Artist()
            {
                Id = 1,
                Name = "Admin",
                ApplicationUserId = Guid.Parse("3CDFC504-E0E3-41CD-BD90-7C711143FE69"),
            };
            artists.Add(artist);
            Artist artist2 = new Artist()
            {
                Id = 2,
                Name = "Artist",
                ApplicationUserId = Guid.Parse("A7B1ECAD-7870-4D98-85DD-4E2BB4952FE2")
            };
            artists.Add(artist2);
            return artists;
        }
    }

}
