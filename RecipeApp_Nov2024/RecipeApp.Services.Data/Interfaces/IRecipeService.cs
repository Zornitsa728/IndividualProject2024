using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.RecipeViewModels;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IRecipeService
    {
        Task<AddRecipeViewModel> GetAddRecipeViewModelAsync(string userId);
        Task AddRecipeAsync(AddRecipeViewModel model);
        Task<IEnumerable<Recipe>> GetRecipesAsync();
        Task<(IEnumerable<RecipeCardViewModel>, int)> GetCurrPageRecipes(IEnumerable<Recipe> recipes, string? userId, int pageNumber, int pageSize);
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredient> updatedIngredients);
        List<SelectListItem> GetUnitsOfMeasurementSelectList();
        Task<IEnumerable<RecipeCardViewModel>> SearchRecipesAsync(string query, List<int> favoriteRecipeIds);
        Task<EditRecipeViewModel> GetEditRecipeviewModel(Recipe recipe);
        Task<bool> DeleteRecipeAsync(int id);
        Task<RecipeDetailsViewModel> GetRecipeDetailsViewModel(string userId, Recipe recipe);
    }
}
