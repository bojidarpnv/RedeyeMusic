using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Home;
using RedeyeMusic.Web.ViewModels.Playlist;

namespace RedeyeMusic.Services.Data
{
    public class PlaylistService : IPlaylistService
    {
        private readonly RedeyeMusicDbContext dbContext;
        public PlaylistService(RedeyeMusicDbContext dbContext)
        {
            this.dbContext = dbContext;
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

        public async Task<IEnumerable<PlaylistViewModel>> GetAllPlaylistsByUserIdAsync(string userId)
        {
            IEnumerable<Song> songs = await this.dbContext
                .Songs
                .Include(s => s.Genre)
                .Where(s => s.IsDeleted == false)
                .ToArrayAsync();

            IEnumerable<PlaylistViewModel> playlists = await this.dbContext
                .Playlists
                .Where(p => p.IsDeleted == false && p.ApplicationUserId == Guid.Parse(userId))
                .Select(p => new PlaylistViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Songs = p.PlaylistsSongs.Select(ps => new IndexViewModel()
                    {
                        Id = ps.Song.Id,
                        Title = ps.Song.Title,
                        Duration = ps.Song.Duration,
                        ImageUrl = ps.Song.ImageUrl,
                        ListenCount = ps.Song.ListenCount,
                        Lyrics = ps.Song.Lyrics!,
                        ArtistName = ps.Song.Artist.Name,
                        GenreName = ps.Song.Genre.Name,
                        Mp3FilePath = ps.Song.Mp3FilePath

                    })
                })
                .ToArrayAsync();
            return playlists;

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

        public async Task UpdatePlaylists(int songId, List<int> selectedPlaylistsIds)
        {
            Song song = await this.dbContext
                .Songs
                .FirstAsync(s => s.Id == songId && !s.IsDeleted);



            IEnumerable<Playlist> playlists = await this.dbContext.Playlists
                .Where(p => p.IsDeleted == false)
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
                    var playlistsSongToRemove = playlist.PlaylistsSongs.FirstOrDefault(ps => ps.SongId == songId);
                    if (playlistsSongToRemove != null)
                    {
                        playlist.PlaylistsSongs.Remove(playlistsSongToRemove);
                    }
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
