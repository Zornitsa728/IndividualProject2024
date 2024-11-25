using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly RecipeDbContext dbContext;

        public CategoryService(RecipeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return dbContext.Categories.ToList();
        }
    }
}
