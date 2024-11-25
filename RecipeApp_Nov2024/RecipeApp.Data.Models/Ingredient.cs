namespace RecipeApp.Data.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } =
            new HashSet<RecipeIngredient>();

        // Recipes using the ingredient won't break when an ingredient is "deleted."
        public bool IsDeleted { get; set; }
    }
}
