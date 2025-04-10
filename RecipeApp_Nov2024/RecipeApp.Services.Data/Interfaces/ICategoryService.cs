using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.CategoryViewModels;
using RecipeApp.Web.ViewModels.RecipeViewModels;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<List<CategoryViewModel>> GetCategoriesViewModelAsync();
        Task<(IEnumerable<RecipeCardViewModel>, int)> GetCurrPageRecipesForCategoryAsync(IEnumerable<Recipe> recipes, int categoryId, string? userId, int pageNumber, int pageSize);
    }
}
