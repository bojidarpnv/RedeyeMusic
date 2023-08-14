using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Playlist;
using static RedeyeMusic.Common.NotificationMessagesConstants;

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
                    await this.playlistService.UpdatePlaylists(songId, selectedPlaylistIds, this.User.GetId());
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

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            string userId = this.User.GetId();
            IEnumerable<PlaylistViewModel> playlists = await this.playlistService.GetAllPlaylistsByUserIdAsync(userId);
            return View(playlists);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            bool isOwner = await this.playlistService.IsUserOwnerOfPlaylist(id, this.User.GetId());
            if (!isOwner && !this.User.IsAdmin())
            {
                TempData[ErrorMessage] = "You are not the owner of this playlist";
                return RedirectToAction("Mine", "Playlist");
            }
            PlaylistViewModel playlist = await this.playlistService.GetPlaylistByIdAsync(id);
            return View(playlist);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromPlaylist(int playlistId, int songId)
        {
            bool isOwner = await this.playlistService.IsUserOwnerOfPlaylist(playlistId, this.User.GetId());
            if (!isOwner && !this.User.IsAdmin())
            {
                TempData[ErrorMessage] = "You are not the owner of this playlist";
                return RedirectToAction("Mine", "Playlist");
            }
            await this.playlistService.RemoveSongFromPlaylist(playlistId, songId);
            TempData[SuccessMessage] = "Successfully removed from playlist!";
            return RedirectToAction("Details", "Playlist", new { id = playlistId });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int playlistId)
        {
            bool isOwner = await this.playlistService.IsUserOwnerOfPlaylist(playlistId, this.User.GetId());
            if (!isOwner && !this.User.IsAdmin())
            {
                TempData[ErrorMessage] = "You are not the owner of this playlist";
                return RedirectToAction("Mine", "Playlist");
            }
            await this.playlistService.DeletePlaylistWithIdAsync(playlistId);
            this.TempData[SuccessMessage] = "Successfully deleted playlist!";
            return RedirectToAction("Mine", "Playlist");

        }
    }
}
