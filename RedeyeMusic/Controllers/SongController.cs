using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Web.Controllers
{
    public class SongController : BaseController
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        public SongController(ISongService songService, IGenreService genreService)
        {
            this.songService = songService;
            this.genreService = genreService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddSongFormModel viewModel = new AddSongFormModel()
            {
                Genres = await this.genreService.SelectGenresAsync()
            };
            return View(viewModel);
        }
    }

}
