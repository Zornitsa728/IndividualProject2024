using RecipeApp.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Web.ViewModels.RecipeViewModels
{
    public class IngredientViewModel
    {
        [Required]
        public int IngredientId { get; set; }

        public string? Name { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasurement Unit { get; set; }
    }
}
