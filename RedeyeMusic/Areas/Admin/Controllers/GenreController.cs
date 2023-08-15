using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Genre;
using static RedeyeMusic.Common.GeneralApplicationConstants;
using static RedeyeMusic.Common.NotificationMessagesConstants;
namespace RedeyeMusic.Web.Areas.Admin.Controllers
{
    public class GenreController : BaseAdminController
    {
        private readonly IGenreService genreService;
        private readonly IMemoryCache memoryCache;
        public GenreController(IGenreService genreService, IMemoryCache memoryCache)
        {
            this.genreService = genreService;
            this.memoryCache = memoryCache;
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
                    this.memoryCache.Remove(GenresCacheKey);
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
        [Route("Genre/All")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> All()
        {
            IEnumerable<GenreSelectViewModel> genres =
                this.memoryCache.Get<IEnumerable<GenreSelectViewModel>>(GenresCacheKey);
            if (genres == null)
            {
                genres = await this.genreService.SelectGenresAsync();
                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(GenresCacheDurationMinutes));

                this.memoryCache.Set(GenresCacheKey, genres, cacheOptions);
            }
            return View(genres);
        }
    }
}
