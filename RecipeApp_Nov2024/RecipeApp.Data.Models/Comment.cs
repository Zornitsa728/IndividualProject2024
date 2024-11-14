using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime DatePosted { get; set; }

        public int RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public bool IsDeleted { get; set; }

    }
}
