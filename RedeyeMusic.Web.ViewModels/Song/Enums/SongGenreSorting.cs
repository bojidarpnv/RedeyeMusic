using System.ComponentModel.DataAnnotations;

namespace RedeyeMusic.Web.ViewModels.Song.Enums
{
    public enum SongGenreSorting
    {
        Pop = 1,
        Rock = 2,
        HipHop = 3,
        [Display(Name = "R&B")]
        RandB = 4,
        Classical = 5,
        Rap = 6,
        Country = 7,
        Alternative = 8,
    }
}
