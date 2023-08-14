using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.User;

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

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            List<UserViewModel> allUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FullName = u.FirstName + " " + u.LastName,
                })
                .ToListAsync();

            foreach (UserViewModel user in allUsers)
            {
                Artist? artist = await this.dbContext
                    .Artists
                    .FirstOrDefaultAsync(a => a.ApplicationUserId.ToString() == user.Id);
                if (artist != null)
                {
                    user.ArtistName = artist.Name;
                }
                else
                {
                    user.ArtistName = string.Empty;
                }
            }
            return allUsers;
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

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            var sanitizer = new HtmlSanitizer();
            ApplicationUser? user = await this.dbContext
            .Users
            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
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
