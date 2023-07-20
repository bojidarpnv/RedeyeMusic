using RedeyeMusic.Web.ViewModels.Genre;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IGenreService
    {
        public Task<ICollection<GenreSelectViewModel>> SelectGenresAsync();
        public Task CreateGenreAsync(string genreName);
    }
}
