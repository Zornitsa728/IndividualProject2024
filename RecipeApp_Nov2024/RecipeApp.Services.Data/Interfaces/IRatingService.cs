﻿using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetRatingsAsync(int recipeId);
        Task<Rating> AddRatingAsync(int recipeId, int score, string userId);
        bool CheckRecipeUserRating(int recipeId, string userId);
        Task<double> GetAverageRatingAsync(int recipeId);
        Task<Rating> UpdateRatingAsync(int recipeId, int score, string userId);

    }
}