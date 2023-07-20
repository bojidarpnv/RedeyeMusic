using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Album;

namespace RedeyeMusic.Services.Data
{
    public class AlbumService : IAlbumService
    {
        private readonly RedeyeMusicDbContext dbContext;
        public AlbumService(RedeyeMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAlbum(AlbumSelectViewModel albumSelectViewModel)
        {
            Album album = new Album()
            {
                Name = albumSelectViewModel.Name,
            };
            await this.dbContext.Albums.AddAsync(album);
            await this.dbContext.SaveChangesAsync();
        }



        public async Task<ICollection<AlbumSelectViewModel>> SelectAlbumsByArtistIdAsync(int artistId)
        {
            ICollection<AlbumSelectViewModel> albums = await this.dbContext
                .Albums
                .Select(a => new AlbumSelectViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ArtistId = a.ArtistId,
                })
                .Where(al => al.ArtistId == artistId)
                .ToArrayAsync();
            return albums;
        }
    }
}
