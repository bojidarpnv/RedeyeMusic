using RedeyeMusic.Services.Mapping;

namespace RedeyeMusic.Web.ViewModels.Album
{
    using RedeyeMusic.Data.Models;
    public class AlbumSelectViewModel : IMapFrom<Album>
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public int ArtistId { get; set; }
        public string ImageUrl { get; set; } = null!;



    }
}
