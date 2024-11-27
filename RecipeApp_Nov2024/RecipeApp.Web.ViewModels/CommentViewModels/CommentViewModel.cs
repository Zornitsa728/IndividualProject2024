namespace RecipeApp.Web.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            DatePosted = DateTime.UtcNow;
        }

        public string Content { get; set; } = null!;
        public DateTime DatePosted { get; set; }

        public int RecipeId { get; set; }

        public string UserId { get; set; } = null!;

        public string? UserName { get; set; }
    }
}
