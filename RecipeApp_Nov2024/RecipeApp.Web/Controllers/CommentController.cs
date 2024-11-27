using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CommentViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetComments(int recipeId)
        {
            var comments = await _commentService.GetCommentsAsync(recipeId);

            return View(comments);
        }

        [HttpPost]
        public async Task<ActionResult> PostComment(CommentViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            //todo: if user is not loged
            var comment = await _commentService.AddCommentAsync(model, userId);

            return RedirectToAction("Details", "Recipe", new { id = model.RecipeId });
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int recipeId, int commentId)
        {
            var result = await _commentService.DeleteCommentAsync(recipeId, commentId);
            if (!result) return NotFound();
            return Ok(new { Message = "Comment deleted successfully!" });
        }
    }
}
