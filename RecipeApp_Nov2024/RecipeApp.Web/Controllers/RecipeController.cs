using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Services.Data.Interfaces;
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
            if (userId != null)
            {
                ViewBag.Cookbooks = await favoriteService.GetCookbookDropdownsAsync(userId);
            }

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

            // If the current page has no recipes and it's not the first page, redirect to the previous page
            if (!modelCardView.Any() && pageNumber > 1)
            {
                return RedirectToAction(nameof(MyRecipes), new { pageNumber = pageNumber - 1, pageSize });
            }

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

            if (userId != null)
            {
                ViewBag.Cookbooks = await favoriteService.GetCookbookDropdownsAsync(userId);
            }

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

            var model = await recipeService.GetEditRecipeViewModel(recipe);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllCategoriesAsync();
                model.AvailableIngredients = await ingredientService.GetAllIngredientsAsync();
                model.UnitsOfMeasurement = recipeService.GetUnitsOfMeasurementSelectList();
                return View(model);
            }

            var recipe = await recipeService.GetRecipeByIdAsync(model.Id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Update recipe details
            recipe = await recipeService.UpdateRecipeDetails(recipe, model);

            // Update ingredients if ingredients > 0
            var updatedIngredients = await recipeService.UpdateRecipeIngredients(recipe, model);

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

            // Map cookbooks to a strong-typed view model
            if (userId != null)
            {
                ViewBag.Cookbooks = await favoriteService.GetCookbookDropdownsAsync(userId);
            }

            var (searchResults, totalPages) = await recipeService.SearchRecipesAsync(query, userId, pageNumber, pageSize);

            // Set pagination data in ViewData
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;

            return View(searchResults);
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

            DeleteRecipeViewModel model = await recipeService.GetDeleteRecipeViewModel(recipe);

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
    }
}
