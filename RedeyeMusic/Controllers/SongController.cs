using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Artist;

namespace RedeyeMusic.Web.Controllers
{
    public class SongController : BaseController
    {
        private readonly ISongService songService;
        public SongController(ISongService songService)
        {
            this.songService = songService;
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
                Genres = await this.songService.SelectGenresAsync()
            };
            return View(viewModel);
        }
    }

}
