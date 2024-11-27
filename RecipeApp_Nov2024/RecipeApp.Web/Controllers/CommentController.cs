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

            List<CommentViewModel> model = comments
                .Select(c => new CommentViewModel()
                {
                    Content = c.Content,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
                    RecipeId = c.RecipeId,
                    DatePosted = c.DatePosted
                })
                .ToList();

            //RecipeCommentsViewModel recipeCommentsViewModel = new RecipeCommentsViewModel()
            //{
            //    RecipeId = recipeId,
            //    Comments = model,
            //};

            return View(comments); // todo: make comment view model and use it 
        }

        [HttpPost]
        public async Task<ActionResult> PostComment(CommentViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            //todo: if user is not loged
            var comment = await _commentService.AddCommentAsync(model, userId);
            return RedirectToAction("Details", "Recipe");
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
