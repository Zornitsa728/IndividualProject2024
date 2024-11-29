using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            IEnumerable<Category> categories = await dbContext.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategory(int id)
        {
            Category? category = dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            return category;
        }
    }
}
