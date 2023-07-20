using RedeyeMusic.Web.ViewModels.Artist;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IArtistService
    {
        Task<bool> ArtistExistsByUserIdAsync(string userId);


        Task CreateAsync(string userId, BecomeArtistFormModel formModel);
        Task<bool> ArtistNameExistsAsync(string artistName);
        Task<int?> GetArtistIdByUserIdAsync(string userId);
    }
}
