using RedeyeMusic.Web.ViewModels.Album;
using RedeyeMusic.Web.ViewModels.Genre;
using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.Song;

namespace RedeyeMusic.Web.ViewModels.Song
{
    public class EditSongFormModel
    {
        public EditSongFormModel()
        {
            Genres = new HashSet<GenreSelectViewModel>();
            Albums = new HashSet<AlbumSelectViewModel>();
        }
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Title Length must be between {2} and {1} characters")]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(LyricsMaxLength, MinimumLength = LyricsMinLength)]
        public string Lyrics { get; set; } = null!;
        //[Required]
        //[StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string? AlbumName { get; set; }
        public int AlbumId { get; set; }

        public string? AlbumImageUrl { get; set; }
        public ICollection<AlbumSelectViewModel>? Albums { get; set; }
        //[Required]
        //[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string? AlbumDescription { get; set; }
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public ICollection<GenreSelectViewModel>? Genres { get; set; }


    }
}
