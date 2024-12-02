using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IRecipeService
    {
        Task AddRecipeAsync(Recipe recipe);
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int id);
        Task UpdateRecipeIngredientsAsync(int recipeId, List<RecipeIngredient> updatedIngredients);
    }
}
