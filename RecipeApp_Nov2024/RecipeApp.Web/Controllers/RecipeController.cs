using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.FavoritesViewModels;
using RecipeApp.Web.ViewModels.RecipeViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IFavoriteService favoriteService;
        private readonly ICategoryService categoryService;
        private readonly IIngredientService ingredientService;
        private readonly ICommentService commentService;
        private readonly IRatingService ratingService;

        public RecipeController(IRecipeService recipeService, IFavoriteService favoriteService, ICategoryService categoryService, IIngredientService ingredientService, ICommentService commentService, IRatingService ratingService)
        {
            this.recipeService = recipeService;
            this.favoriteService = favoriteService;
            this.categoryService = categoryService;
            this.ingredientService = ingredientService;
            this.commentService = commentService;
            this.ratingService = ratingService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 9)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipes = await recipeService.GetRecipesAsync();

            var (modelCardView, totalPages) = await recipeService.GetCurrPageRecipes(recipes, userId, pageNumber, pageSize);

            //Map cookbooks to a strong - typed view model
            await SetCookbooksInViewBag(userId!);

            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;

            return View(modelCardView);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            AddRecipeViewModel model = (await recipeService.GetAddRecipeViewModelAsync(userId));

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                model.AvailableIngredients = await ingredientService.GetAllIngredientsAsync();
                model.UnitsOfMeasurement = recipeService.GetUnitsOfMeasurementSelectList();

                return View(model);
            }

            await recipeService.AddRecipeAsync(model);

            return RedirectToAction(nameof(MyRecipes));
        }

        [HttpGet]
        public async Task<IActionResult> MyRecipes(int pageNumber = 1, int pageSize = 9)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Index", "Home");
            }

            var myRecipes = recipeService.GetRecipesAsync().Result.Where(r => r.UserId == currentUserId);

            var (modelCardView, totalPages) = await recipeService.GetCurrPageRecipes(myRecipes, currentUserId, pageNumber, pageSize);

            // Set pagination data in ViewData
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;

            return View(modelCardView);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var recipe = await recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipeModel = await recipeService.GetRecipeDetailsViewModel(userId, recipe);

            await SetCookbooksInViewBag(userId!);

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

            var model = await recipeService.GetEditRecipeviewModel(recipe);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                model.AvailableIngredients = await ingredientService.GetAllIngredientsAsync();
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Search(string query, int pageNumber = 1, int pageSize = 9)
        {
            if (string.IsNullOrWhiteSpace(query))
            { //TODO: stay on the same page
                return RedirectToAction("Index", "Home");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<int> favoriteRecipeIds = new List<int>();

            if (userId != null)
            {
                var cookbooks = await favoriteService.GetUserCookbooksAsync(userId);

                //get all recipes from user cookbooks
                favoriteRecipeIds = cookbooks
                   .SelectMany(cb => cb.RecipeCookbooks)
                   .Select(rc => rc.RecipeId)
                   .ToList();

                // Map cookbooks to a strong-typed view model
                ViewBag.Cookbooks = cookbooks
                    .Select(c => new CookbookDropdownViewModel
                    {
                        Id = c.Id,
                        Title = c.Title
                    })
                    .ToList();
            }

            var searchResults = await recipeService.SearchRecipesAsync(query, favoriteRecipeIds);

            var currPageRecipes = searchResults
                .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only the records for the current page
                .ToList();

            var totalPages = (int)Math.Ceiling(searchResults.Count() / (double)pageSize);

            // Set pagination data in ViewData
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;

            return View(currPageRecipes);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id, int pageNumber)
        {
            var recipe = await recipeService.GetRecipeByIdAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (recipe == null || recipe.UserId != userId)
            {
                return View(nameof(Index));
            }

            DeleteRecipeViewModel model = new DeleteRecipeViewModel()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl
            };

            ViewData["PageNumber"] = pageNumber; // Pass it to the view

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRecipeViewModel recipe, int pageNumber = 1)
        {
            bool isDeleted = await recipeService.DeleteRecipeAsync(recipe.Id);

            if (!isDeleted)
            {
                TempData["ErrorMessage"] =
                    "Unexpected error occurred while trying to delete the recipe! Please contact system administrator!";
                return this.RedirectToAction(nameof(Delete), new { id = recipe.Id });
            }

            return RedirectToAction("MyRecipes", new { pageNumber });
        }

        private async Task SetCookbooksInViewBag(string userId)
        {
            var cookbooks = await favoriteService.GetUserCookbooksAsync(userId);

            ViewBag.Cookbooks = cookbooks
                .Select(c => new CookbookDropdownViewModel
                {
                    Id = c.Id,
                    Title = c.Title
                }).ToList();
        }
    }
}
