using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Data.Models;
using System.ComponentModel.DataAnnotations;
using static RecipeApp.Common.EntityValidationConstants.Recipe;
using static RecipeApp.Common.EntityValidationMessages.Recipe;


namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class EditRecipeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

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
        public IEnumerable<Ingredient> AvailableIngredients { get; set; } =
             new List<Ingredient>();

        [Required(ErrorMessage = IngredientMessage)]
        public IList<IngredientViewModel> Ingredients { get; set; } =
            new List<IngredientViewModel>();

        //public double Quantity { get; set; }
        public List<SelectListItem> UnitsOfMeasurement { get; set; } =
            new List<SelectListItem>();
    }

}
