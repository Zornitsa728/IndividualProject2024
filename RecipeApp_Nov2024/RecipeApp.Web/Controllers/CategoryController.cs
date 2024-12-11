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
            var allCategories = await categoryService.GetAllCategoriesAsync();

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
        public async Task<IActionResult> CategoryRecipes(int id, int pageNumber = 1, int pageSize = 9)
        {
            Category? category = await categoryService.GetCategoryAsync(id);

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var recipes = await recipeService.GetRecipesAsync();

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<int> favoriteRecipeIds = new List<int>();

            if (userId != null)
            {
                var cookbooks = await favoriteService.GetUserCookbooksAsync(userId);

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

            var totalPages = (int)Math.Ceiling(recipesModel.Count() / (double)pageSize);

            var currPageRecipes = recipesModel
                .Skip((pageNumber - 1) * pageSize) // Skip records for previous pages
                .Take(pageSize) // Take only the records for the current page
                .ToList();

            ViewData["Title"] = category.Name;

            // Set pagination data in ViewData
            ViewData["CurrentPage"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;

            return View(currPageRecipes);
        }
    }
}
