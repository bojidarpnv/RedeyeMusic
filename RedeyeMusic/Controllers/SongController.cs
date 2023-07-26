using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Services.Data.Models.Song;
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
        [HttpPost]
        public async Task<IActionResult> Add(AddSongFormModel songModel)
        {

            string userId = this.User.GetId();
            int? artistId = await this.artistService.GetArtistIdByUserIdAsync(userId);
            songModel.Genres = await this.genreService.SelectGenresAsync();
            songModel.Albums = await this.albumService.SelectAlbumsByArtistIdAsync((int)artistId);
            //if albumId is 0 it is because of being an already existing album, getting the id here from the Name and Description, cannot properly pass Id with JS;
            if (songModel.AlbumId == 0)
            {
                songModel.AlbumId = await this.albumService.GetAlbumId(songModel);
            }
            //if albumName and albumDescription are null it is because of it being a new Album so we need to get them from the new album.
            if (songModel.AlbumName == null && songModel.AlbumDescription == null)
            {
                songModel = await this.albumService.GetAlbumDescriptionAndNameById(songModel.AlbumId, songModel);
            }

            if (!ModelState.IsValid)
            {
                return View(songModel);
            }
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
                try
                {
                    await this.songService.AddSongAsync(songModel, (int)artistId);
                    TempData[SuccessMessage] = "Successfully added a song!";
                }
                catch (Exception _)
                {
                    this.ModelState.AddModelError(string.Empty, "Unexpected error occurrer while trying to add your new Song! Please try again later!");
                    return View(songModel);
                }
            }

            return RedirectToAction("Mine", "Song");
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
            else
            {
                return View(songModel);
            }
            return RedirectToAction("Mine", "Song");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AllSongsQueryModel queryModel)
        {
            AllSongsSearchedModel searchResult = new AllSongsSearchedModel();
            if (!string.IsNullOrEmpty(queryModel.SearchString))
            {
                searchResult = await this.songService.SearchSongsAsync(queryModel);
            }
            return View(searchResult);
        }
    }

}
