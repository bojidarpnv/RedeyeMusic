using RedeyeMusic.Web.ViewModels.Album;
using System.ComponentModel.DataAnnotations;

namespace RedeyeMusic.Web.ViewModels.Artist
{
    public class ArtistInfoOnSongViewModel
    {
        public ArtistInfoOnSongViewModel()
        {
            this.Albums = new HashSet<AlbumSelectViewModel>();
        }
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; } = null!;
        public ICollection<AlbumSelectViewModel> Albums { get; set; }
    }
}
