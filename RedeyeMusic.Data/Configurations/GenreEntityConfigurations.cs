using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations
{
    public class GenreEntityConfigurations : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(this.GenerateGenres());
        }

        private Genre[] GenerateGenres()
        {
            ICollection<Genre> genres = new HashSet<Genre>();

            Genre genre;
            genre = new Genre()
            {
                Id = 1,
                Name = "FirstGenre"
            };
            genres.Add(genre);
            genre = new Genre()
            {
                Id = 2,
                Name = "SecondGenre"
            };
            genres.Add(genre);

            return genres.ToArray();
        }

    }
}
