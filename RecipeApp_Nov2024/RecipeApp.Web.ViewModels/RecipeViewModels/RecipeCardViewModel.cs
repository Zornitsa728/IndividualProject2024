namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class RecipeCardViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool Liked { get; set; }
    }
}
