using Microsoft.AspNetCore.Identity;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;

namespace RedeyeMusic.Services.Data
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<bool> ValidatePasswordAsync(string userId, string password)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            return await userManager.CheckPasswordAsync(user, password);
        }
    }
}
