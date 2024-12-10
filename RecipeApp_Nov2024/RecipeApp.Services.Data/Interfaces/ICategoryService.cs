using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
    }
}
