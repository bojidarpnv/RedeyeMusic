using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Services.Data.Models.Song;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Home;
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
        private readonly IApplicationUserService applicationUserService;
        private readonly ILogger<ArtistController> logger;
        private readonly IWebHostEnvironment env;
        public SongController(ISongService songService, IGenreService genreService, IArtistService artistService, IApplicationUserService applicationUserService, IAlbumService albumService, ILogger<ArtistController> logger, IWebHostEnvironment env)
        {
            this.songService = songService;
            this.genreService = genreService;
            this.artistService = artistService;
            this.albumService = albumService;
            this.applicationUserService = applicationUserService;
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
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(userId);
            bool doesArtistHaveAnySongs = await this.artistService.DoesArtistHaveAnySongsAsync(artistId);
            if (!doesArtistHaveAnySongs)
            {
                RedirectToAction("AddFirstSong", "Song");
            }
            songModel.Genres = await this.genreService.SelectGenresAsync();
            songModel.Albums = await this.albumService.SelectAlbumsByArtistIdAsync(artistId);
            //if albumId is 0 it is because of being an already existing album, getting the id here from the Name and Description, cannot properly pass Id with JS;
            if (songModel.AlbumId == 0)
            {
                songModel.AlbumId = await this.albumService.GetAlbumId(songModel);
            }
            //if albumName and albumDescription are null it is because of it being a new Album so we need to get them from the new album.
            if (songModel.AlbumName == null && songModel.AlbumDescription == null)
            {
                songModel = await this.albumService.GetAlbumDescriptionAndNameAndUrlById(songModel.AlbumId, songModel);
            }
            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(userId);
            if (!isAgent)
            {
                TempData[ErrorMessage] = "You must become an artist in order to add Songs";
            }

            if (ModelState.IsValid)
            {
                string fullFilePath = null;
                if (songModel.Mp3File != null)
                {
                    string folder = "songs/Mp3s/";
                    folder += Guid.NewGuid().ToString() + songModel.Mp3File.FileName;
                    songModel.FilePath = folder;
                    string serverFolder = Path.Combine(env.WebRootPath, folder);
                    fullFilePath = serverFolder;
                    using (FileStream stream = new FileStream(serverFolder, FileMode.Create))
                    {
                        await songModel.Mp3File.CopyToAsync(stream);
                    }
                }
                try
                {
                    int songId = await this.songService.AddSongAsync(songModel, (int)artistId, fullFilePath!);
                    TempData[SuccessMessage] = "Successfully added a song!";
                    return RedirectToAction("Details", "Song", new { id = songId });
                }
                catch (Exception _)
                {
                    this.ModelState.AddModelError(string.Empty, "Unexpected error occurrer while trying to add your new Song! Please try again later!");
                    return View(songModel);
                }

            }
            else
            {
                return View(songModel);
            }

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
                GeneralError();
            }

            bool isAgent = await this.artistService.ArtistExistsByUserIdAsync(userId);
            if (!isAgent)
            {
                TempData[ErrorMessage] = "You must become an artist in order to add Songs";
            }

            if (ModelState.IsValid)
            {
                string fullFilePath = null!;
                if (songModel.Mp3File != null)
                {
                    string folder = "songs/Mp3s/";
                    folder += Guid.NewGuid().ToString() + songModel.Mp3File.FileName;
                    songModel.FilePath = folder;
                    string serverFolder = Path.Combine(env.WebRootPath, folder);
                    fullFilePath = serverFolder;
                    using (FileStream stream = new FileStream(serverFolder, FileMode.Create))
                    {
                        await songModel.Mp3File.CopyToAsync(stream);
                    }

                }
                try
                {

                    int songId = await this.songService.AddFirstSongAsync(songModel, (int)artistId, fullFilePath);
                    TempData[SuccessMessage] = "Successfully added your first song!";
                    return RedirectToAction("Details", "Song", new { id = songId });
                }
                catch (Exception _)
                {
                    this.ModelState.AddModelError(string.Empty, "Unexpected error occurrer while trying to add your new Song! Please try again later!");
                    return View(songModel);
                }
            }
            else
            {
                return View(songModel);
            }

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AllSongsQueryModel queryModel)
        {
            AllSongsSearchedModel searchResult = new AllSongsSearchedModel();
            if (!string.IsNullOrEmpty(queryModel.SearchString))
            {
                try
                {

                    searchResult = await this.songService.SearchSongsAsync(queryModel);
                }
                catch
                {
                    GeneralError();
                }
            }
            return View(searchResult);
        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<IndexViewModel> mySongs =
                new List<IndexViewModel>();
            string userId = this.User.GetId();
            bool isUserArtist = await this.artistService
                .ArtistExistsByUserIdAsync(userId);
            try
            {
                if (isUserArtist)
                {
                    int artistId = await this.artistService.GetArtistIdByUserIdAsync(userId);

                    mySongs.AddRange(await this.songService.AllByArtistIdAsync(artistId));
                }

                return this.View(mySongs);
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
            bool songExists = await this.songService.ExistsById(id);

            if (!songExists)
            {
                this.TempData[ErrorMessage] = "Song with provided id does not exist!";
                return RedirectToAction("All, Song");
            }
            try
            {
                SongDetailsViewModel viewModel = await this.songService
                .GetDetailsByIdAsync(id);
                return View(viewModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool songExists = await this.songService.ExistsById(id);

            if (!songExists)
            {
                this.TempData[ErrorMessage] = "Song with provided id does not exist!";
                return RedirectToAction("All, Song");
            }
            bool isUserArtist = await this.artistService.ArtistExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserArtist)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit song info!";
                return RedirectToAction("Become", "Artist");
            }
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(this.User.GetId());
            bool isArtistOwner = await this.artistService.IsArtistWithIdOwnerOfSongWithIdAsync(artistId, id);
            if (!isArtistOwner)
            {
                this.TempData[ErrorMessage] = "You are not the artist of this song!";
                return RedirectToAction("Mine", "Song");
            }
            try
            {
                EditSongFormModel songFormModel = await this.songService
                .GetSongForEditByIdAsync(id);
                songFormModel.Albums = await this.albumService.SelectAlbumsByArtistIdAsync(artistId);
                songFormModel.Genres = await this.genreService.SelectGenresAsync();
                return View(songFormModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditSongFormModel songModel)
        {
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(this.User.GetId());
            if (!ModelState.IsValid)
            {
                songModel.Albums = await this.albumService.SelectAlbumsByArtistIdAsync(artistId);
                songModel.Genres = await this.genreService.SelectGenresAsync();
                return this.View(songModel);
            }
            bool songExists = await this.songService.ExistsById(id);

            if (!songExists)
            {
                this.TempData[ErrorMessage] = "Song with provided id does not exist!";
                return RedirectToAction("All, Song");
            }
            bool isUserArtist = await this.artistService.ArtistExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserArtist)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit song info!";
                return RedirectToAction("Become", "Artist");
            }

            bool isArtistOwner = await this.artistService.IsArtistWithIdOwnerOfSongWithIdAsync(artistId, id);
            if (!isArtistOwner)
            {
                this.TempData[ErrorMessage] = "You are not the artist of this song!";
                return RedirectToAction("Mine", "Song");
            }
            try
            {
                await this.songService.EditSongByIdAndModel(id, songModel);
                this.TempData[SuccessMessage] = $"Successfully edited song {songModel.Title}";
            }
            catch (Exception)
            {
                GeneralError();
                songModel.Albums = await this.albumService.SelectAlbumsByArtistIdAsync(artistId);
                songModel.Genres = await this.genreService.SelectGenresAsync();
                return View(songModel);
            }
            return RedirectToAction("Details", "Song", new { id });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, string password)
        {
            bool songExists = await this.songService.ExistsById(id);

            if (!songExists)
            {
                this.TempData[ErrorMessage] = "Song with provided id does not exist!";
                return RedirectToAction("All, Song");
            }
            bool isUserArtist = await this.artistService.ArtistExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserArtist)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit song info!";
                return RedirectToAction("Become", "Artist");
            }
            int artistId = await this.artistService.GetArtistIdByUserIdAsync(this.User.GetId());
            bool isArtistOwner = await this.artistService.IsArtistWithIdOwnerOfSongWithIdAsync(artistId, id);
            if (!isArtistOwner)
            {
                this.TempData[ErrorMessage] = "You are not the artist of this song!";
                return RedirectToAction("Mine", "Song");
            }
            var isPasswordValid = await this.applicationUserService.ValidatePasswordAsync(this.User.GetId(), password);

            if (isPasswordValid)
            {
                await this.songService.DeleteSongByIdAsync(id);
                this.TempData[SuccessMessage] = $"Successfully deleted song";

                return RedirectToAction("Mine", "Song");
            }
            else
            {
                // Password is incorrect, show error message or redirect to a different page
                this.TempData[ErrorMessage] = "Incorrect password";
                return RedirectToAction("Details", "Song", new { id = id });
            }

        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error ocurred! Please try again later";
            return RedirectToAction("Index", "Home");
        }
    }

}
