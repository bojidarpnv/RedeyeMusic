using Microsoft.AspNetCore.Mvc;

namespace RedeyeMusic.Web.Controllers
{
    public class ArtistController : Controller
    {
        public async Task<IActionResult> BecomeArtist()
        {
            return View();
        }
    }
}
