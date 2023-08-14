using RedeyeMusic.Web.ViewModels.Home;
using RedeyeMusic.Web.ViewModels.Playlist;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IPlaylistService
    {
        Task<IEnumerable<PlaylistViewModel>> GetAllPlaylistsByUserIdAsync(string userId);
        Task<IndexViewModel> GetSongToAddToPlaylistByIdAsync(int songId);
        Task<int> CreatePlaylistAsync(string playlistName, int songId, string userId);
        Task AddSongToPlaylistAsync(int songId, int playlistId);
        Task UpdatePlaylists(int songId, List<int> selectedPlaylistsIds, string userId);
        Task<PlaylistViewModel> GetPlaylistByIdAsync(int playlistId);
        Task<bool> IsUserOwnerOfPlaylist(int playlistId, string userId);
        Task RemoveSongFromPlaylist(int playlistId, int songId);
        Task DeletePlaylistWithIdAsync(int playlistId);
    }
}
