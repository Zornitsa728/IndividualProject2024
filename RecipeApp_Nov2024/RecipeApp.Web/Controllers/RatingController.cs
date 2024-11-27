using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        //[HttpGet]
        //public async Task<ActionResult> GetRatings(int recipeId)
        //{
        //    var ratings = await _ratingService.GetRatingsAsync(recipeId);
        //    return Ok(ratings);
        //}

        [HttpPost]
        public async Task<ActionResult> PostRating(int recipeId, [FromBody] int score)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            var rating = await _ratingService.AddRatingAsync(recipeId, score, userId);
            return Ok(rating);
        }

        [HttpGet]
        public async Task<ActionResult> GetAverageRating(int recipeId)
        {
            double averageRating = await _ratingService.GetAverageRatingAsync(recipeId);
            return View(averageRating);//todo: return to partial view
        }
    }
}
