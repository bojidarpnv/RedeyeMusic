using Microsoft.AspNetCore.Http;
using RedeyeMusic.Web.ViewModels.Genre;
using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.Song;

namespace RedeyeMusic.Web.ViewModels.Artist
{
    public class AddSongFormModel
    {
        public AddSongFormModel()
        {
            this.Genres = new HashSet<GenreSelectViewModel>();
        }
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Title Length must be between {2} and {1} characters")]
        public string Title { get; set; } = null!;
        [Required]
        public string Lyrics { get; set; } = null!;

        public string AlbumName { get; set; } = null!;
        public string AlbumDescription { get; set; } = null!;

        public int GenreId { get; set; }
        public ICollection<GenreSelectViewModel>? Genres { get; set; }

        [Required]

        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Choose the file for your song")]
        [Required]
        public IFormFile Mp3File { get; set; } = null!;
        public string? FilePath { get; set; }

    }
}
