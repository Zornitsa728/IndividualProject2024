namespace RecipeApp.Data.Models
{
    public class RecipeCookbook
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        public int CookbookId { get; set; }
        public Cookbook Cookbook { get; set; } = null!;

    }
}
