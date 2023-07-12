using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Home;
using System.Diagnostics;

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
            IEnumerable<IndexViewModel> viewModel = await this.songService.GetAll();
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}