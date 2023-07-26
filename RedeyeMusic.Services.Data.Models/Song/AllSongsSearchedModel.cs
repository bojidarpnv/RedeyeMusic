using RedeyeMusic.Web.ViewModels.Home;

namespace RedeyeMusic.Services.Data.Models.Song
{
    public class AllSongsSearchedModel
    {
        public AllSongsSearchedModel()
        {
            this.SongsByTitle = new HashSet<IndexViewModel>();
            this.SongsByGenre = new HashSet<IndexViewModel>();
            this.SongsByArtist = new HashSet<IndexViewModel>();
            this.SongsByLyrics = new HashSet<IndexViewModel>();
        }
        public ICollection<IndexViewModel> SongsByTitle { get; set; }
        public ICollection<IndexViewModel> SongsByGenre { get; set; }
        public ICollection<IndexViewModel> SongsByArtist { get; set; }
        public ICollection<IndexViewModel> SongsByLyrics { get; set; }
    }
}
