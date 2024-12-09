using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeApp.Data.Models;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.AdminViewModels.RecipeManagementViewModels;
using RecipeApp.Web.ViewModels.CommentViewModels;
using RecipeApp.Web.ViewModels.RatingViewModels;
using RecipeApp.Web.ViewModels.RecipeViewModels;
using System.Security.Claims;

namespace RecipeApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RecipeManagementController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly ICommentService commentService;

        public RecipeManagementController(IRecipeService recipeService, ICommentService commentService)
        {
            this.recipeService = recipeService;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await this.recipeService.GetAllRecipesAsync();

            IEnumerable<RecipeViewModel> model = recipes.Select(r => new RecipeViewModel()
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                Instructions = r.Instructions,
                CategoryId = r.CategoryId,
                CategoryName = r.Category.Name,
                ImageUrl = r.ImageUrl,
            })
                .ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewRecipe(int id)
        {
            var recipe = await recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            var averageRating = await recipeService.GetAverageRatingAsync(id);
            var comments = await recipeService.GetCommentsAsync(id);

            RecipeCommentsViewModel recipeCommentsViewModel = new RecipeCommentsViewModel()
            {
                RecipeId = id,
                Comments = comments
                .Select(c => new CommentViewModel()
                {
                    CommentId = c.Id,
                    Content = c.Content,
                    UserId = c.UserId,
                    UserName = c.User.UserName,
                    RecipeId = c.RecipeId,
                    DatePosted = c.DatePosted
                })
                .ToList()
            };

            RatingViewModel ratingModel = new RatingViewModel()
            {
                AverageRating = averageRating,
                RecipeId = id
            };

            var recipeModel = new RecipeDetailsViewModel
            {
                Recipe = recipe,
                Comments = recipeCommentsViewModel,
                Rating = ratingModel
            };

            return View(recipeModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var recipe = await recipeService.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            if (userId == null)
            {
                return NotFound();
            }

            var model = new EditRecipeViewModel
            {
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                ImageUrl = recipe.ImageUrl,
                UserId = recipe.UserId,
                CategoryId = recipe.CategoryId,
                Categories = await recipeService.GetAllCategoriesAsync(),
                AvailableIngredients = await recipeService.GetAllIngredientsAsync(),
                UnitsOfMeasurement = Enum.GetValues(typeof(UnitOfMeasurement))
                    .Cast<UnitOfMeasurement>()
                    .Select(u => new SelectListItem
                    {
                        Text = u.ToString(),
                        Value = ((int)u).ToString()
                    })
                    .ToList(),
                Ingredients = recipe.RecipeIngredients
                    .Select(ri => new IngredientViewModel()
                    {
                        IngredientId = ri.IngredientId,
                        Name = ri.Ingredient.Name,
                        Quantity = ri.Quantity,
                        Unit = ri.Unit
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await recipeService.GetAllCategoriesAsync();
                model.AvailableIngredients = await recipeService.GetAllIngredientsAsync();
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

            var recipe = await recipeService.GetRecipeByIdAsync(model.Id);

            if (recipe == null)
            {
                return NotFound();
            }

            // Update recipe details
            recipe.Title = model.Title;
            recipe.Description = model.Description;
            recipe.Instructions = model.Instructions;
            recipe.ImageUrl = model.ImageUrl;
            recipe.CategoryId = model.CategoryId;

            // Update ingredients if ingredients > 0
            var updatedIngredients = model.Ingredients.Select(i => new RecipeIngredient()
            {
                IngredientId = i.IngredientId,
                Quantity = i.Quantity,
                Unit = i.Unit,
                RecipeId = recipe.Id
            }).ToList();

            // Remove existing ingredients (if any) and add updated ones
            await recipeService.UpdateRecipeAsync(recipe, updatedIngredients);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await this.recipeService.GetRecipeByIdAsync(id);
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RecipeViewModel model)
        {
            await this.recipeService.DeleteRecipeAsync(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
