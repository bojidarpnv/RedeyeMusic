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
        private readonly ILogger<ArtistController> logger;
        private readonly IWebHostEnvironment env;

        public ArtistController(IArtistService artistService, ILogger<ArtistController> logger, IWebHostEnvironment env)
        {
            this.artistService = artistService;
            this.logger = logger;
            this.env = env;

        }
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(userId);
            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an artist";
                return this.RedirectToAction("Index", "Home");
            }
            AddSongFormModel viewModel = new AddSongFormModel()
            {
                Genres = await this.artistService.SelectGenresAsync()
            };
            return View(viewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Become(AddSongFormModel songModel)
        {
            string userId = User.GetId();
            string userName = Environment.UserName;
            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(userId);
            if (isAgent)
            {
                TempData[ErrorMessage] = "You are already an artist";
                return this.RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(songModel);
            }
            if (ModelState.IsValid)
            {
                if (songModel.Mp3File != null)
                {
                    string folder = "songs/Mp3s/";
                    folder += Guid.NewGuid().ToString() + songModel.Mp3File.FileName;
                    songModel.FilePath = folder;
                    string serverFolder = Path.Combine(env.WebRootPath, folder);

                    await songModel.Mp3File.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                await this.artistService.CreateFirstSongAsync(userId, userName, songModel);

            }
            return RedirectToAction("Index", "Home");
        }


    }
}
