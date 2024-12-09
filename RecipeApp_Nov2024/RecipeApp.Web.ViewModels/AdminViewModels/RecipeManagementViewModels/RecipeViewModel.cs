using System.ComponentModel.DataAnnotations;
using static RecipeApp.Common.EntityValidationConstants.Recipe;
using static RecipeApp.Common.EntityValidationMessages.Recipe;

namespace RecipeApp.Web.ViewModels.AdminViewModels.RecipeManagementViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MinLength(InstructionsMinLength)]
        [MaxLength(InstructionsMaxLength)]
        public string Instructions { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

    }
}
