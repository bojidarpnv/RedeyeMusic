using Microsoft.AspNetCore.Mvc.Rendering;
using RedeyeMusic.Services.Data.Models.Song;
using RedeyeMusic.Web.ViewModels.Genre;
using RedeyeMusic.Web.ViewModels.Home;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<IndexViewModel>> GetAll();
        Task<ICollection<GenreSelectViewModel>> SelectGenresAsync();
        Task<int> AddFirstSongAsync(AddFirstSongFormModel songModel, int artistId, string fullFilePath);
        Task<int> AddSongAsync(AddSongFormModel songModel, int artistId, string fullFilePath);
        Task AddMp3File(AddSongFormModel songModel);
        Task<AllSongsSearchedModel> SearchSongsAsync(AllSongsQueryModel queryModel);
        int GetSongDuration(string mp3FilePath);
        Task<IEnumerable<IndexViewModel>> AllByArtistIdAsync(int artistId);

        Task<SongDetailsViewModel> GetDetailsByIdAsync(int songId);
        Task<bool> ExistsById(int songId);
        Task<EditSongFormModel> GetSongForEditByIdAsync(int songId);
        Task EditSongByIdAndModel(int songId, EditSongFormModel model);
        Task DeleteSongByIdAsync(int songId);
        Task<List<SelectListItem>> GetSongsDropdownItemsAsync(int artistId);
        Task<ListenCountServiceModel> GetListenCountAsync(int songId);


    }
}
