using RedeyeMusic.Web.ViewModels.Genre;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IGenreService
    {
        Task<ICollection<GenreSelectViewModel>> SelectGenresAsync();
        Task CreateGenreAsync(GenreFormModel formModel);

    }
}
