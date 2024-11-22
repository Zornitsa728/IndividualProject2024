using RecipeApp.Data.Models;
using System.ComponentModel.DataAnnotations;
using static RecipeApp.Common.EntityValidationConstants.Recipe;
using static RecipeApp.Common.EntityValidationMessages.Recipe;

namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class AddRecipeViewModel
    {
        public AddRecipeViewModel()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = CreateDateMessage)]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MinLength(InstructionsMinLength)]
        [MaxLength(InstructionsMaxLength)]
        public string Instructions { get; set; } = null!;

        [Url(ErrorMessage = ImageUrlMessage)]
        public string? ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } =
            new HashSet<Category>();

        //[Required(ErrorMessage = IngredientMessage)]
        //public IList<IngredientInputModel> Ingredients =
        //    new List<IngredientInputModel>();

        //public IList<SelectListItem> AvailableIngredients { get; set; } =
        //    new List<SelectListItem>();

    }
}
