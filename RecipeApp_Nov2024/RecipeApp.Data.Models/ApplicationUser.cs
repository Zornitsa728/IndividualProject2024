using Microsoft.AspNetCore.Identity;

namespace RecipeApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
        public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}
