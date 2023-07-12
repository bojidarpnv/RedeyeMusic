using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedeyeMusic.Web.Controllers
{
    public class SongController : BaseController
    {

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }
    }
}
