using RecipeApp.Data.Models;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetRatingsAsync(int recipeId);
        Task AddRatingAsync(int recipeId, int score, string userId);
        bool CheckRecipeUserRating(int recipeId, string userId);
        Task<double> GetAverageRatingAsync(int recipeId);
        Task UpdateRatingAsync(int recipeId, int score, string userId);

    }
}
