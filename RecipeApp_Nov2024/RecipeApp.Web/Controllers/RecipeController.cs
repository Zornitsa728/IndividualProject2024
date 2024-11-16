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
            Category? category = dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<RecipeViewModel> recipes = await dbContext
                .Recipes
                .Where(r => r.RecipeCategories.Any(rc => rc.CategoryId == id))
                .Select(rc => new RecipeViewModel()
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
