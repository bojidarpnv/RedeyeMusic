﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Album;


using static RedeyeMusic.Common.NotificationMessagesConstants;


namespace RedeyeMusic.Web.Controllers
{
    public class AlbumController : BaseController
    {
        private readonly IAlbumService albumService;
        private readonly IArtistService artistService;
        private readonly ISongService songService;
        private readonly IApplicationUserService applicationService;
        public AlbumController(IAlbumService albumService, IArtistService artistService, ISongService songService, IApplicationUserService applicationService)
        {
            this.albumService = albumService;
            this.artistService = artistService;
            this.songService = songService;
            this.applicationService = applicationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumFormModel viewModel)
        {
            string userId = this.User.GetId();
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (artistId != 0 || this.User.IsAdmin())
            {
                try
                {
                    int albumId = await this.albumService.AddAlbum(viewModel, (int)artistId);
                    this.TempData[SuccessMessage] = "Successfully added album";
                    return Ok(new { albumId = albumId });
                }
                catch (Exception)
                {
                    return StatusCode(500, "An error occurred while creating the album.");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Mine(AlbumSelectViewModel viewModel)
        {
            List<AlbumSelectViewModel> myAlbums =
                new List<AlbumSelectViewModel>();
            string userId = this.User.GetId();
            bool isUserArtist = await this.artistService
                .ArtistExistsByUserIdAsync(userId);
            try
            {
                if (isUserArtist || this.User.IsAdmin())
                {
                    int artistId = await this.artistService.GetArtistIdByUserIdAsync(userId);

                    myAlbums.AddRange(await this.albumService.AllByArtistIdAsync(artistId));
                }

                return this.View(myAlbums);
            }
            catch
            {
                return GeneralError();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            bool albumExists = await this.albumService.ExistsById(id);

            if (!albumExists)
            {
                this.TempData[ErrorMessage] = "Song with provided id does not exist!";
                return RedirectToAction("Index", "Home");
            }
            try
            {
                AlbumDetailsViewModel viewModel = await this.albumService
                .GetDetailsByIdAsync(id);
                return View(viewModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error ocurred! Please try again later";
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(this.User.GetId());
            bool isArtistOwner = await this.artistService.IsArtistWithIdOwnerOfAlbumWithIdAsync(artistId, id);
            if (isArtistOwner || this.User.IsAdmin())
            {
                EditAlbumFormModel albumViewModel = await this.albumService.GetAlbumForEditAsync(id);
                return View(albumViewModel);
            }
            else
            {
                this.TempData[ErrorMessage] = "You are not the owner of thisu album!";
                return RedirectToAction("Mine", "Album");
            }
        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditAlbumFormModel viewModel)
        {
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(this.User.GetId());
            bool isArtistOwner = await this.artistService.IsArtistWithIdOwnerOfAlbumWithIdAsync(artistId, viewModel.Id);
            if (!ModelState.IsValid)
            {

                viewModel.Songs = await this.songService.GetSongsDropdownItemsAsync(artistId);
                return View(viewModel);
            }
            if (isArtistOwner || this.User.IsAdmin())
            {
                await this.albumService.UpdateAlbumAsync(viewModel);
                this.TempData[SuccessMessage] = "Successfully edited Album";
                return RedirectToAction("Details", new { id = viewModel.Id });
            }
            else
            {
                this.TempData[ErrorMessage] = "You are not the owner of this album";
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, string password)
        {
            if (password == null)
            {
                GeneralError();
            }
            bool albumExists = await this.albumService.ExistsById(id);

            if (!albumExists)
            {
                this.TempData[ErrorMessage] = "Album with provided id does not exist!";
                return RedirectToAction("Index", "Home");
            }
            bool isUserArtist = await this.artistService.ArtistExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserArtist && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an artist in order to edit album info!";
                return RedirectToAction("Become", "Artist");
            }
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(this.User.GetId());
            bool isArtistOwner = await this.artistService.IsArtistWithIdOwnerOfAlbumWithIdAsync(artistId, id);
            if (!isArtistOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You are not the artist of this album!";
                return RedirectToAction("Mine", "Album");
            }
            var isPasswordValid = await this.applicationService.ValidatePasswordAsync(this.User.GetId(), password!);

            if (isPasswordValid)
            {
                await this.albumService.DeleteAlbumByIdAsync(id);
                this.TempData[SuccessMessage] = $"Successfully deleted album!!!";
                //return Ok(this.TempData);
                return RedirectToAction("Mine", "Album");
            }
            else
            {
                // Password is incorrect, show error message or redirect to a different page
                this.TempData[ErrorMessage] = "Incorrect password";
                return BadRequest("Incorrect password");

            }

        }


    }
}
