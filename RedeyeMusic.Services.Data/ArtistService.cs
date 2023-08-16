using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Artist;

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
        public async Task CreateAsync(string userId, BecomeArtistFormModel formModel)
        {
            Artist artist = new Artist()
            {
                ApplicationUserId = Guid.Parse(userId),
                Name = formModel.ArtistName,
            };
            await this.dbContext.Artists.AddAsync(artist);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ArtistNameExistsAsync(string artistName)
        {
            return await this.dbContext.Artists.AnyAsync(a => a.Name == artistName);
        }



        public async Task<int> GetArtistIdByUserIdAsync(string userId)
        {
            Artist? artist = await this.dbContext
                .Artists
                .FirstOrDefaultAsync(a => a.ApplicationUserId.ToString() == userId);
            if (artist == null)
            {
                return 0;
            }
            return artist.Id;
        }

        public async Task<bool> ArtistNameAlreadyExists(string artistName)
        {
            return await this.dbContext.Artists.AnyAsync(a => a.Name == artistName);
        }
        public async Task<bool> DoesArtistHaveAnySongsAsync(int artistId)
        {
            return await this.dbContext.Songs.AnyAsync(s => s.ArtistId == artistId);
        }

        public async Task<bool> IsArtistWithIdOwnerOfSongWithIdAsync(int artistId, int songId)
        {
            Song? song = await this.dbContext
                .Songs
                .Where(s => s.IsDeleted == false)
                .FirstOrDefaultAsync(s => s.Id == songId);
            if (song == null)
            { return false; }
            return song.ArtistId == artistId;
        }
        public async Task<bool> IsArtistWithIdOwnerOfAlbumWithIdAsync(int artistId, int albumId)
        {

            Album? album = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .FirstOrDefaultAsync(a => a.Id == albumId);
            if (album == null)
            { return false; }
            return album.ArtistId == artistId;
        }

        public async Task<string> GetArtistNameByUserIdAsync(string userId)
        {
            var sanitizer = new HtmlSanitizer();
            Artist? artist = await this.dbContext
                .Artists
                .FirstOrDefaultAsync(a => a.ApplicationUserId == Guid.Parse(userId));

            if (artist == null)
            {
                return string.Empty;
            }
            return sanitizer.Sanitize(artist.Name);
        }
        public async Task<string> GetArtistNameByIdAsync(int artistId)
        {
            Artist? artist = await this.dbContext.Artists.
                FirstOrDefaultAsync(a => a.Id == artistId);
            if (artist == null)
            {
                return string.Empty;
            }
            return artist.Name;
        }
    }
}
