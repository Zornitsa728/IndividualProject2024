using RecipeApp.Data.Models;
using RecipeApp.Web.ViewModels.CommentViewModels;
using RecipeApp.Web.ViewModels.RatingViewModels;

namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class RecipeDetailsViewModel
    {
        public Recipe Recipe { get; set; } = null!;
        public RecipeCommentsViewModel Comments { get; set; } = null!;
        public RatingViewModel Rating { get; set; } = null!;
        public bool Liked { get; set; }
    }

}
