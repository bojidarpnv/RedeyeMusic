namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IApplicationUserService
    {
        public Task<bool> ValidatePasswordAsync(string userId, string password);
    }
}
