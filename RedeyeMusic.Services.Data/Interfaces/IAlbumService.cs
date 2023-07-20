using RedeyeMusic.Web.ViewModels.Album;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IAlbumService
    {
        public Task<ICollection<AlbumSelectViewModel>> SelectAlbumsByArtistIdAsync(int artistId);
        public Task AddAlbum(AlbumSelectViewModel albumSelectViewModel);

    }
}
