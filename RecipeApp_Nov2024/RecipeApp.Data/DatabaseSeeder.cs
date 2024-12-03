using RecipeApp.Data.Models;
using System.Text.Json;

namespace RecipeApp.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedIngredientsFromJson(RecipeDbContext context)
        {

            if (!context.Ingredients.Any())
            {
                var path = Path.Combine(AppContext.BaseDirectory, "wwwroot", "ingredients.json");

                // Read the JSON file
                var jsonData = File.ReadAllText(path);

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
