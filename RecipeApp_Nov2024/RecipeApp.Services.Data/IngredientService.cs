using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class IngredientService : IIngredientService
    {
        private readonly RecipeDbContext dbContext;

        public IngredientService(RecipeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Ingredient> GetAllIngredients()
        {
            return dbContext.Ingredients
                .ToList();
        }

    }
}
