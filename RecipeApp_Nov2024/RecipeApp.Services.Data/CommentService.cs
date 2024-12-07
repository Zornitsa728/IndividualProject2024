using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CommentViewModels;

namespace RecipeApp.Services.Data
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment, int> commentRepository;

        public CommentService(IRepository<Comment, int> commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(int recipeId)
        {
            var comments = await commentRepository.GetAllAttached()
                .Include(c => c.User).ToListAsync();

            if (comments.Any(c => c.RecipeId == recipeId))
            {
                return comments.Where(c => c.RecipeId == recipeId && !c.IsDeleted)
                          .OrderByDescending(c => c.DatePosted)
                          .ToList();
            }

            return new List<Comment>();
        }

        public async Task<Comment> AddCommentAsync(CommentViewModel model, string userId)
        {
            var comment = new Comment
            {
                Content = model.Content,
                DatePosted = model.DatePosted,
                RecipeId = model.RecipeId,
                UserId = userId,
                IsDeleted = false
            };

            await commentRepository.AddAsync(comment);
            return comment;
        }

        public async Task<bool> DeleteCommentAsync(int recipeId, int commentId, string userId)
        {
            var comment = await commentRepository.GetByIdAsync(commentId);

            if (comment == null || comment.RecipeId != recipeId || comment.UserId != userId)
                return false;

            comment.IsDeleted = true; // Soft delete
            await commentRepository.UpdateAsync(comment);
            return true;
        }

        public async Task EditCommentAsync(int commentId, string content)
        {
            var comment = await commentRepository.GetByIdAsync(commentId);
            if (comment != null)
            {
                comment.Content = content;
                await commentRepository.UpdateAsync(comment);
            }
        }
    }
}
