﻿using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.RecipeViewModels;

namespace RecipeApp.Services.Data
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe, int> recipeRepository;
        private readonly IRepository<Ingredient, int> ingredientRepository;
        private readonly IRepository<Comment, int> commentRepository;
        private readonly IRepository<Rating, int> ratingRepository;
        private readonly IRepository<Cookbook, int> cookbookRepository;
        private readonly IRepository<Category, int> categoryRepository;
        private readonly IRepository<RecipeIngredient, object> recipeIngredientRepository;

        public RecipeService(
            IRepository<Recipe, int> recipeRepository,
            IRepository<Ingredient, int> ingredientRepository,
            IRepository<Comment, int> commentRepository,
            IRepository<Rating, int> ratingRepository,
            IRepository<Cookbook, int> cookbookRepository,
            IRepository<Category, int> categoryRepository,
            IRepository<RecipeIngredient, object> recipeIngredientRepository)
        {
            this.recipeRepository = recipeRepository;
            this.ingredientRepository = ingredientRepository;
            this.commentRepository = commentRepository;
            this.ratingRepository = ratingRepository;
            this.cookbookRepository = cookbookRepository;
            this.categoryRepository = categoryRepository;
            this.recipeIngredientRepository = recipeIngredientRepository;
        }

        public async Task AddRecipeAsync(Recipe recipe, List<RecipeIngredient> ingredients)
        {
            await recipeRepository.AddAsync(recipe);

            //avoid duplication
            var uniqueIngredients = ingredients.GroupBy(i => i.IngredientId).Select(i => i.Last());

            //adding only the last unique ingredient
            foreach (var ingredient in uniqueIngredients)
            {
                ingredient.RecipeId = recipe.Id;
                await recipeIngredientRepository.AddAsync(ingredient);
            }
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            var recipes = await recipeRepository.GetAllAttached()
                .Include(r => r.Category)
                .Where(r => !r.IsDeleted)
                .OrderBy(r => r.Title)
                .ToListAsync();

            return recipes;
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            var recipes = await recipeRepository.GetAllAttached()
                .Include(r => r.User)
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            return recipes;
        }

        public async Task<IEnumerable<Cookbook>> GetUserCookbooksAsync(string userId)
        {
            List<Cookbook>? userCookbooks = await cookbookRepository.GetAllAttached()
                .Where(cb => cb.UserId == userId)
                .Include(cb => cb.RecipeCookbooks)
                .ToListAsync();

            return userCookbooks;
        }

        public async Task UpdateRecipeAsync(Recipe recipe, List<RecipeIngredient> updatedIngredients)
        {
            // Update recipe details
            await recipeRepository.UpdateAsync(recipe);

            // Old ingredients
            var existingIngredients = recipeIngredientRepository.GetAllAttached()
                .Where(ri => ri.RecipeId == recipe.Id)
                .ToList();

            //removing the old ingredients
            foreach (var ingredient in existingIngredients)
            {
                await recipeIngredientRepository.DeleteAsync(new object[] { ingredient.RecipeId, ingredient.IngredientId });
            }

            //prevent ingredient duplication
            var uniqueIngredients = updatedIngredients
                .GroupBy(i => i.IngredientId)
                .Select(i => i.Last());

            //adding new ingredients
            foreach (var ingredient in uniqueIngredients)
            {
                ingredient.RecipeId = recipe.Id;
                await recipeIngredientRepository.AddAsync(ingredient);
            }
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipe = await recipeRepository.GetByIdAsync(id);

            if (recipe != null)
            {
                recipe.IsDeleted = true;
                await recipeRepository.UpdateAsync(recipe);
                return true;
            }

            return false;
        }

        public async Task<double> GetAverageRatingAsync(int recipeId)
        {
            var avrgRatingForCurrRecipe = await ratingRepository
                .GetAllAttached()
                .Where(r => r.RecipeId == recipeId)
                .ToListAsync();

            if (avrgRatingForCurrRecipe.Count != 0)
            {
                return avrgRatingForCurrRecipe.Average(r => r.Score);
            }

            return 0;
        }

        public async Task<List<Comment>> GetCommentsAsync(int recipeId)
        {
            var comments = await commentRepository.GetAllAttached()
                .Include(c => c.User)
                .Where(c => c.RecipeId == recipeId && c.IsDeleted == false)
                .ToListAsync();

            return comments;
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            var ingredients = await ingredientRepository
                .GetAllAsync();

            return ingredients;

        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<RecipeCardViewModel>> SearchRecipesAsync(string query, List<int> favoriteRecipeIds)
        {
            var searchQuery = query.ToLower().Trim();

            IEnumerable<RecipeCardViewModel> matches = await recipeRepository.GetAllAttached()
                .Where(r => r.IsDeleted == false)
                .Where(r => r.Title.ToLower().Contains(searchQuery) || r.Description.ToLower().Contains(searchQuery))
                .Select(r => new RecipeCardViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl,
                    Liked = favoriteRecipeIds.Contains(r.Id)
                })
                .ToListAsync();

            return matches;
        }
    }
}