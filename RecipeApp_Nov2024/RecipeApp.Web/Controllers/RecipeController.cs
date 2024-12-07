using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CommentViewModels;
using RecipeApp.Web.ViewModels.FavoritesViewModels;
using RecipeApp.Web.ViewModels.RatingViewModels;
using RecipeApp.Web.ViewModels.RecipeViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipes = await recipeService.GetAllRecipesAsync();

            List<RecipeCardViewModel> model = new List<RecipeCardViewModel>();

            if (userId != null)
            {
                var cookbooks = await recipeService.GetUserCookbooksAsync(userId);

                //get all recipes from user cookbooks
                List<int> favoriteRecipeIds = cookbooks
                   .SelectMany(cb => cb.RecipeCookbooks)
                   .Select(rc => rc.RecipeId)
                   .ToList();

                model = recipes.Select(r => new RecipeCardViewModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl,
                    Liked = favoriteRecipeIds.Contains(r.Id) //tag from all the liked ones (Liked - bool)
                }).ToList();

                // Map cookbooks to a strong-typed view model
                ViewBag.Cookbooks = cookbooks
                    .Select(c => new CookbookDropdownViewModel
                    {
                        Id = c.Id,
                        Title = c.Title
                    })
                    .ToList();
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            AddRecipeViewModel model = new AddRecipeViewModel();
            model.Categories = await recipeService.GetAllCategoriesAsync();
            model.AvailableIngredients = await recipeService.GetAllIngredientsAsync();

            model.UnitsOfMeasurement = Enum.GetValues(typeof(UnitOfMeasurement))
                .Cast<UnitOfMeasurement>()
                .Select(u => new SelectListItem
                {
                    Text = u.ToString(),
                    Value = ((int)u).ToString()
                })
                .ToList();

            model.UserId = userId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await recipeService.GetAllCategoriesAsync();
                model.AvailableIngredients = await recipeService.GetAllIngredientsAsync();

                model.UnitsOfMeasurement = Enum.GetValues(typeof(UnitOfMeasurement))
                    .Cast<UnitOfMeasurement>()
                    .Select(u => new SelectListItem
                    {
                        Text = u.ToString(),
                        Value = ((int)u).ToString()
                    })
                    .ToList();

                return View(model);
            }

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

            await recipeService.AddRecipeAsync(recipe, ingredients);

            return RedirectToAction(nameof(MyRecipes));
        }

        [HttpGet]
        public async Task<IActionResult> MyRecipes()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Index", "Home");
            }

            var allRecipes = await recipeService.GetAllRecipesAsync();

            var myRecipes = allRecipes.Where(r => r.UserId == currentUserId);

            return View(myRecipes);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var averageRating = await recipeService.GetAverageRatingAsync(id);
            var comments = await recipeService.GetCommentsAsync(id);

            RecipeCommentsViewModel recipeCommentsViewModel = new RecipeCommentsViewModel()
            {
                RecipeId = id,
                Comments = comments
                .Select(c => new CommentViewModel()
                {
                    CommentId = c.Id,
                    Content = c.Content,
                    UserId = c.UserId,
                    UserName = c.User.UserName,
                    RecipeId = c.RecipeId,
                    DatePosted = c.DatePosted
                })
                .ToList()
            };

            RatingViewModel ratingModel = new RatingViewModel()
            {
                AverageRating = averageRating,
                RecipeId = id
            };

            var recipeModel = new RecipeDetailsViewModel
            {
                Recipe = recipe,
                Comments = recipeCommentsViewModel,
                Rating = ratingModel
            };

            return View(recipeModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var recipe = await recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (userId != recipe.UserId)
            {
                return NotFound();
            }

            var model = new EditRecipeViewModel
            {
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                UserId = recipe.UserId,
                CategoryId = recipe.CategoryId,
                Categories = await recipeService.GetAllCategoriesAsync(),
                AvailableIngredients = await recipeService.GetAllIngredientsAsync(),
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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await recipeService.GetAllCategoriesAsync();
                model.AvailableIngredients = await recipeService.GetAllIngredientsAsync();
                model.UnitsOfMeasurement = Enum.GetValues(typeof(UnitOfMeasurement))
                    .Cast<UnitOfMeasurement>()
                    .Select(u => new SelectListItem
                    {
                        Text = u.ToString(),
                        Value = ((int)u).ToString()
                    })
                    .ToList();
                return View(model);
            }

            var recipe = await recipeService.GetRecipeByIdAsync(model.Id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Update recipe details
            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Instructions = model.Instructions;
            recipe.ImageUrl = model.ImageUrl;
            recipe.CategoryId = model.CategoryId;

            // Update ingredients if ingredients > 0
            var updatedIngredients = model.Ingredients.Select(i => new RecipeIngredient()
            {
                IngredientId = i.IngredientId,
                Quantity = i.Quantity,
                Unit = i.Unit,
                RecipeId = recipe.Id
            }).ToList();

            // Remove existing ingredients (if any) and add updated ones
            await recipeService.UpdateRecipeAsync(recipe, updatedIngredients);

            return RedirectToAction(nameof(MyRecipes));
        }
    }
}
