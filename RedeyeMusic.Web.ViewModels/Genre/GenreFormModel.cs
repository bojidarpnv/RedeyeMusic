using System.ComponentModel.DataAnnotations;
using static RedeyeMusic.Common.EntityValidationConstants.Genre;
namespace RedeyeMusic.Web.ViewModels.Genre
{
    public class GenreFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;
    }
}
