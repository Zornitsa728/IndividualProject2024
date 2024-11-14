using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Data.Models;
using static RecipeApp.Common.EntityValidationConstants.Ingredient;

namespace RecipeApp.Data.Configuration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.Property(i => i.Quantity)
                .IsRequired()
                .HasMaxLength(QuantityMaxLength);
        }
    }
}
