namespace RecipeApp.Common
{
    using static RecipeApp.Common.EntityValidationConstants.Recipe;

    public static class EntityValidationMessages
    {
        public static class Recipe
        {
            public const string TitleRequiredMessage = "Movie title is required";

            public const string CreateDateMessage = $"Release date is required in format {CreatedOnDateFormat}";

            public const string ImageUrlMessage = "Invalid URL.";

            public const string IngredientMessage = "At least one ingredient must be added.";

            public const string CategoryMessage = "At least one category must be selected.";
        }

        public static class Ingredient
        {
            public const string IngredientNameMessage = "Ingredient name is required.";

            public const string UnitOfMeasurmentMessage = "Unit of measurement is required.";

            public const string QuantityMessage = "Quantity is required.";


        }
    }
}
