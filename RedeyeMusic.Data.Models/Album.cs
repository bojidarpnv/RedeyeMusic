using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RedeyeMusic.Common.EntityValidationConstants.Album;

namespace RedeyeMusic.Data.Models
{
    public class Album
    {
        public Album()
        {
            this.Songs = new HashSet<Song>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        //Shouldnt be nullable but is for now for testing purposes!
        public string? ImageUrl { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; } = null!;

        //[ForeignKey(nameof(Genre))]
        //public int GenreId { get; set; }
        //public virtual Genre Genre { get; set; } = null!;

        public virtual ICollection<Song> Songs { get; set; }
        public bool IsDeleted { get; set; }
    }
}
