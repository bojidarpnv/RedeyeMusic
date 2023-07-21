namespace RedeyeMusic.Web.ViewModels.Album
{
    public class AlbumSelectViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; }
        public int ArtistId { get; set; }

    }
}
