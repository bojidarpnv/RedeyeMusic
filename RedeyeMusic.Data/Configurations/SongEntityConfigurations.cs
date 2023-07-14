using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations
{
    public class SongEntityConfigurations : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder
                .HasOne(a => a.Artist)
                .WithMany(s => s.Songs)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(g => g.Genre)
                .WithMany(s => s.Songs)
                .HasForeignKey(g => g.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateSongs());
        }
        private Song[] GenerateSongs()
        {
            ICollection<Song> songs = new HashSet<Song>();

            Song song;
            song = new Song()
            {
                Id = 1,
                Title = "SampleSong",
                ImageUrl = "https://images.theconversation.com/files/258026/original/file-20190208-174861-nms2kt.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=926&fit=clip",
                FilePath = "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3",
                ArtistId = 1,
                AlbumId = 1,
                GenreId = 1,
            };
            songs.Add(song);

            song = new Song()
            {
                Id = 2,
                Title = "SampleSong2",
                ImageUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/fe529a64193929.5aca8500ba9ab.jpg",
                FilePath = "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3",
                ArtistId = 1,
                AlbumId = 2,
                GenreId = 2,
            };
            songs.Add(song);
            song = new Song()
            {
                Id = 3,
                Title = "SampleSong3",
                ImageUrl = "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/149562217/original/fc77d96de1229ad6ca6f83289fd2d4b4c068a568/make-album-and-song-covers.jpg",
                FilePath = "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3",
                ArtistId = 1,
                AlbumId = 1,
                GenreId = 1,
            };
            songs.Add(song);

            return songs.ToArray();
        }
    }
}
