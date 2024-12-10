using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.RecipeViewModels;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IRecipeService
    {
        Task AddRecipeAsync(Recipe recipe, List<RecipeIngredient> ingredients);
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredient> updatedIngredients);
        Task<IEnumerable<Cookbook>> GetUserCookbooksAsync(string userId);
        Task<double> GetAverageRatingAsync(int recipeId);
        Task<List<Comment>> GetCommentsAsync(int recipeId);
        Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<RecipeCardViewModel>> SearchRecipesAsync(string query, List<int> favoriteRecipeIds);
        Task<bool> DeleteRecipeAsync(int id);
    }
}
