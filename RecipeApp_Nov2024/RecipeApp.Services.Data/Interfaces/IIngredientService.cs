using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IIngredientService
    {
        IList<Ingredient> GetAllIngredients();
    }
}
