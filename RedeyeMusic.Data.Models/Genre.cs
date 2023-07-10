using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.Genre;

namespace RedeyeMusic.Data.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Songs = new HashSet<Song>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Song> Songs { get; set; }
    }
}