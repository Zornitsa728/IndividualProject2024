using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.CommentViewModels;

namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class RecipeDetailsViewModel
    {
        public Recipe Recipe { get; set; } = null!;
        public RecipeCommentsViewModel Comments { get; set; } = null!;
        public double AverageRating { get; set; }
    }

}
