using RedeyeMusic.Web.ViewModels.User;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IApplicationUserService
    {
        Task<bool> ValidatePasswordAsync(string userId, string password);
        Task<string> GetFullNameByEmailAsync(string email);
        Task<string> GetFullNameByIdAsync(string userId);
        Task<IEnumerable<UserViewModel>> GetAllAsync();
    }
}
