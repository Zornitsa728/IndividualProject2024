using System.ComponentModel.DataAnnotations;
using static RecipeApp.Common.EntityValidationConstants.Cookbook;

namespace RecipeApp.Web.ViewModels.FavoritesViewModels
{
    public class CookbookCreateViewModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }
    }
}
