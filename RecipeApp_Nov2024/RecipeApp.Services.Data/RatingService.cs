using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class RatingService : IRatingService
    {
        private readonly RecipeDbContext dbContext;

        public RatingService(RecipeDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Rating>> GetRatingsAsync(int recipeId)
        {
            return await dbContext.Ratings
                .Where(r => r.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task<Rating> AddRatingAsync(int recipeId, int score, string userId)
        {
            var rating = new Rating
            {
                RecipeId = recipeId,
                UserId = userId,
                Score = score
            };

            dbContext.Ratings.Add(rating);
            await dbContext.SaveChangesAsync();
            return rating;
        }

        public async Task<double> GetAverageRatingAsync(int recipeId)
        {
            if (await dbContext.Ratings.AnyAsync(r => r.RecipeId == recipeId))
            {
                return await dbContext.Ratings
                .Where(r => r.RecipeId == recipeId)
                .AverageAsync(r => r.Score);
            }

            return 0;
        }

        public bool CheckRecipeUserRating(int recipeId, string userId)
        {
            var rating = dbContext.Ratings.FirstOrDefault(r => r.RecipeId == recipeId && r.UserId == userId);

            if (rating != null)
            {
                return false;
            }

            return true;
        }

        public async Task<Rating> UpdateRatingAsync(int recipeId, int score, string userId)
        {
            var rating = dbContext.Ratings.First(r => r.RecipeId == recipeId && r.UserId == userId);

            rating.Score = score;

            dbContext.Ratings.Update(rating);
            await dbContext.SaveChangesAsync();

            return rating;
        }
    }
}
