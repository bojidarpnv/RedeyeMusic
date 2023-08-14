using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Genre;
using static RedeyeMusic.Common.NotificationMessagesConstants;
namespace RedeyeMusic.Web.Areas.Admin.Controllers
{
    public class GenreController : BaseAdminController
    {
        private readonly IGenreService genreService;
        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(GenreFormModel formModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.genreService.CreateGenreAsync(formModel);
                    this.TempData[SuccessMessage] = "Successfully added genre";
                    return RedirectToAction("All", "Genre");
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }

            return View(formModel);

        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<GenreSelectViewModel> genres = await this.genreService.SelectGenresAsync();
            return View(genres);
        }
    }
}
