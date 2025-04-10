using Microsoft.AspNetCore.Mvc;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CategoryViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService categoryService;
        private IRecipeService recipeService;
        private IFavoriteService favoriteService;

        public CategoryController(ICategoryService categoryService, IRecipeService recipeService, IFavoriteService favoriteService)
        {
            this.categoryService = categoryService;
            this.recipeService = recipeService;
            this.favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CategoryViewModel> model = await categoryService.GetCategoriesViewModelAsync();

            return View(model);
        }

        [HttpGet] //only recepies 
        public async Task<IActionResult> CategoryRecipes(int id, int pageNumber = 1, int pageSize = 9)
        {
            Category? category = await categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var recipes = await recipeService.GetRecipesAsync();

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var (currPageRecipes, totalPages) = await categoryService.GetCurrPageRecipesForCategoryAsync(recipes, id, userId, pageNumber, pageSize);

            if (userId != null)
            {
                ViewBag.Cookbooks = await favoriteService.GetCookbookDropdownsAsync(userId);
            }

            ViewData["Title"] = category.Name;

            // Set pagination data in ViewData
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;

            return View(currPageRecipes);
        }
    }
}
