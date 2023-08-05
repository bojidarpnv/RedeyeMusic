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
        public AlbumController(IAlbumService albumService, IArtistService artistService)
        {
            this.albumService = albumService;
            this.artistService = artistService;
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
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error ocurred! Please try again later";
            return RedirectToAction("Index", "Home");
        }
    }
}
