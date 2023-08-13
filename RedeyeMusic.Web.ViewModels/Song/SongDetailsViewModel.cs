using RedeyeMusic.Web.ViewModels.Artist;
using RedeyeMusic.Web.ViewModels.Home;

namespace RedeyeMusic.Web.ViewModels.Song
{
    public class SongDetailsViewModel : IndexViewModel
    {
        public ArtistInfoOnSongViewModel Artist { get; set; } = null!;
        public string AlbumName { get; set; } = null!;
        public string AlbumImageUrl { get; set; } = null!;
    }
}
