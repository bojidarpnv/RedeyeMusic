using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RedeyeMusic.Common.EntityValidationConstants.Playlist;

namespace RedeyeMusic.Data.Models
{
    public class Playlist
    {
        public Playlist()
        {
            this.PlaylistsSongs = new HashSet<PlaylistsSongs>();
        }
        [Key]
        public int Id { get; set; }

        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        [ForeignKey(nameof(ApplicationUser))]
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<PlaylistsSongs> PlaylistsSongs { get; set; }
        public bool IsDeleted { get; set; }
    }
}
