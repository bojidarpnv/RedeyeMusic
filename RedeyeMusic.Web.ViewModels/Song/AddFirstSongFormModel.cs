﻿using Microsoft.AspNetCore.Http;
using RedeyeMusic.Web.ViewModels.Genre;
using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.Album;
using static RedeyeMusic.Common.EntityValidationConstants.Song;

namespace RedeyeMusic.Web.ViewModels.Song
{
    public class AddFirstSongFormModel
    {
        public AddFirstSongFormModel()
        {
            Genres = new HashSet<GenreSelectViewModel>();
        }
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Title Length must be between {2} and {1} characters")]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(LyricsMaxLength, MinimumLength = LyricsMinLength)]
        public string Lyrics { get; set; } = null!;
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string AlbumName { get; set; } = null!;
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string AlbumDescription { get; set; } = null!;
        [Required]
        [Display(Name = "Album Image URL")]
        public string AlbumImageUrl { get; set; } = null!;
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public ICollection<GenreSelectViewModel>? Genres { get; set; }


        [Required]
        [Display(Name = "Choose the file for your song")]
        public IFormFile Mp3File { get; set; } = null!;
        public string? FilePath { get; set; }

    }
}
