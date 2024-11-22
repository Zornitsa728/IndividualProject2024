using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data.Models;
using System.Reflection;

namespace RecipeApp.Data
{
    public class RecipeDbContext : IdentityDbContext<ApplicationUser>
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Ingredient> Ingredients { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<Rating> Ratings { get; set; } = null!;

        public DbSet<RecipeIngredient> RecipesIngredients { get; set; } = null!;

        public DbSet<RecipeCookbook> RecipesCookbooks { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
             .HasData(
             new ApplicationUser { Id = "9ccd592c-f245-4344-b4ed-dde7df4677e1" },
             new ApplicationUser { Id = "6ca87836-1e87-4648-803f-c4c416c5d850" }
             );

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
