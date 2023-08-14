using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Home;
using RedeyeMusic.Web.ViewModels.Playlist;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data
{
    public class PlaylistService : IPlaylistService
    {
        private readonly RedeyeMusicDbContext dbContext;
        private readonly IArtistService artistService;
        private readonly IAlbumService albumService;
        public PlaylistService(RedeyeMusicDbContext dbContext, IArtistService artistService, IAlbumService albumService)
        {
            this.dbContext = dbContext;
            this.artistService = artistService;
            this.albumService = albumService;
        }

        public async Task AddSongToPlaylistAsync(int songId, int playlistId)
        {
            Playlist playlist = await this.dbContext
                .Playlists
                .FirstAsync(p => p.IsDeleted == false && p.Id == playlistId);
            Song song = await this.dbContext
                .Songs
                .FirstAsync(s => s.IsDeleted == false && s.Id == songId);
            if (playlist != null)
            {
                playlist.PlaylistsSongs.Add(new PlaylistsSongs
                {
                    SongId = songId,
                    PlaylistId = playlistId
                });

            }
            await this.dbContext.SaveChangesAsync();

        }

        public async Task<int> CreatePlaylistAsync(string playlistName, int songId, string userId)
        {
            Song currentSong = await this.dbContext
                .Songs
                .FirstAsync(s => s.IsDeleted == false && s.Id == songId);
            Playlist playlist = new Playlist()
            {
                Name = playlistName,
                ApplicationUserId = Guid.Parse(userId),
                PlaylistsSongs = new List<PlaylistsSongs>
                {
                    new PlaylistsSongs{SongId = songId}
                }
            };

            await this.dbContext.Playlists.AddAsync(playlist);
            await this.dbContext.SaveChangesAsync();

            return playlist.Id;
        }

        public async Task DeletePlaylistWithIdAsync(int playlistId)
        {
            Playlist playlist = await this.dbContext
                .Playlists
                .FirstAsync(p => p.Id == playlistId && p.IsDeleted == false);
            playlist.IsDeleted = true;
            var playlistSong = await this.dbContext.PlaylistsSongs
                 .FirstOrDefaultAsync(p => p.PlaylistId == playlist.Id);
            if (playlistSong != null)
            {
                this.dbContext.PlaylistsSongs.Remove(playlistSong);
            }
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlaylistViewModel>> GetAllPlaylistsByUserIdAsync(string userId)
        {
            IEnumerable<Song> songs = await this.dbContext
                .Songs
                .Include(s => s.Genre)
                .Where(s => s.IsDeleted == false)
                .ToArrayAsync();

            IEnumerable<PlaylistViewModel> playlists = await this.dbContext
                .Playlists
                .Include(p => p.PlaylistsSongs)
                .ThenInclude(ps => ps.Song)
                .Where(p => p.IsDeleted == false && p.ApplicationUserId == Guid.Parse(userId))
                .Select(p => new PlaylistViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Songs = p.PlaylistsSongs.Select(ps => new SongDetailsOnPlaylistViewModel()
                    {
                        Id = ps.Song.Id,
                        Title = ps.Song.Title,
                        Duration = ps.Song.Duration,
                        ImageUrl = ps.Song.ImageUrl,
                        ListenCount = ps.Song.ListenCount,
                        Lyrics = ps.Song.Lyrics!,
                        ArtistName = ps.Song.Artist.Name,
                        GenreName = ps.Song.Genre.Name,
                        Mp3FilePath = ps.Song.Mp3FilePath,
                        IsDeleted = ps.Song.IsDeleted,
                    })

                })
                .ToArrayAsync();
            return playlists;

        }

        public async Task<PlaylistViewModel> GetPlaylistByIdAsync(int playlistId)
        {
            Playlist playlist = await this.dbContext
                .Playlists
                .Where(p => p.IsDeleted == false)
                .Include(ps => ps.PlaylistsSongs)
                .ThenInclude(s => s.Song)
                .ThenInclude(ss => ss.Genre)
                .Include(p => p.PlaylistsSongs)
                .ThenInclude(s => s.Song)
                .ThenInclude(ss => ss.Artist)
                .Include(p => p.PlaylistsSongs)
                .ThenInclude(s => s.Song)
                .ThenInclude(ss => ss.Album)
                .FirstAsync(p => p.Id == playlistId);
            List<SongDetailsOnPlaylistViewModel> songModels = new List<SongDetailsOnPlaylistViewModel>();
            PlaylistViewModel viewModel;
            if (playlist.PlaylistsSongs.Any())
            {
                IEnumerable<Song> songs = playlist.PlaylistsSongs
                .Where(p => p.PlaylistId == playlistId && p.Song.IsDeleted == false)
                .Select(s => s.Song)
                .ToList();
                foreach (Song song in songs)
                {
                    SongDetailsOnPlaylistViewModel songModel = new SongDetailsOnPlaylistViewModel
                    {
                        Id = song.Id,
                        Title = song.Title,
                        Duration = song.Duration,
                        ImageUrl = song.ImageUrl,
                        ListenCount = song.ListenCount,
                        Lyrics = song.Lyrics!,
                        ArtistName = song.Artist.Name,
                        GenreName = song.Genre.Name,
                        Mp3FilePath = song.Mp3FilePath

                    };
                    songModels.Add(songModel);
                }
            }
            if (songModels.Any())
            {
                viewModel = new PlaylistViewModel
                {
                    Id = playlist.Id,
                    Name = playlist.Name,
                    Songs = songModels
                };
            }
            else
            {
                viewModel = new PlaylistViewModel
                {
                    Id = playlist.Id,
                    Name = playlist.Name,
                    Songs = new List<SongDetailsOnPlaylistViewModel>()
                };
            }


            return viewModel;

        }

        public async Task<IndexViewModel> GetSongToAddToPlaylistByIdAsync(int songId)
        {
            Song song = await this.dbContext
                .Songs
                .FirstAsync(s => s.IsDeleted == false && s.Id == songId);

            IndexViewModel songModel = new IndexViewModel
            {
                Id = song.Id,
                Title = song.Title,
                Duration = song.Duration,
                ImageUrl = song.ImageUrl,
                ListenCount = song.ListenCount,
                Lyrics = song.Lyrics!,
                ArtistName = song.Artist.Name,
                GenreName = song.Genre.Name,
                Mp3FilePath = song.Mp3FilePath
            };

            return songModel;
        }

        public async Task<bool> IsUserOwnerOfPlaylist(int playlistId, string userId)
        {
            Playlist playlist = await this.dbContext
                .Playlists
                .FirstAsync(p => p.IsDeleted == false && p.Id == playlistId);
            return playlist.ApplicationUserId.ToString() == userId;
        }

        public async Task RemoveSongFromPlaylist(int playlistId, int songId)
        {
            PlaylistsSongs playlistSong = await this.dbContext
                .PlaylistsSongs
                .FirstAsync(ps => ps.PlaylistId == playlistId && ps.SongId == songId);

            this.dbContext.PlaylistsSongs.Remove(playlistSong);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task UpdatePlaylists(int songId, List<int> selectedPlaylistsIds, string userId)
        {
            Song song = await this.dbContext
                .Songs
                .FirstAsync(s => s.Id == songId && !s.IsDeleted);



            IEnumerable<Playlist> playlists = await this.dbContext.Playlists
                .Where(p => p.IsDeleted == false && p.ApplicationUserId.ToString() == userId)
                .Include(p => p.PlaylistsSongs)
                .ToListAsync();


            foreach (var playlist in playlists)
            {
                if (selectedPlaylistsIds.Contains(playlist.Id))
                {
                    if (!playlist.PlaylistsSongs.Any(ps => ps.SongId == songId))
                    {
                        playlist.PlaylistsSongs.Add(new PlaylistsSongs
                        {
                            PlaylistId = playlist.Id,
                            SongId = songId
                        });
                    }
                }
                else
                {
                    {
                        var playlistsSongToRemove = playlist.PlaylistsSongs.FirstOrDefault(ps => ps.SongId == songId && ps.PlaylistId == playlist.Id);
                        if (playlistsSongToRemove != null)
                        {
                            playlist.PlaylistsSongs.Remove(playlistsSongToRemove);
                        }
                    }

                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
