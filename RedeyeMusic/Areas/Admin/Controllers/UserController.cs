using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.User;
using static RedeyeMusic.Common.GeneralApplicationConstants;
namespace RedeyeMusic.Web.Areas.Admin.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IApplicationUserService userService;
        private readonly IMemoryCache memoryCache;
        public UserController(IApplicationUserService userService, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
        }
        [Route("User/All")]
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> users = this.memoryCache.Get<IEnumerable<UserViewModel>>(UsersCacheKey);
            if (users == null)
            {
                users = await this.userService.GetAllAsync();

                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(UsersCacheDurationMinutes));

                this.memoryCache.Set(UsersCacheKey, users, cacheEntryOptions);
            }

            return View(users);
        }
    }
}
