using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
    }
}
