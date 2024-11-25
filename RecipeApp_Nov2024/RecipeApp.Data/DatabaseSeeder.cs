using RecipeApp.Data.Models;
using System.Text.Json;

namespace RecipeApp.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedIngredientsFromJson(RecipeDbContext context)
        {
            if (!context.Ingredients.Any()) // Avoid duplicate entries
            {

                // Read the JSON file
                var jsonData = File.ReadAllText("D:\\C#\\Source\\repos\\IndividualProject2024\\RecipeApp_Nov2024\\RecipeApp.Web\\wwwroot\\ingredients.json");

                // Deserialize JSON to a list of ingredients
                var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(jsonData);

                if (ingredients != null)
                {
                    context.Ingredients.AddRange(ingredients);
                    context.SaveChanges();
                }
            }
        }
    }
}
