using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
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

        public Task CreateGenreAsync(string genreName)
        {
            throw new NotImplementedException();
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
