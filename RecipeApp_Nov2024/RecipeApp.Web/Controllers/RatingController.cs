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
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<ActionResult> GetRatings(int recipeId)
        {
            var ratings = await _ratingService.GetRatingsAsync(recipeId);
            return Ok(ratings);
        }

        [HttpPost]
        public async Task<ActionResult> PostRating(int recipeId, int score)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            var isRatingValid = _ratingService.CheckRecipeUserRating(recipeId, userId);

            Rating rating;

            if (!isRatingValid)
            {
                rating = await _ratingService.UpdateRatingAsync(recipeId, score, userId);
            }
            else
            {
                rating = await _ratingService.AddRatingAsync(recipeId, score, userId);
            }

            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

        [HttpGet]
        public async Task<ActionResult> GetAverageRating(int recipeId)
        {
            double averageRating = await _ratingService.GetAverageRatingAsync(recipeId);
            return View(averageRating);//todo: return to partial view

        }
    }
}
