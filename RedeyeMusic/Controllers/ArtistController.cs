using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Artist;
using static RedeyeMusic.Common.NotificationMessagesConstants;

namespace RedeyeMusic.Web.Controllers
{
    public class ArtistController : BaseController
    {
        private readonly IArtistService artistService;
        private readonly IGenreService genreService;
        private readonly ILogger<ArtistController> logger;
        private readonly IWebHostEnvironment env;

        public ArtistController(IArtistService artistService, IGenreService genreService, ILogger<ArtistController> logger, IWebHostEnvironment env)
        {
            this.artistService = artistService;
            this.genreService = genreService;
            this.logger = logger;
            this.env = env;

        }
        [HttpGet]
        public async Task<IActionResult> Become(BecomeArtistFormModel artistModel)
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(userId);
            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an artist";
                return this.RedirectToAction("Add", "Song");
            }

            return View(artistModel);

        }
        [HttpPost]
        public async Task<IActionResult> Become(BecomeArtistFormModel artistModel, string artistName)
        {
            string userId = User.GetId();
            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(userId);
            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an artist";
                return this.RedirectToAction("Index", "Home");
            }
            if (await this.artistService.ArtistNameExistsAsync(artistName))
            {
                ModelState.AddModelError(nameof(artistModel.ArtistName), "Artist name already exists. Enter another one.");
            }
            if (!ModelState.IsValid)
            {
                return View(artistModel);
            }

            await this.artistService.CreateAsync(userId, artistModel);

            return RedirectToAction("AddFirstSong", "Song");
        }


    }
}
