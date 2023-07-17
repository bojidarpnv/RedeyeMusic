using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Artist;
using RedeyeMusic.Web.ViewModels.Genre;

namespace RedeyeMusic.Services.Data
{
    public class ArtistService : IArtistService
    {
        private readonly RedeyeMusicDbContext dbContext;
        public ArtistService(RedeyeMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> ArtistExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Artists
                .AnyAsync(a => a.ApplicationUserId.ToString() == userId);
            return result;
        }
        public async Task<ICollection<GenreSelectViewModel>> SelectGenresAsync()
        {
            ICollection<GenreSelectViewModel> allGenres = await this.dbContext
                .Genres
                .Select(g => new GenreSelectViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToArrayAsync();
            return allGenres;
        }
        public async Task CreateFirstSongAsync(string userId, string userName, AddSongFormModel songModel)
        {

            Artist artist = new Artist()
            {
                Name = userName,
                ApplicationUserId = Guid.Parse(userId),

            };
            await this.dbContext.Artists.AddAsync(artist);
            await this.dbContext.SaveChangesAsync();
            Album album = new Album()
            {
                Name = songModel.AlbumName,
                Description = songModel.AlbumDescription,
                ArtistId = artist.Id,
                GenreId = songModel.GenreId,


            };

            Song song = new Song()
            {
                Title = songModel.Title,
                Lyrics = songModel.Lyrics,
                ImageUrl = songModel.ImageUrl,
                FilePath = songModel.FilePath,
                GenreId = songModel.GenreId,
                ArtistId = artist.Id,
                AlbumId = album.Id,
            };

            album.GenreId = song.GenreId;


            await this.dbContext.Albums.AddAsync(album);
            await this.dbContext.Songs.AddAsync(song);
            await this.dbContext.SaveChangesAsync();
        }

    }
}
