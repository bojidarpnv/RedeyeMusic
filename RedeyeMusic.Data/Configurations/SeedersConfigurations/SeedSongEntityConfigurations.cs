using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations.SeedersConfigurations
{
    public class SeedSongEntityConfigurations : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasData(GenerateSongs());
        }
        private Song[] GenerateSongs()
        {
            ICollection<Song> songs = new HashSet<Song>();

            Song song;
            song = new Song()
            {
                Id = 1,
                Title = "SampleSong",
                Lyrics = "SampleSongLyrics",
                ImageUrl = "https://images.theconversation.com/files/258026/original/file-20190208-174861-nms2kt.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=926&fit=clip",
                Mp3FilePath = "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3",
                ArtistId = 1,
                AlbumId = 1,
                GenreId = 1,
            };
            songs.Add(song);

            song = new Song()
            {
                Id = 2,
                Title = "SampleSong2",
                Lyrics = "SampleSong2Lyrics",
                ImageUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/fe529a64193929.5aca8500ba9ab.jpg",
                Mp3FilePath = "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3",
                ArtistId = 1,
                AlbumId = 2,
                GenreId = 2,
            };
            songs.Add(song);
            song = new Song()
            {
                Id = 3,
                Title = "SampleSong3",
                Lyrics = "SampleSong3Lyrics",
                ImageUrl = "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/149562217/original/fc77d96de1229ad6ca6f83289fd2d4b4c068a568/make-album-and-song-covers.jpg",
                Mp3FilePath = "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3",
                ArtistId = 1,
                AlbumId = 1,
                GenreId = 1,
            };
            songs.Add(song);

            return songs.ToArray();
        }
    }
}
