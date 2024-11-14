using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; }

        public int RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;
    }
}
