using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Data.Models;

namespace RecipeApp.Data.Configuration
{
    public class RecipeCategoryConfiguration : IEntityTypeConfiguration<RecipeCategory>
    {
        public void Configure(EntityTypeBuilder<RecipeCategory> builder)
        {
            builder.HasKey(rc => new { rc.RecipeId, rc.CategoryId });

            builder.Property(rc => rc.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(rc => rc.Recipe)
                .WithMany(r => r.RecipeCategories)
                .HasForeignKey(rc => rc.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rc => rc.Category)
                .WithMany(c => c.RecipeCategories)
                .HasForeignKey(rc => rc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
