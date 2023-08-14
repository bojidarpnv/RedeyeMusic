using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Genre;

namespace RedeyeMusic.Services.Data
{
    public class GenreService : IGenreService
    {
        private readonly RedeyeMusicDbContext dbContext;
        public GenreService(RedeyeMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateGenreAsync(GenreFormModel formModel)
        {
            Genre genre = new Genre()
            {
                Name = formModel.Name,
            };
            await this.dbContext.Genres.AddAsync(genre);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<GenreSelectViewModel>> SelectGenresAsync()
        {

            ICollection<GenreSelectViewModel> allGenres = await this.dbContext
                .Genres
                .Select(g => new GenreSelectViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToArrayAsync();
            return allGenres;
        }

    }
}
