﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations
{
    public class AlbumEntityConfigurations : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasData(this.GenerateAlbums());
        }
        private Album[] GenerateAlbums()
        {
            ICollection<Album> albums = new HashSet<Album>();

            Album album;
            album = new Album()
            {
                Id = 1,
                Name = "FirstAlbum",
                ArtistId = 1,
                GenreId = 1,
                Description = "Mauris suscipit, nunc sit amet sollicitudin varius, nisl eros consectetur diam, nec.",
            };
            albums.Add(album);
            album = new Album()
            {
                Id = 2,
                Name = "SecondAlbum",
                ArtistId = 1,
                GenreId = 2,
                Description = "Nunc vitae imperdiet felis. Integer ac finibus turpis. Praesent ipsum arcu, placerat.",
            };
            albums.Add(album);

            return albums.ToArray();
        }
    }
}