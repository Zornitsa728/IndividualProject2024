using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext dbContext;

        public RecipeService(RecipeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            await dbContext.Recipes.AddAsync(recipe);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await dbContext.Recipes
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            var recipe = await dbContext.Recipes
               .Include(r => r.RecipeIngredients)
               .ThenInclude(ri => ri.Ingredient)
               .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            return (recipe);

        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            dbContext.Recipes.Update(recipe);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);

            if (recipe != null)
            {
                recipe.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateRecipeIngredientsAsync(int recipeId, List<RecipeIngredient> updatedIngredients)
        {
            var existingIngredients = dbContext.RecipesIngredients.Where(ri => ri.RecipeId == recipeId);

            // Remove existing ingredients
            dbContext.RecipesIngredients.RemoveRange(existingIngredients);

            // Add new ingredients
            await dbContext.RecipesIngredients.AddRangeAsync(updatedIngredients);

            await dbContext.SaveChangesAsync();
        }

    }
}
