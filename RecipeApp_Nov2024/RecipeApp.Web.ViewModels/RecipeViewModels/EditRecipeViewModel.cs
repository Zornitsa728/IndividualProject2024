using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Data.Models;

namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class EditRecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Instructions { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string UserId { get; set; } = null!;
        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } =
            new HashSet<Category>();
        public IEnumerable<Ingredient> AvailableIngredients { get; set; } =
             new List<Ingredient>();
        public IList<IngredientViewModel> Ingredients { get; set; } =
            new List<IngredientViewModel>();

        //public double Quantity { get; set; }
        public List<SelectListItem> UnitsOfMeasurement { get; set; } =
            new List<SelectListItem>();

        public List<int> DeletedIngredients { get; set; } = new List<int>();
    }

}
