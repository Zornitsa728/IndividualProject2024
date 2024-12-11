using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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


        [HttpPost]
        public async Task<ActionResult> PostRating(int recipeId, int score)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            var isRatingValid = ratingService.CheckRecipeUserRating(recipeId, userId);

            if (!isRatingValid)
            {
                await ratingService.UpdateRatingAsync(recipeId, score, userId);
            }
            else
            {
                await ratingService.AddRatingAsync(recipeId, score, userId);
            }

            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

    }
}
