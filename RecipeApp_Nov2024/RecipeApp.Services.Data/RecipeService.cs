﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly IIngredientService ingredientService;
        private readonly ICategoryService categoryService;
        private IRepository<Recipe, int> object1;
        private IRepository<Ingredient, int> object2;
        private IRepository<Comment, int> object3;
        private IRepository<Rating, int> object4;
        private IRepository<Cookbook, int> object5;
        private IRepository<Category, int> object6;
        private IRepository<RecipeIngredient, object> object7;

        public RecipeService(
            IRepository<Recipe, int> recipeRepository,
            IRepository<Ingredient, int> ingredientRepository,
            IRepository<Comment, int> commentRepository,
            IRepository<Rating, int> ratingRepository,
            IRepository<Cookbook, int> cookbookRepository,
            IRepository<Category, int> categoryRepository,
            IRepository<RecipeIngredient, object> recipeIngredientRepository,
            IIngredientService ingredientService,
            ICategoryService categoryService)
        {
            this.recipeRepository = recipeRepository;
            this.ingredientRepository = ingredientRepository;
            this.commentRepository = commentRepository;
            this.ratingRepository = ratingRepository;
            this.cookbookRepository = cookbookRepository;
            this.categoryRepository = categoryRepository;
            this.recipeIngredientRepository = recipeIngredientRepository;
            this.ingredientService = ingredientService;
            this.categoryService = categoryService;
        }

        public RecipeService(IRepository<Recipe, int> object1, IRepository<Ingredient, int> object2, IRepository<Comment, int> object3, IRepository<Rating, int> object4, IRepository<Cookbook, int> object5, IRepository<Category, int> object6, IRepository<RecipeIngredient, object> object7)
        {
            this.object1 = object1;
            this.object2 = object2;
            this.object3 = object3;
            this.object4 = object4;
            this.object5 = object5;
            this.object6 = object6;
            this.object7 = object7;
        }

        public async Task<AddRecipeViewModel> GetAddRecipeViewModelAsync(string userId)
        {
            return new AddRecipeViewModel
            {
                Categories = await categoryService.GetAllCategoriesAsync(),
                AvailableIngredients = await ingredientService.GetAllIngredientsAsync(),
                UnitsOfMeasurement = GetUnitsOfMeasurementSelectList(),
                UserId = userId
            };
        }
        public async Task AddRecipeAsync(AddRecipeViewModel model)
        {
            var recipe = new Recipe
            {
                Title = model.Title,
                Description = model.Description,
                Instructions = model.Instructions,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                UserId = model.UserId
            };

            var ingredients = model.Ingredients
                .Select(i => new RecipeIngredient
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    RecipeId = recipe.Id
                }).ToList();

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

        public async Task<(IEnumerable<RecipeCardViewModel>, int)> GetCurrPageRecipes(IEnumerable<Recipe> recipes, string? userId, int pageNumber, int pageSize)
        {
            List<int> favoriteRecipeIds = new List<int>();

            if (!string.IsNullOrEmpty(userId))
            {
                var cookbooks = await GetUserCookbooksAsync(userId);

                // Get all recipe IDs from user cookbooks
                favoriteRecipeIds = cookbooks
                    .SelectMany(cb => cb.RecipeCookbooks)
                            .Select(rc => rc.RecipeId)
                            .ToList();
            }

            var model = recipes.Select(r => new RecipeCardViewModel()
            {
                Id = r.Id,
                Title = r.Title,
                ImageUrl = r.ImageUrl,
                Liked = favoriteRecipeIds.Contains(r.Id)
            }).ToList();

            var totalPages = (int)Math.Ceiling(model.Count() / (double)pageSize);

            var currPageRecipes = model
                .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only the records for the current page
                .ToList();

            return (currPageRecipes, totalPages);
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
        public List<SelectListItem> GetUnitsOfMeasurementSelectList()
        {
            return Enum.GetValues(typeof(UnitOfMeasurement))
                .Cast<UnitOfMeasurement>()
                .Select(u => new SelectListItem
                {
                    Text = u.ToString(),
                    Value = ((int)u).ToString()
                })
                .ToList();
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