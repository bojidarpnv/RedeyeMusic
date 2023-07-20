using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Common;
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
            return await this.dbContext.Artists.AnyAsync(a=>a.Name == artistName);
        }

        public Task CreateFirstSongAsync(string userId, string userName, AddSongFormModel song)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> GetArtistIdByUserIdAsync(string userId)
        {
            Artist? artist = await this.dbContext
                .Artists
                .FirstOrDefaultAsync(a => a.ApplicationUserId.ToString() == userId);
            if(artist == null)
            {
                return null;
            }
            return artist.Id;
        }
        
    }
}
