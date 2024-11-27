using RecipeApp.Data.Models;
using System.Text.Json;

namespace RecipeApp.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedIngredientsFromJson(RecipeDbContext context)
        {

            if (!context.Ingredients.Any())
            {   //TODO: the path getter is not working so i need to fix this later 
                var path = "D:\\C#\\Source\\repos\\IndividualProject2024\\RecipeApp_Nov2024\\RecipeApp.Web\\wwwroot\\ingredients.json";

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
