using RedeyeMusic.Web.ViewModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedeyeMusic.Services.Data.Interfaces
{
    public interface IGenreService
    {
        public Task<ICollection<GenreSelectViewModel>> SelectGenresAsync();
        public Task CreateAsync(string genreName);
    }
}
