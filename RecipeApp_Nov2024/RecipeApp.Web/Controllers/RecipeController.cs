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
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;
        private readonly IIngredientService _ingredientService;
        private readonly IRatingService _ratingService;
        private readonly ICommentService _commentService;
        private readonly IFavoriteService _favoriteService;

        public RecipeController(
           IRecipeService recipeService,
           ICategoryService categoryService,
           IIngredientService ingredientService,
           IRatingService ratingService,
           ICommentService commentService,
           IFavoriteService favoriteService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
            _ingredientService = ingredientService;
            _ratingService = ratingService;
            _commentService = commentService;
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipes = _recipeService.GetAllRecipes();
            var cookbooks = await _favoriteService.GetUserCookbooksAsync(userId);

            // Map cookbooks to a strong-typed view model
            ViewBag.Cookbooks = cookbooks
                .Select(c => new CookbookDropdownViewModel
                {
                    Id = c.Id,
                    Title = c.Title
                })
                .ToList();

            return View(recipes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            AddRecipeViewModel model = new AddRecipeViewModel();
            model.Categories = await _categoryService.GetAllCategories();
            model.AvailableIngredients = _ingredientService.GetAllIngredients();

            model.UnitsOfMeasurement = Enum.GetValues(typeof(UnitOfMeasurement))
                .Cast<UnitOfMeasurement>()
                .Select(u => new SelectListItem
                {
                    Text = u.ToString(),
                    Value = ((int)u).ToString()
                })
                .ToList();

            //TODO: if user is not log in it should send him to login page and only loged in users can see add recipe
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddRecipeViewModel model)
        {
            _recipeService.DeleteAllTests();

            if (!ModelState.IsValid)
            {
                // Reload necessary data in case of validation errors
                model.Categories = await _categoryService.GetAllCategories();

                model.AvailableIngredients = _ingredientService.GetAllIngredients();

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

            // Map the AddRecipeViewModel to Recipe entity
            var recipe = new Recipe
            {
                Title = model.Title,
                Description = model.Description,
                Instructions = model.Instructions,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                UserId = model.UserId
            };

            _recipeService.AddRecipeAsync(recipe);

            recipe.RecipeIngredients = model.Ingredients
                .Select(i => new RecipeIngredient
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    RecipeId = recipe.Id
                }).ToList();

            _recipeService.UpdateRecipe(recipe);

            return RedirectToAction(nameof(MyRecipes));
        }

        [HttpGet]
        public IActionResult MyRecipes()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Index", "Home");
            }

            var myRecipes = _recipeService.GetAllRecipes()
                .Where(r => r.UserId == currentUserId);

            return View(myRecipes);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            var averageRating = await _ratingService.GetAverageRatingAsync(id);
            var comments = await _commentService.GetCommentsAsync(id);

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

            //todo: check for null recipe

            var recipeModel = new RecipeDetailsViewModel
            {
                Recipe = recipe,
                Comments = recipeCommentsViewModel,
                Rating = ratingModel
            };

            return View(recipeModel);
        }
    }
}
