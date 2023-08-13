namespace RedeyeMusic.Web.ViewModels.Home
{
    using RedeyeMusic.Data.Models;
    using RedeyeMusic.Services.Mapping;
    public class IndexViewModel : IMapFrom<Song>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int Duration { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int ListenCount { get; set; }
        public string Lyrics { get; set; } = null!;
        public string ArtistName = null!;

        public string GenreName = null!;
        public string Mp3FilePath = null!;
    }
}
