using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.CommentViewModels;

namespace RecipeApp.Services.Data
{
    public class CommentService : ICommentService
    {
        private readonly RecipeDbContext dbContext;

        public CommentService(RecipeDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(int recipeId)
        {
            if (await dbContext.Comments.AnyAsync(c => c.RecipeId == recipeId))
            {
                var comments = await dbContext.Comments
                    .Include(c => c.User)
                    .Where(c => c.RecipeId == recipeId && !c.IsDeleted)
                    .OrderByDescending(c => c.DatePosted)
                    .ToListAsync();

                return comments;
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

            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteCommentAsync(int recipeId, int commentId, string userId)
        {
            var comment = await dbContext.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId && c.RecipeId == recipeId);

            if (comment == null)
                return false;

            if (comment.UserId != userId)
                return false;

            comment.IsDeleted = true; // Soft delete
            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task EditCommentAsync(int commentId, string content)
        {
            var comment = await dbContext.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            comment.Content = content;

            await dbContext.SaveChangesAsync();
        }
    }
}
