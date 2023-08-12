using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.User;
namespace RedeyeMusic.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Playlists = new HashSet<Playlist>();

        }

        //public bool IsSubscribed { get; set; }

        public bool IsDeleted { get; set; }

        //public string ImageUrl { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;
    }
}
