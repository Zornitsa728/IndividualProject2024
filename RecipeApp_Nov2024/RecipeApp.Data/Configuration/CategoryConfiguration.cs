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
                new Category
                {
                    Id = 1,
                    Name = "Appetizers",
                    ImageUrl = "/images/Category/appetizers.jpg"
                },
                new Category
                {
                    Id = 2,
                    Name = "Bread Recipes",
                    ImageUrl = "/images/Category/breadRecipes.jpg"
                },
                new Category
                {
                    Id = 3,
                    Name = "Breakfast",
                    ImageUrl = "/images/Category/breakfast.jpg"
                },
                new Category
                {
                    Id = 4,
                    Name = "Desserts",
                    ImageUrl = "/images/Category/desserts.jpg"
                },
                new Category
                {
                    Id = 5,
                    Name = "Drinks",
                    ImageUrl = "/images/Category/drinks.jpg"
                },
                new Category
                {
                    Id = 6,
                    Name = "Main Dishes",
                    ImageUrl = "/images/Category/mainDishes.jpg"
                },
                new Category
                {
                    Id = 7,
                    Name = "Salads",
                    ImageUrl = "/images/Category/salads.jpg"
                },
                new Category
                {
                    Id = 8,
                    Name = "Sandwiches",
                    ImageUrl = "/images/Category/sandwiches.jpg"
                },
                new Category
                {
                    Id = 9,
                    Name = "Side Dishes",
                    ImageUrl = "/images/Category/sideDishes.jpg"
                },
                new Category
                {
                    Id = 10,
                    Name = "Soups",
                    ImageUrl = "/images/Category/soups.jpg"
                });

        }
    }
}
