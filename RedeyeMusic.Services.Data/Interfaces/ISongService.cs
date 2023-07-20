using RedeyeMusic.Web.ViewModels.Genre;
using RedeyeMusic.Web.ViewModels.Home;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<IndexViewModel>> GetAll();
        public Task<ICollection<GenreSelectViewModel>> SelectGenresAsync();
        public Task AddFirstSongAsync(AddFirstSongFormModel songModel, int artistId);
        public Task<AddSongFormModel> AddSongAsync(AddSongFormModel songModel, int artistId, string albumName);
        public Task AddMp3File(AddSongFormModel songModel);
    }
}
