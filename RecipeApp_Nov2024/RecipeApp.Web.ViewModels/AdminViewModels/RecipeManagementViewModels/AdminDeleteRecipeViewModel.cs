namespace RecipeApp.Web.ViewModels.AdminViewModels.RecipeManagementViewModels
{
    public class AdminDeleteRecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }

}
