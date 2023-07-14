using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;

namespace RedeyeMusic.Web.Controllers
{
    public class ArtistController : BaseController
    {
        private readonly IArtistService artistService;
        public ArtistController(IArtistService artistService)
        {
            this.artistService = artistService;
        }
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.artistService.ArtistExistsByUserId(userId);
            if (isAgent)
            {
                return this.BadRequest();
            }
            return View();
        }
    }
}
