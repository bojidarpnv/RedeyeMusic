﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RedeyeMusic.Common.EntityValidationConstants.Song;

namespace RedeyeMusic.Data.Models
{
    public class Song
    {
        public Song()
        {
            this.PlaylistsSongs = new HashSet<PlaylistsSongs>();
        }
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(TitleMaxLength)]
        public string Title { get; set; } = null!;
        [MaxLength(DurationMaxLengthInSeconds)]
        public int Duration { get; set; }

        public int ListenCount { get; set; }


        [StringLength(LyricsMaxLength)]
        [Required]
        public string Lyrics { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public string Mp3FilePath { get; set; } = null!;


        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; } = null!;


        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; } = null!;


        [ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; } = null!;
        public virtual ICollection<PlaylistsSongs> PlaylistsSongs { get; set; } = null!;

        public bool IsDeleted { get; set; }

    }
}
