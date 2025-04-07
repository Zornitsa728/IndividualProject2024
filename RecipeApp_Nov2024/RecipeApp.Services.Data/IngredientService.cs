using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepository<Ingredient, int> ingredientRepository;

        public IngredientService(IRepository<Ingredient, int> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }
        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            var ingredients = await ingredientRepository
                .GetAllAsync();

            return ingredients;

        }
    }
}
