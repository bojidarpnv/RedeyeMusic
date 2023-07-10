using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.Playlist;

namespace RedeyeMusic.Data.Models
{
    public class Playlist
    {
        public Playlist()
        {
            this.Songs = new List<Song>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Song> Songs { get; set; }
    }
}
