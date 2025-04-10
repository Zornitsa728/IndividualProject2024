using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.CategoryViewModels;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
        Task<List<CategoryViewModel>> GetCategoriesViewModelAsync();

    }
}
