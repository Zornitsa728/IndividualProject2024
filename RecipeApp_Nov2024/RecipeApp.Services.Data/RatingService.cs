﻿using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Data
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating, int> ratingRepository;

        public RatingService(IRepository<Rating, int> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task<IEnumerable<Rating>> GetRatingsAsync(int recipeId)
        {
            return await ratingRepository.GetAllAttached()
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

            await ratingRepository.AddAsync(rating);
            return rating;
        }

        public async Task<double> GetAverageRatingAsync(int recipeId)
        {
            IQueryable<Rating>? currRecipeRatings = ratingRepository
                .GetAllAttached()
                .Where(r => r.RecipeId == recipeId);

            if (currRecipeRatings != null)
            {
                return await currRecipeRatings
                .AverageAsync(r => r.Score);
            }

            return 0;
        }

        public bool CheckRecipeUserRating(int recipeId, string userId)
        {
            var rating = ratingRepository.GetAllAttached()
                .FirstOrDefault(r => r.RecipeId == recipeId && r.UserId == userId);

            if (rating != null)
            {
                return false;
            }

            return true;
        }

        public async Task<Rating> UpdateRatingAsync(int recipeId, int score, string userId)
        {
            var rating = ratingRepository
                .GetAllAttached()
                .First(r => r.RecipeId == recipeId && r.UserId == userId);

            rating.Score = score;

            await ratingRepository.UpdateAsync(rating);

            return rating;
        }
    }
}
