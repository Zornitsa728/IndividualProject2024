using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IRecipeService
    {
        Task AddRecipeAsync(Recipe recipe, List<RecipeIngredient> ingredients);
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredient> updatedIngredients);
        Task DeleteRecipeAsync(int id);
        Task<IEnumerable<Cookbook>> GetUserCookbooksAsync(string userId);
        Task<double> GetAverageRatingAsync(int recipeId);
        Task<List<Comment>> GetCommentsAsync(int recipeId);
        Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
