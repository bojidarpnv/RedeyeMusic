using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Album;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data
{
    public class AlbumService : IAlbumService
    {
        private readonly RedeyeMusicDbContext dbContext;
        public AlbumService(RedeyeMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAlbum(AlbumFormModel albumViewModel, int artistId)
        {
            Album album = new Album()
            {
                Name = albumViewModel.Name,
                ArtistId = artistId,
                Description = albumViewModel.Description,
                ImageUrl = albumViewModel.ImageUrl
            };
            await this.dbContext.Albums.AddAsync(album);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AddSongFormModel> GetAlbumDescriptionAndNameAndUrlById(int albumId, AddSongFormModel songModel)
        {

            Album album = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .FirstAsync(a => a.Id == albumId);
            songModel.AlbumDescription = album.Description;
            songModel.AlbumName = album.Name;
            songModel.AlbumImageUrl = album.ImageUrl;
            return songModel;
        }
        public async Task<int> GetAlbumId(AddSongFormModel songModel)
        {
            Album album = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .FirstAsync(a => a.Name == songModel.AlbumName && a.Description == songModel.AlbumDescription);

            return album.Id;
        }

        public async Task<ICollection<AlbumSelectViewModel>> SelectAlbumsByArtistIdAsync(int artistId)
        {
            ICollection<AlbumSelectViewModel> albums = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .Select(a => new AlbumSelectViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ArtistId = a.ArtistId,
                    ImageUrl = a.ImageUrl,
                })
                .Where(al => al.ArtistId == artistId)
                .ToArrayAsync();
            return albums;
        }
    }
}
