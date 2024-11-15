using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
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
            IEnumerable<RecipeIndexViewModel> allCategories = await dbContext
                .Categories
                .Select(c => new RecipeIndexViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl
                })
            .ToListAsync();

            return View(allCategories);
        }
    }
}
