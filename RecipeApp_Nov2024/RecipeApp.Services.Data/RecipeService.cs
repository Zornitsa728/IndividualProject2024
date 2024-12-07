using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe, int> recipeRepository;
        private readonly IRepository<RecipeIngredient, object> recipeIngredientRepository;
        private readonly IRepository<Comment, int> commentRepository;
        private readonly IRepository<Rating, int> ratingRepository;
        private readonly IRepository<Cookbook, int> cookbookRepository;
        private readonly IRepository<Category, int> categoryRepository;

        public RecipeService(
            IRepository<Recipe, int> recipeRepository,
            IRepository<RecipeIngredient, object> recipeIngredientRepository,
            IRepository<Comment, int> commentRepository,
            IRepository<Rating, int> ratingRepository,
            IRepository<Cookbook, int> cookbookRepository,
            IRepository<Category, int> categoryRepository)
        {
            this.recipeRepository = recipeRepository;
            this.recipeIngredientRepository = recipeIngredientRepository;
            this.commentRepository = commentRepository;
            this.ratingRepository = ratingRepository;
            this.cookbookRepository = cookbookRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task AddRecipeAsync(Recipe recipe, List<RecipeIngredient> ingredients)
        {
            await recipeRepository.AddAsync(recipe);

            foreach (var ingredient in ingredients)
            {
                ingredient.RecipeId = recipe.Id;
                await recipeIngredientRepository.AddAsync(ingredient);
            }
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await recipeRepository.GetAllAttached()
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await recipeRepository.GetAllAttached()
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<IEnumerable<Cookbook>> GetUserCookbooksAsync(string userId)
        {
            return await cookbookRepository.GetAllAttached()
                .Where(cb => cb.UserId == userId)
                .Include(cb => cb.RecipeCookbooks)
                .ToListAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredient> updatedIngredients)
        {
            // Update recipe details
            await recipeRepository.UpdateAsync(recipe);

            // Replace ingredients
            var existingIngredients = recipeIngredientRepository.GetAllAttached()
                .Where(ri => ri.RecipeId == recipe.Id)
                .ToList();

            foreach (var ingredient in existingIngredients)
            {
                await recipeIngredientRepository.DeleteAsync(ingredient.IngredientId);
            }

            foreach (var ingredient in updatedIngredients)
            {
                ingredient.RecipeId = recipe.Id;
                await recipeIngredientRepository.AddAsync(ingredient);
            }
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await recipeRepository.GetByIdAsync(id);
            if (recipe != null)
            {
                recipe.IsDeleted = true;
                await recipeRepository.UpdateAsync(recipe);
            }
        }

        public async Task<double> GetAverageRatingAsync(int recipeId)
        {
            return await ratingRepository.GetAllAttached()
                .Where(r => r.RecipeId == recipeId)
                .AverageAsync(r => r.Score);
        }

        public async Task<List<Comment>> GetCommentsAsync(int recipeId)
        {
            return await commentRepository.GetAllAttached()
                .Where(c => c.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            return await recipeIngredientRepository.GetAllAttached()
                .Select(r => r.Ingredient)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await categoryRepository.GetAllAsync();
        }
    }
}