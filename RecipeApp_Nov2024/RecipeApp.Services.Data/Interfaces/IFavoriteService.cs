using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.FavoritesViewModels;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IFavoriteService
    {
        Task CreateCookbookAsync(CookbookCreateViewModel model, string userId);
        Task<List<Cookbook>> GetUserCookbooksAsync(string userId);
        Task AddRecipeToCookbookAsync(int cookbookId, int recipeId);
        Task RemoveRecipeFromCookbookAsync(int cookbookId, int recipeId);
        Task<Cookbook> GetCookbookWithRecipesAsync(int cookbookId);
        Task<bool> RemoveCookbookAsync(int cookbookId);
        Task<List<int>> GetAllFavoriteRecipesIds(string userId);
    }

}
