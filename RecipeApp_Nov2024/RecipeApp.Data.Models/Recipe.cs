using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Data.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string Description { get; set; } = null!;

        public string Instructions { get; set; } = null!;

        public string? ImageUrl { get; set; }

        // UserId uses string because IdentityUser uses string for the Id
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } =
            new HashSet<RecipeIngredient>();

        public ICollection<Comment> Comments { get; set; } =
            new HashSet<Comment>();

        public ICollection<Rating> Ratings { get; set; } =
            new HashSet<Rating>();

        // Navigation property for the cookbooks containing this recipe
        public ICollection<RecipeCookbook> RecipeCookbooks { get; set; } = new HashSet<RecipeCookbook>();

        public bool IsDeleted { get; set; }
    }
}
