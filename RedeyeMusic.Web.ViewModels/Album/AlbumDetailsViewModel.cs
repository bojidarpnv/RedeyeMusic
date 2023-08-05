using RedeyeMusic.Web.ViewModels.Home;

namespace RedeyeMusic.Web.ViewModels.Album
{
    public class AlbumDetailsViewModel : AlbumSelectViewModel
    {
        public AlbumDetailsViewModel()
        {
            this.Songs = new HashSet<IndexViewModel>();
        }
        public IEnumerable<IndexViewModel> Songs { get; set; } = null!;
        public string ArtistName { get; set; } = null!;
    }
}
