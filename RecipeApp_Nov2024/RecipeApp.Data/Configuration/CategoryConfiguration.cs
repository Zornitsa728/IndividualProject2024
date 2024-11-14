using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Data.Models;
using static RecipeApp.Common.EntityValidationConstants.Category;

namespace RecipeApp.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(NameMaxLength);

            builder.HasData(
                new Category { Id = 1, Name = "Appetizers" },
                new Category { Id = 2, Name = "Bread Recipes" },
                new Category { Id = 3, Name = "Breakfast" },
                new Category { Id = 4, Name = "Desserts" },
                new Category { Id = 5, Name = "Drinks" },
                new Category { Id = 6, Name = "Main Dishes" },
                new Category { Id = 7, Name = "Salads" },
                new Category { Id = 8, Name = "Sandwiches" },
                new Category { Id = 9, Name = "Side Dishes" },
                new Category { Id = 10, Name = "Soups" });

        }
    }
}
