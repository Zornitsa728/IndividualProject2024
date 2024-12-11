using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpGet]
        public async Task<ActionResult> GetRatings(int recipeId)
        {
            IEnumerable<Rating>? ratings = await ratingService.GetRatingsAsync(recipeId);

            return Ok(ratings);
        }

        [HttpPost]
        public async Task<ActionResult> PostRating(int recipeId, int score)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            var isRatingValid = ratingService.CheckRecipeUserRating(recipeId, userId);

            Rating rating;

            if (!isRatingValid)
            {
                rating = await ratingService.UpdateRatingAsync(recipeId, score, userId);
            }
            else
            {
                rating = await ratingService.AddRatingAsync(recipeId, score, userId);
            }

            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

        [HttpGet]
        public async Task<ActionResult> GetAverageRating(int recipeId)
        {
            double averageRating = await ratingService.GetAverageRatingAsync(recipeId);
            return View(averageRating);//todo: return to partial view

        }
    }
}
