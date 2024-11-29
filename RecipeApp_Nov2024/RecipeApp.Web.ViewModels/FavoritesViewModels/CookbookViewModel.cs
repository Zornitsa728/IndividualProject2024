using RecipeApp.Web.ViewModels.CategoryViewModels;

namespace RecipeApp.Web.ViewModels.FavoritesViewModels
{
    public class CookbookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public string UserId { get; set; } = null!;

        public List<CategoryRecipeViewModel> Recipes { get; set; } = new();
    }

}
