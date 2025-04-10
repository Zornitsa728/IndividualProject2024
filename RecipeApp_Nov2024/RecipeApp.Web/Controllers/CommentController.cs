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
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        //[HttpGet]
        //public async Task<ActionResult> GetComments(int recipeId)
        //{
        //    var comments = await commentService.GetCommentsAsync(recipeId);

        //    return View(comments);
        //}

        [HttpPost]
        public async Task<ActionResult> PostComment(CommentViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            //todo: if user is not loged
            var comment = await commentService.AddCommentAsync(model, userId);

            return RedirectToAction("Details", "Recipe", new { id = model.RecipeId });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteComment(int recipeId, int commentId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            var result = await commentService.DeleteCommentAsync(recipeId, commentId, userId);

            if (!result) return RedirectToAction("Index", "Home");

            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }

        [HttpPost]
        public async Task<ActionResult> EditComment(int recipeId, int commentId)
        {
            var comments = await commentService.GetCommentsAsync(recipeId);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            var comment = comments.FirstOrDefault(c => c.Id == commentId);

            if (comment == null || comment.UserId != userId)
                return RedirectToAction("Details", "Recipe", new { id = recipeId });

            CommentViewModel commentModel = await commentService.GetCommentViewModel(comment);

            return View(commentModel);
        }

        [HttpGet]
        public async Task<ActionResult> EditComment(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await commentService.EditCommentAsync(model.CommentId, model.Content);

            return RedirectToAction("Details", "Recipe", new { id = model.RecipeId });
        }
    }
}
