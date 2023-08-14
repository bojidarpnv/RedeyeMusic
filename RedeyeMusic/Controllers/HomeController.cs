using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Home;
using static RedeyeMusic.Common.GeneralApplicationConstants;
namespace RedeyeMusic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISongService songService;
        public HomeController(ISongService songService)
        {
            this.songService = songService;
        }


        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return this.RedirectToAction("Index", "Home", new { Area = AdminAreaName });
            }
            IEnumerable<IndexViewModel> viewModel = await this.songService.GetAll();
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404 || statusCode == 400)
            {
                return this.View("Error404");
            }
            if (statusCode == 401)
            {
                return this.View("Error401");
            }
            return View();
        }
    }
}