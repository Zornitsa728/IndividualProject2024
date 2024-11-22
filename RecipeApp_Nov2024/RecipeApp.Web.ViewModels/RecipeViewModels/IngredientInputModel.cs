using RecipeApp.Data.Models;
using System.ComponentModel.DataAnnotations;

using static RecipeApp.Common.EntityValidationConstants.Ingredient;
using static RecipeApp.Common.EntityValidationMessages.Ingredient;

namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class IngredientInputModel
    {
        [Required]
        public int? IngredientId { get; set; } // For existing ingredient

        public string? NewIngredientName { get; set; } // For new ingredient


        [Required(ErrorMessage = QuantityMessage)]
        [Range(QuantityMinLength, QuantityMaxLength)]
        public double Quantity { get; set; }

        [Required(ErrorMessage = UnitOfMeasurmentMessage)]
        public UnitOfMeasurement Unit { get; set; }
    }
}
