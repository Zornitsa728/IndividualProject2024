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

        public async Task<IEnumerable<CommentViewModel>> GetCommentsAsync(int recipeId)
        {
            if (await dbContext.Comments.AnyAsync(c => c.RecipeId == recipeId))
            {
                var comments = await dbContext.Comments
                    .Where(c => c.RecipeId == recipeId && !c.IsDeleted)
                    .OrderByDescending(c => c.DatePosted)
                    .ToListAsync();

                return comments
                .Select(c => new CommentViewModel()
                {
                    Content = c.Content,
                    RecipeId = c.RecipeId,
                    DatePosted = c.DatePosted
                })
                .ToList();
            }

            return new List<CommentViewModel>();
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

        public async Task<bool> DeleteCommentAsync(int recipeId, int commentId)
        {
            var comment = await dbContext.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId && c.RecipeId == recipeId);

            if (comment == null)
                return false;

            comment.IsDeleted = true; // Soft delete
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
