using Microsoft.AspNetCore.Mvc;
using RecipeApp.Data;
using RecipeApp.Web.ViewModels.RecipeViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    public class RecipeController : Controller
    {
        private RecipeDbContext dbContext;

        public RecipeController(RecipeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            AddRecipeViewModel model = new AddRecipeViewModel();
            model.Categories = dbContext.Categories
                .ToList();

            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            return View(model);
        }


    }
}
