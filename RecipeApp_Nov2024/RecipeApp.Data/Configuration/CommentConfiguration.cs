using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeApp.Data.Models;
using static RecipeApp.Common.EntityValidationConstants.Comment;

namespace RecipeApp.Data.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(ContentMaxLength);

            // Configuring the foreign key relationship with User 
            builder.
                HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
