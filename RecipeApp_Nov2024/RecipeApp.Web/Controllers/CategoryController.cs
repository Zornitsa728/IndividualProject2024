using Microsoft.AspNetCore.Mvc;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CategoryViewModels;
using RecipeApp.Web.ViewModels.FavoritesViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        private IRecipeService _recipeService;
        private IFavoriteService _favoriteService;

        public CategoryController(ICategoryService categoryService, IRecipeService recipeService, IFavoriteService favoriteService)
        {
            _categoryService = categoryService;
            _recipeService = recipeService;
            _favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCategories = await _categoryService.GetAllCategories();

            IEnumerable<CategoryViewModel> model = allCategories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl
                })
            .ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryRecipes(int id)
        {
            Category? category = await _categoryService.GetCategory(id);

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var recipes = await _recipeService.GetAllRecipesAsync();

            IEnumerable<CategoryRecipeViewModel> recipesModel = recipes
                .Where(r => r.CategoryId == id & r.IsDeleted == false)
                .Select(rc => new CategoryRecipeViewModel()
                {
                    Id = rc.Id,
                    Title = rc.Title,
                    ImageUrl = rc.ImageUrl,
                })
                .ToList();

            ViewData["Title"] = category.Name;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return View("Index");
            }

            var cookbooks = await _favoriteService.GetUserCookbooksAsync(userId);

            // Map cookbooks to a strong-typed view model
            ViewBag.Cookbooks = cookbooks
                .Select(c => new CookbookDropdownViewModel
                {
                    Id = c.Id,
                    Title = c.Title
                })
                .ToList();

            return View(recipesModel);
        }
    }
}
