using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.Recipe;

namespace RecipeApp.Web.Controllers
{
    public class RecipeController : Controller
    {
        private RecipeDbContext dbContext;

        public RecipeController(RecipeDbContext dbContext)
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
        public async Task<IActionResult> Category(int id)
        {
            if (!dbContext.Categories.Any(c => c.Id == id))
            {
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<Recipe> recipes = await dbContext.Recipes
                .Where(r => r.RecipeCategories.All(r => r.CategoryId == id))
                .AsNoTracking()
                .ToListAsync();

            return View(recipes);
        }
    }
}
