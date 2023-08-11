using RedeyeMusic.Web.ViewModels.Home;

namespace RedeyeMusic.Web.ViewModels.Playlist
{
    public class PlaylistViewModel
    {
        public PlaylistViewModel()
        {
            this.Songs = new HashSet<IndexViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual IEnumerable<IndexViewModel> Songs { get; set; } = null!;
    }
}
