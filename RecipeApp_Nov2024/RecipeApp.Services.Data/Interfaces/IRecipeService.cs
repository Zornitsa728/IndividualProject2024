using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IRecipeService
    {
        void AddRecipeAsync(Recipe recipe);
        IEnumerable<Recipe> GetAllRecipes();
        Recipe? GetRecipeById(int id);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(int id);
        void DeleteAllTests();
    }
}
