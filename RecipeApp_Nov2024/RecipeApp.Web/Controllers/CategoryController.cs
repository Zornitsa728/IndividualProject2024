using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.CategoryViewModels;

namespace RecipeApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private RecipeDbContext dbContext;

        public CategoryController(RecipeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryViewModel> allCategories = await dbContext
                .Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl
                })
            .AsNoTracking()
            .ToListAsync();

            return View(allCategories);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryRecipes(int id)
        {
            Category? category = dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<CategoryRecipeViewModel> recipes = await dbContext
                .Recipes
                .Where(r => r.CategoryId == id & r.IsDeleted == false)
                .Select(rc => new CategoryRecipeViewModel()
                {
                    Id = rc.Id,
                    Title = rc.Title,
                    ImageUrl = rc.ImageUrl,
                })
                .AsNoTracking()
                .ToListAsync();

            ViewData["Title"] = category.Name;

            return View(recipes);
        }
    }
}
