using RedeyeMusic.Web.ViewModels.Artist;
using RedeyeMusic.Web.ViewModels.Genre;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IArtistService
    {
        Task<bool> ArtistExistsByUserIdAsync(string userId);

        public Task<ICollection<GenreSelectViewModel>> SelectGenresAsync();
        Task CreateFirstSongAsync(string userId, string userName, AddSongFormModel song);
    }
}
