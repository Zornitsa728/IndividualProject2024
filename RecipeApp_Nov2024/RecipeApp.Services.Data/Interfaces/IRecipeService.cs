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
        Task<Recipe> UpdateRecipeDetails(Recipe recipe, EditRecipeViewModel model);
        Task<List<RecipeIngredient>> UpdateRecipeIngredients(Recipe recipe, EditRecipeViewModel model);
        Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredient> updatedIngredients);
        List<SelectListItem> GetUnitsOfMeasurementSelectList();
        Task<(IEnumerable<RecipeCardViewModel>, int)> SearchRecipesAsync(string query, string userId, int pageNumber, int pageSize);
        Task<EditRecipeViewModel> GetEditRecipeViewModel(Recipe recipe);
        Task<DeleteRecipeViewModel> GetDeleteRecipeViewModel(Recipe recipe);
        Task<bool> DeleteRecipeAsync(int id);
        Task<RecipeDetailsViewModel> GetRecipeDetailsViewModel(string userId, Recipe recipe);
    }
}
