using RedeyeMusic.Web.ViewModels.Artist;
using RedeyeMusic.Web.ViewModels.Genre;
using RedeyeMusic.Web.ViewModels.Home;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<IndexViewModel>> GetAll();
        public Task<ICollection<GenreSelectViewModel>> SelectGenresAsync();
        public Task<AddSongFormModel> AddSongAsync(AddSongFormModel songModel);
        public Task AddMp3File(AddSongFormModel songModel);
    }
}
