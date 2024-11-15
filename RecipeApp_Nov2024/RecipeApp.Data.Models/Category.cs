namespace RecipeApp.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public ICollection<RecipeCategory> RecipeCategories { get; set; } =
            new HashSet<RecipeCategory>();
    }
}
