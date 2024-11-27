namespace RecipeApp.Web.ViewModels.CommentViewModels
{
    public class RecipeCommentsViewModel
    {
        public int RecipeId { get; set; }
        public IEnumerable<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }

}
