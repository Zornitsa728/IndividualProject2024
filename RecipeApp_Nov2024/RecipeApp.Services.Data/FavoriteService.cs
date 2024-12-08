using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.FavoritesViewModels;

namespace RecipeApp.Services.Data
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IRepository<Cookbook, int> cookbookRepository;
        private readonly IRepository<RecipeCookbook, object> recipeCookbookRepository;

        public FavoriteService(IRepository<Cookbook, int> cookbookRepository, IRepository<RecipeCookbook, object> recipeCookbookRepository)
        {
            this.cookbookRepository = cookbookRepository;
            this.recipeCookbookRepository = recipeCookbookRepository;
        }

        public async Task CreateCookbookAsync(CookbookCreateViewModel model, string userId)
        {
            var cookbook = new Cookbook
            {
                Title = model.Title,
                Description = model.Description,
                UserId = userId
            };

            await cookbookRepository.AddAsync(cookbook);
        }

        public async Task<List<Cookbook>> GetUserCookbooksAsync(string userId)
        {
            var cookbooks = cookbookRepository.GetAllAttached();

            return await cookbooks.Where(c => c.UserId == userId)
                            .Include(c => c.RecipeCookbooks)
                            .ThenInclude(rc => rc.Recipe)
                            .ToListAsync();
        }
        public async Task AddRecipeToCookbookAsync(int cookbookId, int recipeId)
        {
            var cookbook = await cookbookRepository.GetByIdAsync(cookbookId);

            if (cookbook != null && !cookbook.RecipeCookbooks.Any(rc => rc.RecipeId == recipeId))
            {
                var newRecipeCookbook = new RecipeCookbook
                {
                    CookbookId = cookbookId,
                    RecipeId = recipeId
                };

                await recipeCookbookRepository.AddAsync(newRecipeCookbook);
            }
        }

        public async Task RemoveRecipeFromCookbookAsync(int cookbookId, int recipeId)
        {
            var recipeCookbook = (await recipeCookbookRepository
                .GetByIdAsync(new object[] { recipeId, cookbookId }));

            if (recipeCookbook != null)
            {
                await recipeCookbookRepository.DeleteAsync(new object[] { recipeId, cookbookId });
            }
        }

        public async Task<Cookbook> GetCookbookWithRecipesAsync(int cookbookId)
        {
            Cookbook? cookbook = await cookbookRepository.GetAllAttached()
               .Include(c => c.RecipeCookbooks)
               .ThenInclude(rc => rc.Recipe)
               .FirstOrDefaultAsync(c => c.Id == cookbookId);

            return cookbook;
        }

        public async Task<bool> RemoveCookbookAsync(int cookbookId)
        {
            var cookbook = await cookbookRepository
                .GetAllAttached().Include(r => r.RecipeCookbooks)
               .FirstOrDefaultAsync(rc => rc.Id == cookbookId);

            if (cookbook != null)
            {
                //if any recipes are in the book
                if (cookbook.RecipeCookbooks.Any())
                {
                    foreach (var recipe in cookbook.RecipeCookbooks)
                    {
                        await recipeCookbookRepository.DeleteAsync(new object[] { recipe.RecipeId, recipe.CookbookId });
                    }
                }

                // the empty book
                await cookbookRepository.DeleteAsync(cookbookId);
                return true;
            }

            return false;
        }
    }
}