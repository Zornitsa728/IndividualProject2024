namespace RecipeApp.Data.Models
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = null!;

        public double Quantity { get; set; }

        public bool IsDeleted { get; set; }
    }
}
