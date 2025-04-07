using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
    }
}