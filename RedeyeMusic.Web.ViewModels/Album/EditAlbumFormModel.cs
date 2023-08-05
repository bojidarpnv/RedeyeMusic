using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.Album;

namespace RedeyeMusic.Web.ViewModels.Album
{
    public class EditAlbumFormModel
    {
        public EditAlbumFormModel()
        {
            this.Songs = new List<SelectListItem>();
            this.SelectedSongIds = new List<int>();
        }
        public int Id { get; set; }
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
        [Required(ErrorMessage = "Please select a song.")]
        public List<int> SelectedSongIds { get; set; } = null!;
        public List<SelectListItem> Songs { get; set; }
    }
}
