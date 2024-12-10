namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class DeleteRecipeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
