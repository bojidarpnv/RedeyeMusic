namespace RedeyeMusic.Web.ViewModels.Song
{
    public class AllSongsQueryModel
    {
        public AllSongsQueryModel()
        {
            //this.Genres = new HashSet<string>();
            //this.Songs = new HashSet<IndexViewModel>();
        }
        //public enum SongGenreSorting { get; set; }
        //public string Genre { get; set; }
        public string? SearchString { get; set; } = null!;

        //public IEnumerable<string> Genres { get; set; }

        //public IEnumerable<IndexViewModel> Songs { get; set; }

    }
}
