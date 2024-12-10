using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category, int> categoryRepository;

        public CategoryService(IRepository<Category, int> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await categoryRepository.GetByIdAsync(id);
        }
    }
}

