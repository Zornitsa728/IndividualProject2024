using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Data.Models;

using static RecipeApp.Common.EntityValidationConstants.Ingredient;

namespace RecipeApp.Data.Configuration
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            builder.HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ri => ri.Quantity)
                .IsRequired(true)
                .HasMaxLength(QuantityMaxLength);

            builder.Property(ri => ri.Unit)
                .IsRequired(true);
        }

        private List<RecipeIngredient> SeedRecipeIngredients()
        {
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>()
            {
                 new RecipeIngredient()
                 {
                     IngredientId = 1,
                     RecipeId = 1,
                     Unit = UnitOfMeasurement.Milliliter,
                     Quantity = 250
                 },
                new RecipeIngredient()
                {
                    IngredientId = 3,
                    RecipeId = 1,
                    Unit = UnitOfMeasurement.Gram,
                    Quantity = 100
                },
                new RecipeIngredient()
                {
                    IngredientId = 1,
                    RecipeId = 2,
                    Unit = UnitOfMeasurement.Milliliter,
                    Quantity = 100
                },
                new RecipeIngredient()
                {
                    IngredientId = 4,
                    RecipeId = 2,
                    Unit = UnitOfMeasurement.Gram,
                    Quantity = 50
                }
            };

            return recipeIngredients;
        }
    }
}
