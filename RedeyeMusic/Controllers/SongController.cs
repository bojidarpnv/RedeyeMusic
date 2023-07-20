using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Song;
using static RedeyeMusic.Common.NotificationMessagesConstants;

namespace RedeyeMusic.Web.Controllers
{
    public class SongController : BaseController
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        private readonly IArtistService artistService;
        private readonly IAlbumService albumService;
        private readonly ILogger<ArtistController> logger;
        private readonly IWebHostEnvironment env;
        public SongController(ISongService songService, IGenreService genreService, IArtistService artistService, IAlbumService albumService, ILogger<ArtistController> logger, IWebHostEnvironment env)
        {
            this.songService = songService;
            this.genreService = genreService;
            this.artistService = artistService;
            this.albumService = albumService;
            this.logger = logger;
            this.env = env;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(this.User.GetId());
            int? artistId = await this.artistService.GetArtistIdByUserIdAsync(this.User.GetId());
            if (artistId == null)
            {
                throw new InvalidOperationException();
            }
            if (!isAgent)
            {
                TempData[ErrorMessage] = "You must become an artist in order to add Songs";
                return RedirectToAction("Become", "Artist");
            }
            AddSongFormModel viewModel = new AddSongFormModel()
            {
                Genres = await this.genreService.SelectGenresAsync(),
                Albums = await this.albumService.SelectAlbumsByArtistIdAsync((int)artistId)
            };
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> AddFirstSong()
        {
            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(this.User.GetId());
            if (!isAgent)
            {
                TempData[ErrorMessage] = "You must become an artist in order to add Songs";
                return RedirectToAction("Become", "Artist");
            }
            AddFirstSongFormModel viewModel = new AddFirstSongFormModel()
            {
                Genres = await this.genreService.SelectGenresAsync()
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddFirstSong(AddFirstSongFormModel songModel)
        {
            string userId = this.User.GetId();
            int? artistId = await this.artistService.GetArtistIdByUserIdAsync(userId);
            songModel.Genres = await this.genreService.SelectGenresAsync();
            if (artistId == null)
            {
                throw new InvalidOperationException();
            }

            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(userId);
            if (!isAgent)
            {
                TempData[ErrorMessage] = "You must become an artist in order to add Songs";
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
                await this.songService.AddFirstSongAsync(songModel, (int)artistId);
                TempData[SuccessMessage] = "Successfully added your first song!";
            }
            return RedirectToAction("Mine", "Song");
        }
    }

}
