﻿using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeDbContext dbContext;

        public RecipeService(RecipeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Add a new recipe to the database.
        /// </summary>
        /// <param name="recipe">The recipe to add.</param>
        public void AddRecipe(Recipe recipe)
        {
            dbContext.Recipes.Add(recipe);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Get all recipes from the database.
        /// </summary>
        /// <returns>List of recipes.</returns>
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return dbContext.Recipes
                .Where(r => !r.IsDeleted)
                .ToList();
        }

        /// <summary>
        /// Get a recipe by its ID.
        /// </summary>
        /// <param name="id">The recipe ID.</param>
        /// <returns>A single recipe, or null if not found.</returns>
        public Recipe? GetRecipeById(int id)
        {
            return dbContext.Recipes
                .FirstOrDefault(r => r.Id == id && !r.IsDeleted);
        }

        /// <summary>
        /// Update an existing recipe.
        /// </summary>
        /// <param name="recipe">The updated recipe.</param>
        public void UpdateRecipe(Recipe recipe)
        {
            dbContext.Recipes.Update(recipe);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Delete a recipe by marking it as deleted.
        /// </summary>
        /// <param name="id">The recipe ID to delete.</param>
        public void DeleteRecipe(int id)
        {
            var recipe = dbContext.Recipes.FirstOrDefault(r => r.Id == id);
            if (recipe != null)
            {
                recipe.IsDeleted = true;
                dbContext.SaveChanges();
            }
        }
    }
}
