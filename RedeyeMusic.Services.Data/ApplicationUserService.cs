using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;

namespace RedeyeMusic.Services.Data
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RedeyeMusicDbContext dbContext;
        public ApplicationUserService(UserManager<ApplicationUser> userManager, RedeyeMusicDbContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            var sanitizer = new HtmlSanitizer();
            ApplicationUser? user = await this.dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return string.Empty;
            }

            return sanitizer.Sanitize(user.FirstName + " " + user.LastName);

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
