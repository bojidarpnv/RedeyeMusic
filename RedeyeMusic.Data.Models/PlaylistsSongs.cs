using System.ComponentModel.DataAnnotations.Schema;

namespace RedeyeMusic.Data.Models
{
    public class PlaylistsSongs
    {
        [ForeignKey(nameof(Playlist))]
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; } = null!;


        [ForeignKey(nameof(Song))]
        public int SongId { get; set; }
        public Song Song { get; set; } = null!;
    }
}
