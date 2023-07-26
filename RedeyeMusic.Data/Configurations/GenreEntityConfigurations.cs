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
                Name = "Pop"
            };
            genres.Add(genre);
            genre = new Genre()
            {
                Id = 2,
                Name = "Rock"
            };
            genres.Add(genre);
            genre = new Genre()
            {
                Id = 3,
                Name = "HipHop"
            };
            genres.Add(genre);
            genre = new Genre()
            {
                Id = 4,
                Name = "R&B"
            };
            genres.Add(genre);
            genre = new Genre()
            {
                Id = 5,
                Name = "Classical"
            };
            genres.Add(genre);
            genre = new Genre()
            {
                Id = 6,
                Name = "Rap"
            };
            genres.Add(genre);
            genre = new Genre()
            {
                Id = 7,
                Name = "Country"
            };
            genres.Add(genre);
            genre = new Genre()
            {
                Id = 8,
                Name = "Alternative"
            };
            genres.Add(genre);

            return genres.ToArray();
        }

    }
}
