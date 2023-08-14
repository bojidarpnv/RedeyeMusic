using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.User;

namespace RedeyeMusic.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IApplicationUserService userService;
        public UserController(IApplicationUserService userService)
        {
            this.userService = userService;
        }
        [Route("User/All")]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> viewModel = await this.userService.GetAllAsync();

            return View(viewModel);
        }
    }
}
