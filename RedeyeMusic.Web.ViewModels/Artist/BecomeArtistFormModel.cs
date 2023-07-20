using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RedeyeMusic.Common.EntityValidationConstants.Artist;

namespace RedeyeMusic.Web.ViewModels.Artist
{
    public class BecomeArtistFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string ArtistName { get; set; } = null!;
    }
}
