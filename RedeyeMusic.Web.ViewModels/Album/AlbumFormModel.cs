using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.Album;

namespace RedeyeMusic.Web.ViewModels.Album
{
    public class AlbumFormModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
    }
}
