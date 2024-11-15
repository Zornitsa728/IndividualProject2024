using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Web.ViewModels.Recipe;

namespace RecipeApp.Web.Views.Shared.Components.CategoryDropdown
{
    public class CategoryDropdownViewComponent : ViewComponent
    {
        private readonly RecipeDbContext dbContext;

        public CategoryDropdownViewComponent(RecipeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await dbContext
                .Categories
                 .Select(c => new CategoryViewModel()
                 {
                     Id = c.Id,
                     Name = c.Name,
                     ImageUrl = c.ImageUrl
                 })
                .ToListAsync();

            return View(categories);
        }
    }
}
