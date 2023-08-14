using RedeyeMusic.Web.ViewModels.Home;
using RedeyeMusic.Web.ViewModels.Playlist;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IPlaylistService
    {
        public Task<IEnumerable<PlaylistViewModel>> GetAllPlaylistsByUserIdAsync(string userId);
        public Task<IndexViewModel> GetSongToAddToPlaylistByIdAsync(int songId);
        public Task<int> CreatePlaylistAsync(string playlistName, int songId, string userId);
        public Task AddSongToPlaylistAsync(int songId, int playlistId);
        public Task UpdatePlaylists(int songId, List<int> selectedPlaylistsIds);
    }
}
