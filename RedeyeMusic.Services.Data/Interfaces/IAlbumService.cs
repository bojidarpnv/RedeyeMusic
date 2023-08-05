using RedeyeMusic.Web.ViewModels.Album;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IAlbumService
    {
        public Task<ICollection<AlbumSelectViewModel>> SelectAlbumsByArtistIdAsync(int artistId);
        public Task AddAlbum(AlbumFormModel albumViewModel, int artistId);

        public Task<int> GetAlbumId(AddSongFormModel songModel);
        public Task<AddSongFormModel> GetAlbumDescriptionAndNameAndUrlById(int albumId, AddSongFormModel songModel);
        public Task<IEnumerable<AlbumSelectViewModel>> AllByArtistIdAsync(int artistId);
    }
}
