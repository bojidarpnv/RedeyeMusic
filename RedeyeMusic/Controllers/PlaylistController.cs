﻿using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Playlist;

namespace RedeyeMusic.Web.Controllers
{
    public class PlaylistController : BaseController
    {
        private readonly ISongService songService;
        private readonly IPlaylistService playlistService;
        public PlaylistController(ISongService songService, IPlaylistService playlistService)
        {
            this.songService = songService;
            this.playlistService = playlistService;

        }
        [HttpGet]
        public async Task<IActionResult> AddToPlaylist(int songId)
        {
            AddToPlaylistViewModel model = new AddToPlaylistViewModel
            {
                SongModel = await this.playlistService.GetSongToAddToPlaylistByIdAsync(songId),
                Playlists = await this.playlistService.GetAllPlaylistsAsync()
            };

            return PartialView("_AddToPlaylistModal", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(int songId, string playlistName, List<int> selectedPlaylistIds)
        {
            int playlistId = await this.playlistService.CreatePlaylistAsync(playlistName, songId, this.User.GetId());
            //await this.playlistService.AddSongToPlaylistAsync(songId, playlistId);

            selectedPlaylistIds.Add(playlistId);
            await Update(songId, selectedPlaylistIds);
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int songId, List<int> selectedPlaylistIds)
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
    }
}