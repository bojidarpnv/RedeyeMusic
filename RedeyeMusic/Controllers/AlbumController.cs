using Microsoft.AspNetCore.Authorization;
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
        public AlbumController(IAlbumService albumService, IArtistService artistService, ISongService songService)
        {
            this.albumService = albumService;
            this.artistService = artistService;
            this.songService = songService;

        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumFormModel viewModel)
        {
            string userId = this.User.GetId();
            int? artistId = await this.artistService.GetArtistIdByUserIdAsync(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await this.albumService.AddAlbum(viewModel, (int)artistId);
                this.TempData[SuccessMessage] = "Successfully added album";
                return Ok(new { albumId = viewModel.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the album.");
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
                if (isUserArtist)
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
            EditAlbumFormModel albumViewModel = await this.albumService.GetAlbumForEditAsync(id);
            return View(albumViewModel);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditAlbumFormModel viewModel)
        {
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(this.User.GetId());
            if (!ModelState.IsValid)
            {
                // Populate the songs dropdown again in case of model validation error
                viewModel.Songs = await this.songService.GetSongsDropdownItemsAsync(artistId);
                return View(viewModel);
            }

            await this.albumService.UpdateAlbumAsync(viewModel);
            this.TempData[SuccessMessage] = "Successfully edited Album";
            return RedirectToAction("Details", new { id = viewModel.Id });
        }




    }
}
