using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Services.Mapping;
using RedeyeMusic.Web.ViewModels.Album;
using RedeyeMusic.Web.ViewModels.Home;
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

        public async Task<int> AddAlbum(AlbumFormModel albumViewModel, int artistId)
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
            return album.Id;
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

            return (album.Id);
        }

        public async Task<ICollection<AlbumSelectViewModel>> SelectAlbumsByArtistIdAsync(int artistId)
        {
            ICollection<AlbumSelectViewModel> albums = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .To<AlbumSelectViewModel>()
                .Where(al => al.ArtistId == artistId)
                .ToArrayAsync();
            return albums;
        }
        public async Task<IEnumerable<AlbumSelectViewModel>> AllByArtistIdAsync(int artistId)
        {
            IEnumerable<AlbumSelectViewModel> allArtistAlbums = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .Where(a => a.ArtistId == artistId)
                .To<AlbumSelectViewModel>()
                .ToArrayAsync();

            return allArtistAlbums;
        }
        public async Task<bool> ExistsById(int albumId)
        {
            bool result = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .AnyAsync(a => a.Id == albumId);
            return result;
        }
        public async Task<AlbumDetailsViewModel> GetDetailsByIdAsync(int albumId)
        {
            Album album = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .Include(a => a.Artist)
                .ThenInclude(a => a.ApplicationUser)
                .FirstAsync(a => a.Id == albumId);
            IEnumerable<Song> songs = await this.dbContext
                .Songs
                .Include(s => s.Genre)
                .Where(s => s.IsDeleted == false)
                .Where(s => s.AlbumId == albumId)
                .ToArrayAsync();

            return new AlbumDetailsViewModel()
            {
                Id = albumId,
                Name = album.Name,
                ArtistId = album.ArtistId,
                ArtistName = album.Artist.Name,
                Description = album.Description,
                ImageUrl = album.ImageUrl!,
                Songs = songs.Select(s => new IndexViewModel()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Duration = s.Duration,
                    ImageUrl = s.ImageUrl,
                    ListenCount = s.ListenCount,
                    Lyrics = s.Lyrics!,
                    ArtistName = s.Artist.Name,
                    GenreName = s.Genre.Name,
                    Mp3FilePath = s.Mp3FilePath

                })
            };
        }
        public async Task<EditAlbumFormModel> GetAlbumForEditAsync(int albumId)
        {

            Album album = await this.dbContext.Albums
                .Where(a => a.IsDeleted == false)
                .Include(a => a.Artist)
                .ThenInclude(a => a.ApplicationUser)
                .FirstAsync(a => a.Id == albumId);

            IEnumerable<Song> songs = await this.dbContext.Songs
                .Where(s => s.IsDeleted == false)
                .Where(s => s.ArtistId == album.ArtistId)
                .ToArrayAsync();

            List<SelectListItem> songItems = songs
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                })
                .ToList();
            List<int> selectedSongIds = album.Songs.Select(s => s.Id).ToList();
            return new EditAlbumFormModel
            {
                Id = album.Id,
                Name = album.Name,
                Description = album.Description,
                ImageUrl = album.ImageUrl!,
                SelectedSongIds = selectedSongIds,
                Songs = songItems
            };
        }

        public async Task UpdateAlbumAsync(EditAlbumFormModel viewModel)
        {
            Album album = await this.dbContext.Albums
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(a => a.Id == viewModel.Id && a.IsDeleted == false);

            if (album == null)
            {

                throw new ArgumentException("Album not found.");
            }


            album.Name = viewModel.Name;
            album.Description = viewModel.Description;


            var selectedSongIds = viewModel.SelectedSongIds ?? new List<int>();

            // Get the existing selected songs to remove from album
            var existingSelectedSongs = album.Songs
                .Where(s => s.IsDeleted == false)
                .ToList();

            foreach (var existingSelectedSong in existingSelectedSongs)
            {
                if (!selectedSongIds.Contains(existingSelectedSong.Id))
                {

                    album.Songs.Remove(existingSelectedSong);
                }
            }


            var newSelectedSongs = await this.dbContext.Songs
                .Where(s => selectedSongIds.Contains(s.Id))
                .ToListAsync();


            foreach (var newSelectedSong in newSelectedSongs)
            {
                if (!album.Songs.Contains(newSelectedSong))
                {

                    album.Songs.Add(newSelectedSong);
                }
            }

            await this.dbContext.SaveChangesAsync();
        }
        public async Task DeleteAlbumByIdAsync(int albumId)
        {
            Album album = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .FirstAsync(a => a.Id == albumId);

            List<Song> songsFromAlbum = await this.dbContext.Songs
                .Where(s => s.AlbumId == albumId && s.IsDeleted == false)
                .ToListAsync();
            foreach (var song in songsFromAlbum)
            {
                song.IsDeleted = true;
            }
            album.IsDeleted = true;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string> GetAlbumNameById(int albumId)
        {
            Album album = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .FirstAsync(a => a.Id == albumId);
            return album.Name;
        }
    }
}
