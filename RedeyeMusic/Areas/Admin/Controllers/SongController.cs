using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.Infrastrucutre.Extensions;
using RedeyeMusic.Web.ViewModels.Home;
namespace RedeyeMusic.Web.Areas.Admin.Controllers
{
    public class SongController : BaseAdminController
    {
        private readonly IArtistService artistService;
        private readonly ISongService songService;
        public SongController(IArtistService artistService, ISongService songService)
        {
            this.artistService = artistService;
            this.songService = songService;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<IndexViewModel> viewModel = await this.songService.GetAll();
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<IndexViewModel> mySongs =
                new List<IndexViewModel>();
            string userId = this.User.GetId();
            bool isUserArtist = await this.artistService
                .ArtistExistsByUserIdAsync(userId);
            try
            {
                if (isUserArtist)
                {
                    int artistId = await this.artistService.GetArtistIdByUserIdAsync(userId);

                    mySongs.AddRange(await this.songService.AllByArtistIdAsync(artistId));
                }

                return this.View(mySongs);
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }


        }
    }
}
