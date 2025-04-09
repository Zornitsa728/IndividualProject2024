using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CommentViewModels;
using RecipeApp.Web.ViewModels.RatingViewModels;
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
        private readonly IFavoriteService favoriteService;
        private readonly IRatingService ratingService;
        private readonly ICommentService commentService;
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
            ICategoryService categoryService,
            IFavoriteService favoriteService, IRatingService ratingService, ICommentService commentService)
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
            this.favoriteService = favoriteService;
            this.ratingService = ratingService;
            this.commentService = commentService;
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
                var cookbooks = await favoriteService.GetUserCookbooksAsync(userId);

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

        public async Task<Recipe> UpdateRecipeDetails(Recipe recipe, EditRecipeViewModel model)
        {
            // Update recipe details
            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Instructions = model.Instructions;
            recipe.ImageUrl = model.ImageUrl;
            recipe.CategoryId = model.CategoryId;

            return recipe;
        }

        public async Task<List<RecipeIngredient>> UpdateRecipeIngredients(Recipe recipe, EditRecipeViewModel model)
        {
            // Update ingredients if ingredients > 0
            var updatedIngredients = model.Ingredients.Select(i => new RecipeIngredient()
            {
                IngredientId = i.IngredientId,
                Quantity = i.Quantity,
                Unit = i.Unit,
                RecipeId = recipe.Id
            }).ToList();

            return updatedIngredients;
        }

        public async Task<EditRecipeViewModel> GetEditRecipeViewModel(Recipe recipe)
        {
            var model = new EditRecipeViewModel
            {
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                UserId = recipe.UserId,
                CategoryId = recipe.CategoryId,
                Categories = await categoryService.GetAllCategoriesAsync(),
                AvailableIngredients = await ingredientService.GetAllIngredientsAsync(),
                UnitsOfMeasurement = Enum.GetValues(typeof(UnitOfMeasurement))
                    .Cast<UnitOfMeasurement>()
                    .Select(u => new SelectListItem
                    {
                        Text = u.ToString(),
                        Value = ((int)u).ToString()
                    })
                    .ToList(),
                Ingredients = recipe.RecipeIngredients
                    .Select(ri => new IngredientViewModel()
                    {
                        IngredientId = ri.IngredientId,
                        Name = ri.Ingredient.Name,
                        Quantity = ri.Quantity,
                        Unit = ri.Unit
                    })
                    .ToList()
            };

            return model;
        }

        public async Task<DeleteRecipeViewModel> GetDeleteRecipeViewModel(Recipe recipe)
        {
            return new DeleteRecipeViewModel()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl
            };
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
        public async Task<(IEnumerable<RecipeCardViewModel>, int)> SearchRecipesAsync(string query, string userId, int pageNumber, int pageSize)
        {
            List<int> favoriteRecipeIds = new List<int>();

            if (userId != null)
            {
                var cookbooks = await favoriteService.GetUserCookbooksAsync(userId);

                //get all recipes from user cookbooks
                favoriteRecipeIds = await favoriteService.GetAllFavoriteRecipesIds(userId) ?? new List<int>();

            }

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

            var totalPages = (int)Math.Ceiling(matches.Count() / (double)pageSize);

            var currPageRecipes = matches
                .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only the records for the current page
                .ToList();

            return (matches, totalPages);
        }

        public async Task<RecipeDetailsViewModel> GetRecipeDetailsViewModel(string userId, Recipe recipe)
        {
            var recipeModel = new RecipeDetailsViewModel();

            if (userId != null)
            {
                List<int> favoriteRecipeIds = await favoriteService.GetAllFavoriteRecipesIds(userId!);

                var averageRating = await ratingService.GetAverageRatingAsync(recipe.Id);
                var comments = await commentService.GetCommentsAsync(recipe.Id);

                RecipeCommentsViewModel recipeCommentsViewModel = new RecipeCommentsViewModel()
                {
                    RecipeId = recipe.Id,
                    Comments = comments
                    .Select(c => new CommentViewModel()
                    {
                        CommentId = c.Id,
                        Content = c.Content,
                        UserId = c.UserId,
                        UserName = c.User.UserName,
                        RecipeId = c.RecipeId,
                        DatePosted = c.DatePosted,
                        UserCommented = (c.UserId == userId)
                    })
                    .ToList()
                };

                RatingViewModel ratingModel = new RatingViewModel()
                {
                    AverageRating = averageRating,
                    RecipeId = recipe.Id
                };

                recipeModel = new RecipeDetailsViewModel
                {
                    Recipe = recipe,
                    Comments = recipeCommentsViewModel,
                    Rating = ratingModel,
                    Liked = favoriteRecipeIds.Contains(recipe.Id)
                };
            }

            return recipeModel;
        }
    }
}