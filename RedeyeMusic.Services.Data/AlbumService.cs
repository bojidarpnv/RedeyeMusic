﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
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
        public async Task<IEnumerable<AlbumSelectViewModel>> AllByArtistIdAsync(int artistId)
        {
            IEnumerable<AlbumSelectViewModel> allArtistAlbums = await this.dbContext
                .Albums
                .Where(a => a.IsDeleted == false)
                .Where(a => a.ArtistId == artistId)
                .Select(a => new AlbumSelectViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ArtistId = a.ArtistId,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl
                })
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
                ImageUrl = album.ImageUrl,
                Songs = songs.Select(s => new IndexViewModel()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Duration = s.Duration,
                    ImageUrl = s.ImageUrl,
                    ListenCount = s.ListenCount,
                    Lyrics = s.Lyrics,
                    ArtistName = s.Artist.Name,
                    GenreName = s.Genre.Name,
                    Mp3FilePath = s.FilePath

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
                ImageUrl = album.ImageUrl,
                SelectedSongIds = selectedSongIds, // Set the default selected song ID here
                Songs = songItems
            };
        }

        public async Task UpdateAlbumAsync(EditAlbumFormModel viewModel)
        {
            Album album = await this.dbContext.Albums
                .Include(a => a.Songs)
                .Where(s => s.IsDeleted == false)
                .FirstAsync(a => a.Id == viewModel.Id);

            if (album == null)
            {
                // Handle the case when the album is not found
                throw new ArgumentException("Album not found.");
            }

            // Update the album properties
            album.Name = viewModel.Name;
            album.Description = viewModel.Description;

            // Get the selected song IDs from the form (if any)
            var selectedSongIds = viewModel.SelectedSongIds ?? new List<int>();

            // Get the existing selected songs (if any) to be removed from their old albums
            var existingSelectedSongs = album.Songs
                .Where(s => s.IsDeleted == false)
                .ToList();

            foreach (var existingSelectedSong in existingSelectedSongs)
            {
                if (!selectedSongIds.Contains(existingSelectedSong.Id))
                {
                    // The song is no longer selected for this album, remove it from the album
                    album.Songs.Remove(existingSelectedSong);
                }
            }

            // Get the new selected songs to add to the album
            var newSelectedSongs = await this.dbContext.Songs
                .Where(s => selectedSongIds.Contains(s.Id))
                .ToListAsync();

            // Add the selected songs to the album
            foreach (var newSelectedSong in newSelectedSongs)
            {
                if (!album.Songs.Contains(newSelectedSong))
                {
                    // Avoid adding duplicate songs
                    album.Songs.Add(newSelectedSong);
                }
            }

            await this.dbContext.SaveChangesAsync();
        }

    }
}
