using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CategoryViewModels;
using RecipeApp.Web.ViewModels.RecipeViewModels;

namespace RecipeApp.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category, int> categoryRepository;
        private readonly IFavoriteService favoriteService;

        public CategoryService(IRepository<Category, int> categoryRepository, IFavoriteService favoriteService)
        {
            this.categoryRepository = categoryRepository;
            this.favoriteService = favoriteService;
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

        public async Task<(IEnumerable<RecipeCardViewModel>, int)> GetCurrPageRecipesForCategoryAsync(IEnumerable<Recipe> recipes, int categoryId, string? userId, int pageNumber, int pageSize)
        {
            List<int> favoriteRecipeIds = new List<int>();

            if (!string.IsNullOrEmpty(userId))
            {
                var cookbooks = await favoriteService.GetUserCookbooksAsync(userId);

                // Get all recipe IDs from user cookbooks
                favoriteRecipeIds = cookbooks
                    .SelectMany(cb => cb.RecipeCookbooks)
                            .Select(rc => rc.RecipeId)
                            .ToList();
            }

            IEnumerable<RecipeCardViewModel> recipesModel = recipes
           .Where(r => r.CategoryId == categoryId & r.IsDeleted == false)
           .Select(rc => new RecipeCardViewModel()
           {
               Id = rc.Id,
               Title = rc.Title,
               ImageUrl = rc.ImageUrl,
               Liked = favoriteRecipeIds.Contains(rc.Id)
           })
           .ToList();


            var totalPages = (int)Math.Ceiling(recipesModel.Count() / (double)pageSize);

            var currPageRecipes = recipesModel
                .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only the records for the current page
                .ToList();

            return (currPageRecipes, totalPages);
        }
    }
}

