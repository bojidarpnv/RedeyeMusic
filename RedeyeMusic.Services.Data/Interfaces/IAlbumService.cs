using RedeyeMusic.Web.ViewModels.Album;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IAlbumService
    {
        Task<ICollection<AlbumSelectViewModel>> SelectAlbumsByArtistIdAsync(int artistId);
        Task AddAlbum(AlbumFormModel albumViewModel, int artistId);

        Task<int> GetAlbumId(AddSongFormModel songModel);
        Task<AddSongFormModel> GetAlbumDescriptionAndNameAndUrlById(int albumId, AddSongFormModel songModel);
        Task<IEnumerable<AlbumSelectViewModel>> AllByArtistIdAsync(int artistId);
        Task<bool> ExistsById(int albumId);
        Task<AlbumDetailsViewModel> GetDetailsByIdAsync(int albumId);
        Task<EditAlbumFormModel> GetAlbumForEditAsync(int albumId);
        Task UpdateAlbumAsync(EditAlbumFormModel viewModel);
    }
}
