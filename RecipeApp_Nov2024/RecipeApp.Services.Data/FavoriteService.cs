using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.FavoritesViewModels;

namespace RecipeApp.Services.Data
{
    public class FavoriteService : IFavoriteService
    {
        private readonly RecipeDbContext dbContext;

        public FavoriteService(RecipeDbContext context)
        {
            dbContext = context;
        }

        public async Task CreateCookbookAsync(CookbookCreateViewModel model, string userId)
        {
            var cookbook = new Cookbook
            {
                Title = model.Title,
                Description = model.Description,
                UserId = userId
            };

            await dbContext.Cookbooks.AddAsync(cookbook);

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Cookbook>> GetUserCookbooksAsync(string userId)
        {
            return await dbContext.Cookbooks
                .Where(c => c.UserId == userId)
                .Include(c => c.RecipeCookbooks)
                .ThenInclude(rc => rc.Recipe)
                .ToListAsync();
        }
        public async Task AddRecipeToCookbookAsync(int cookbookId, int recipeId)
        {
            var cookbook = await dbContext.Cookbooks
                .Include(c => c.RecipeCookbooks)
                .FirstOrDefaultAsync(c => c.Id == cookbookId);

            if (cookbook != null && !cookbook.RecipeCookbooks.Any(rc => rc.RecipeId == recipeId))
            {
                if (!cookbook.RecipeCookbooks.Any(r => r.RecipeId == recipeId))
                {

                    cookbook.RecipeCookbooks.Add(new RecipeCookbook
                    {
                        CookbookId = cookbookId,
                        RecipeId = recipeId
                    });

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveRecipeFromCookbookAsync(int cookbookId, int recipeId)
        {
            var recipeCookbook = await dbContext.RecipesCookbooks
                .FirstOrDefaultAsync(rc => rc.CookbookId == cookbookId && rc.RecipeId == recipeId);

            if (recipeCookbook != null)
            {
                dbContext.RecipesCookbooks.Remove(recipeCookbook);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Cookbook> GetCookbookWithRecipesAsync(int cookbookId)
        {
            Cookbook? cookbook = await dbContext.Cookbooks
               .Include(c => c.RecipeCookbooks)
               .ThenInclude(rc => rc.Recipe)
               .FirstOrDefaultAsync(c => c.Id == cookbookId);

            return cookbook;
        }

        public async Task<bool> RemoveCookbookAsync(int cookbookId)
        {
            var cookbook = await dbContext.Cookbooks
                .Include(r => r.RecipeCookbooks)
               .FirstOrDefaultAsync(rc => rc.Id == cookbookId);

            if (cookbook != null)
            {
                dbContext.Cookbooks.Remove(cookbook);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}