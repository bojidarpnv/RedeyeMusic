using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Playlist;

namespace RedeyeMusic.Web.Controllers
{
    public class PlaylistController : BaseController
    {
        private readonly ISongService songService;
        private readonly IPlaylistService playlistService;
        private readonly IArtistService artistService;
        public PlaylistController(ISongService songService, IPlaylistService playlistService, IArtistService artistService)
        {
            this.songService = songService;
            this.playlistService = playlistService;
            this.artistService = artistService;

        }
        [HttpGet]
        public async Task<IActionResult> AddToPlaylist(int songId)
        {
            AddToPlaylistViewModel model = new AddToPlaylistViewModel
            {
                SongModel = await this.playlistService.GetSongToAddToPlaylistByIdAsync(songId),
                Playlists = await this.playlistService.GetAllPlaylistsByUserIdAsync(this.User.GetId())
            };

            return PartialView("_AddToPlaylistModal", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int songId, string playlistName, List<int> selectedPlaylistIds)
        {
            bool isArtist = await this.artistService.ArtistExistsByUserIdAsync(this.User.GetId());
            if (!isArtist || this.User.IsAdmin())
            {
                int playlistId = await this.playlistService.CreatePlaylistAsync(playlistName, songId, this.User.GetId());
                //await this.playlistService.AddSongToPlaylistAsync(songId, playlistId);

                selectedPlaylistIds.Add(playlistId);
                await Update(songId, selectedPlaylistIds);
            }
            else
            {
                return BadRequest();
            }
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int songId, List<int> selectedPlaylistIds)
        {
            bool isArtist = await this.artistService.ArtistExistsByUserIdAsync(this.User.GetId());
            if (!isArtist || this.User.IsAdmin())
            {
                try
                {
                    await this.playlistService.UpdatePlaylists(songId, selectedPlaylistIds);
                    return Json(new { success = true });
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
