namespace RecipeApp.Web.ViewModels.Recipe
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}
