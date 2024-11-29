using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategory(int id);
    }
}
