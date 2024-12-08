using Microsoft.AspNetCore.Mvc;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CategoryViewModels;
using RecipeApp.Web.ViewModels.FavoritesViewModels;
using RecipeApp.Web.ViewModels.RecipeViewModels;
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

        [HttpGet] //only recepies 
        public async Task<IActionResult> CategoryRecipes(int id)
        {
            Category? category = await _categoryService.GetCategory(id);

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var recipes = await _recipeService.GetAllRecipesAsync();

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<int> favoriteRecipeIds = new List<int>();

            if (userId != null)
            {
                var cookbooks = await _favoriteService.GetUserCookbooksAsync(userId);

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

            IEnumerable<RecipeCardViewModel> recipesModel = recipes
                .Where(r => r.CategoryId == id & r.IsDeleted == false)
                .Select(rc => new RecipeCardViewModel()
                {
                    Id = rc.Id,
                    Title = rc.Title,
                    ImageUrl = rc.ImageUrl,
                    Liked = favoriteRecipeIds.Contains(rc.Id)
                })
                .ToList();

            ViewData["Title"] = category.Name;

            return View(recipesModel);
        }
    }
}
