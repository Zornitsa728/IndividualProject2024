using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CategoryViewModels;

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

        public async Task<List<CategoryViewModel>> GetCategoriesViewModelAsync()
        {
            var allCategories = await GetAllCategoriesAsync();

            List<CategoryViewModel> model = allCategories
            .Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl
            })
            .ToList();

            return model;
        }
    }
}

