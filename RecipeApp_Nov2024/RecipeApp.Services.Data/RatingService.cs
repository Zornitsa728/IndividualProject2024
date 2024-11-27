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
    }
}
