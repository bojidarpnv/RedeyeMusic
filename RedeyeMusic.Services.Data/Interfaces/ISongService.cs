using RedeyeMusic.Services.Data.Models.Song;
using RedeyeMusic.Web.ViewModels.Genre;
using RedeyeMusic.Web.ViewModels.Home;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<IndexViewModel>> GetAll();
        public Task<ICollection<GenreSelectViewModel>> SelectGenresAsync();
        public Task AddFirstSongAsync(AddFirstSongFormModel songModel, int artistId, string fullFilePath);
        public Task AddSongAsync(AddSongFormModel songModel, int artistId, string fullFilePath);
        public Task AddMp3File(AddSongFormModel songModel);
        public Task<AllSongsSearchedModel> SearchSongsAsync(AllSongsQueryModel queryModel);
        public int GetSongDuration(string mp3FilePath);
    }
}
