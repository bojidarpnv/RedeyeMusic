using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RedeyeMusic.Common.EntityValidationConstants.Artist;

namespace RedeyeMusic.Data.Models
{
    public class Artist
    {
        public Artist()
        {
            this.Songs = new HashSet<Song>();
            this.Albums = new HashSet<Album>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Song> Songs { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        //Null for now before implementing code to use this functionality (having users become artists)
        [ForeignKey(nameof(ApplicationUser))]
        public Guid? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; } = null!;

    }
}
