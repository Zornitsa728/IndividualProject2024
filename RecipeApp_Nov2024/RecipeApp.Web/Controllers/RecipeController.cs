using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.RecipeViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Controllers
{
    public class RecipeController : Controller
    {
        //private RecipeDbContext dbContext;

        //public RecipeController(RecipeDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}

        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;
        private readonly IIngredientService _ingredientService;

        public RecipeController(
           IRecipeService recipeService,
           ICategoryService categoryService,
           IIngredientService ingredientService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var recipes = _recipeService.GetAllRecipes();
            return View(recipes); // Create a view to display all recipes
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            AddRecipeViewModel model = new AddRecipeViewModel();
            model.Categories = _categoryService.GetAllCategories();
            model.AvailableIngredients = _ingredientService.GetAllIngredients();

            model.UnitsOfMeasurement = Enum.GetValues(typeof(UnitOfMeasurement))
                .Cast<UnitOfMeasurement>()
                .Select(u => new SelectListItem
                {
                    Text = u.ToString(),
                    Value = ((int)u).ToString()
                })
                .ToList();

            //TODO: if user is not log in it should send him to login page and only loged in users can see add recipe
            model.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload necessary data in case of validation errors
                model.Categories = _categoryService.GetAllCategories();

                model.AvailableIngredients = _ingredientService.GetAllIngredients();

                model.UnitsOfMeasurement = Enum.GetValues(typeof(UnitOfMeasurement))
                    .Cast<UnitOfMeasurement>()
                    .Select(u => new SelectListItem
                    {
                        Text = u.ToString(),
                        Value = ((int)u).ToString()
                    })
                    .ToList();

                return View(model);
            }

            // Map the AddRecipeViewModel to Recipe entity
            var recipe = new Recipe
            {
                Title = model.Title,
                Description = model.Description,
                Instructions = model.Instructions,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                UserId = model.UserId
            };

            _recipeService.AddRecipe(recipe);

            recipe.RecipeIngredients = model.Ingredients
                .Select(i => new RecipeIngredient
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    RecipeId = recipe.Id
                }).ToList();

            _recipeService.UpdateRecipe(recipe);

            return RedirectToAction(nameof(Index));
        }
    }
}
