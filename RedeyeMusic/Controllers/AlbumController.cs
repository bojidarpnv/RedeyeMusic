using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Album;

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
                return BadRequest(ModelState); // Return 400 Bad Request with validation errors if the model state is not valid
            }

            try
            {
                await this.albumService.AddAlbum(viewModel, (int)artistId);

                return Ok(new { albumId = viewModel.Id }); // Return 200 OK with the newly created album's ID in the response
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, "An error occurred while creating the album."); // Return 500 Internal Server Error if an exception occurs during album creation
            }
        }
    }
}
