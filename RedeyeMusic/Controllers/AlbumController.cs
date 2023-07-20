//using Microsoft.AspNetCore.Mvc;
//using RedeyeMusic.Services.Data.Interfaces;
//using RedeyeMusic.Web.ViewModels.Album;

//namespace RedeyeMusic.Web.Controllers
//{
//    public class AlbumController : Controller
//    {
//        private readonly IAlbumService albumService;
//        public AlbumController(IAlbumService albumService)
//        {
//            this.albumService = albumService;
//        }
//        [HttpGet]
//        public IActionResult Create()
//        {
//            AlbumSelectViewModel viewModel = new AlbumSelectViewModel();
//            return View(viewModel);
//        }
//        [HttpPost]
//        public async Task<string> Create(AlbumSelectViewModel viewModel)
//        {
//            await this.albumService.AddAlbum(viewModel);
//            return viewModel.Name;
//        }
//    }
//}
