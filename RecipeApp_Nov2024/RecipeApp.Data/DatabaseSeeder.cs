using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RecipeApp.Data.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public static void SeedRecipesFromJson(RecipeDbContext context)
        {
            if (!context.Recipes.Any())
            {
                var path = Path.Combine(AppContext.BaseDirectory, "wwwroot", "recipes.json");

                var jsonData = File.ReadAllText(path);

                var recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonData);

                if (recipes != null)
                {
                    context.Recipes.AddRange(recipes);
                    context.SaveChanges();
                }
            }
        }

        public static void SeedRecipesIngredientsFromJson(RecipeDbContext context)
        {
            if (!context.RecipesIngredients.Any())
            {
                var path = Path.Combine(AppContext.BaseDirectory, "wwwroot", "recipesIngredients.json");

                var jsonData = File.ReadAllText(path);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // Ignore case for property names
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) } // Enum case handling
                };

                var recipeIngredients = JsonSerializer.Deserialize<List<RecipeIngredient>>(jsonData, options);

                if (recipeIngredients != null)
                {
                    context.RecipesIngredients.AddRange(recipeIngredients);
                    context.SaveChanges();
                }
            }
        }

        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin" };

            foreach (var role in roles)
            {
                var roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
                if (!roleExists)
                {
                    var result = roleManager.CreateAsync(new IdentityRole { Name = role }).GetAwaiter().GetResult();
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {role}");
                    }
                }
            }
        }

        public static void AssignAdminRole(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };
                var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {adminEmail}");
                }
            }

            var isInRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
            if (!isInRole)
            {
                var addRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign admin role to user: {adminEmail}");
                }
            }
        }
    }
}
