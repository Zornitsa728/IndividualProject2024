using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Data.Models;

namespace RecipeApp.Data.Configuration
{
    public class RecipeCookbookConfiguration : IEntityTypeConfiguration<RecipeCookbook>
    {
        public void Configure(EntityTypeBuilder<RecipeCookbook> builder)
        {
            builder.HasKey(rc => new { rc.RecipeId, rc.CookbookId });

            builder.HasOne(rc => rc.Recipe)
                .WithMany(r => r.RecipeCookbooks)
                .HasForeignKey(rc => rc.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(rc => rc.Cookbook)
                .WithMany(c => c.RecipeCookbooks)
                .HasForeignKey(rc => rc.CookbookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
