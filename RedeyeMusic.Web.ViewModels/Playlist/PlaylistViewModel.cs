using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Web.ViewModels.Playlist
{
    public class PlaylistViewModel
    {
        public PlaylistViewModel()
        {
            this.Songs = new HashSet<SongDetailsOnPlaylistViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual IEnumerable<SongDetailsOnPlaylistViewModel> Songs { get; set; } = null!;
    }
}
