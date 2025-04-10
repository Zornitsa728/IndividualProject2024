using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CategoryViewModels;
using RecipeApp.Web.ViewModels.FavoritesViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoriteService favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            List<CookbookViewModel> model = await favoriteService.GetCookbooksViewModelAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewRecipes(int cookbookId)
        {
            var cookbook = await favoriteService.GetCookbookWithRecipesAsync(cookbookId);

            if (cookbook == null)
            {
                return RedirectToAction("Index");
            }

            CookbookViewModel model = new CookbookViewModel
            {
                Id = cookbook.Id,
                Title = cookbook.Title,
                Description = cookbook.Description,
                UserId = cookbook.UserId,
                Recipes = cookbook.RecipeCookbooks.Select(rc => new CategoryRecipeViewModel
                {
                    Id = rc.RecipeId,
                    Title = rc.Recipe.Title,
                    ImageUrl = rc.Recipe.ImageUrl
                }).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CookbookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await favoriteService.CreateCookbookAsync(model, userId);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> AddRecipe(int cookbookId, int recipeId, string returnUrl)
        {
            await favoriteService.AddRecipeToCookbookAsync(cookbookId, recipeId);

            // Redirect back to the return URL if provided, otherwise redirect to a default page
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Favorites");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCookbook(int cookbookId, int recipeId)
        {
            await favoriteService.RemoveRecipeFromCookbookAsync(cookbookId, recipeId);

            // Redirect back to the current cookbook's page
            return RedirectToAction("ViewRecipes", "Favorites", new { cookbookId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCookbook(int cookbookId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await favoriteService.RemoveCookbookAsync(cookbookId);

            return RedirectToAction(nameof(Index));
        }
    }
}