using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedeyeMusic.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Playlists = new HashSet<Playlist>();

        }

        public bool IsSubscribed { get; set; }

        public bool IsDeleted { get; set; }
        
        public virtual ICollection<Playlist> Playlists { get; set; }
    }
}
