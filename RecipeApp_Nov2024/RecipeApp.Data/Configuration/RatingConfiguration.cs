using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Data.Models;
using static RecipeApp.Common.EntityValidationConstants.Rating;

namespace RecipeApp.Data.Configuration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Score)
                .IsRequired()
                .HasMaxLength(ScoreMaxValue);

            builder
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
