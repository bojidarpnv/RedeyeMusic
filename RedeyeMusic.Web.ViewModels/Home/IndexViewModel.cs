namespace RedeyeMusic.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int ListenCount { get; set; }
        public string Lyrics { get; set; } = null!;
        public string ArtistName = null!;

        public string GenreName = null!;
    }
}
