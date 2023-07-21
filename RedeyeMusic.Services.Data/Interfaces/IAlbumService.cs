using RedeyeMusic.Web.ViewModels.Album;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IAlbumService
    {
        public Task<ICollection<AlbumSelectViewModel>> SelectAlbumsByArtistIdAsync(int artistId);
        public Task AddAlbum(AlbumFormModel albumViewModel, int artistId);

        public Task<int> GetAlbumId(AddSongFormModel songModel);
        public Task<AddSongFormModel> GetAlbumDescriptionAndNameById(int albumId, AddSongFormModel songModel);
    }
}
