using RedeyeMusic.Web.ViewModels.Artist;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IArtistService
    {
        Task<bool> ArtistExistsByUserIdAsync(string userId);

        Task CreateFirstSongAsync(string userId, string userName, AddSongFormModel song);
        Task CreateAsync(string userId, BecomeArtistFormModel formModel);
        Task<bool> ArtistNameExistsAsync(string artistName);
        Task<int?> GetArtistIdByUserIdAsync(string userId);
    }
}
