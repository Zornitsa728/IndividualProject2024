namespace RecipeApp.Data.Models
{
    public enum UnitOfMeasurement
    {
        Gram,
        Kilogram,
        Liter,
        Milliliter,
        Cup,
        Tablespoon,
        Teaspoon,
        Piece,
        Pinch
    }
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; } = null!;

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; } = null!;

        public double Quantity { get; set; }

        public UnitOfMeasurement Unit { get; set; }

        public bool IsDeleted { get; set; }
    }
}
