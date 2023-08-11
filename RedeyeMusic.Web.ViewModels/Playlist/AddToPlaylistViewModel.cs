using RedeyeMusic.Web.ViewModels.Home;

namespace RedeyeMusic.Web.ViewModels.Playlist
{
    public class AddToPlaylistViewModel
    {
        public AddToPlaylistViewModel()
        {
            this.Playlists = new HashSet<PlaylistViewModel>();
        }
        public IndexViewModel SongModel { get; set; } = null!;
        public IEnumerable<PlaylistViewModel> Playlists { get; set; } = null!;
    }
}
