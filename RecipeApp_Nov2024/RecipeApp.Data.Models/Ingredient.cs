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
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Quantity { get; set; }
        public UnitOfMeasurement Unit { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } =
            new HashSet<RecipeIngredient>();

        // TODO: Soft Delete 
    }
}
