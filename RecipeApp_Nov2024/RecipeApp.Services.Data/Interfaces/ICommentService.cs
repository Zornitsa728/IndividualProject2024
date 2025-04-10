using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.CommentViewModels;

namespace RecipeApp.Services.Data.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsAsync(int recipeId);
        Task<Comment> AddCommentAsync(CommentViewModel model, string userId);
        Task<bool> DeleteCommentAsync(int recipeId, int commentId, string userId);
        Task<CommentViewModel> GetCommentViewModel(Comment comment);
        Task EditCommentAsync(int commentId, string content);
    }
}
