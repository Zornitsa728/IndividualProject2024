namespace RecipeApp.Data.Models
{
    public class RecipeCategory
    {
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}