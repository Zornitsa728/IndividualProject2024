namespace RecipeApp.Data.Models
{
    public class Cookbook
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        // Foreign key to the user who owns this cookbook
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        // Navigation property for the recipes in this cookbook
        public ICollection<RecipeCookbook> RecipeCookbooks { get; set; } = new HashSet<RecipeCookbook>();
    }
}
