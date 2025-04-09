using MockQueryable;
using Moq;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data;
using RecipeApp.Services.Data.Interfaces;
using RecipeApp.Web.ViewModels.RecipeViewModels;

namespace RecipeApp.Services.Tests
{
    [TestFixture]
    public class RecipeServiceTests
    {
        private Mock<IRepository<Recipe, int>> recipeRepository;
        private Mock<IRepository<Ingredient, int>> ingredientRepository;
        private Mock<IRepository<Comment, int>> commentRepository;
        private Mock<IRepository<Rating, int>> ratingRepository;
        private Mock<IRepository<Cookbook, int>> cookbookRepository;
        private Mock<IRepository<Category, int>> categoryRepository;
        private Mock<IRepository<RecipeIngredient, object>> recipeIngredientRepository;
        private Mock<IIngredientService> ingredientService;
        private Mock<ICategoryService> categoryService;
        private Mock<IFavoriteService> favoriteService;
        private Mock<IRatingService> ratingService;
        private Mock<ICommentService> commentService;

        private RecipeService recipeService;

        [SetUp]
        public void Setup()
        {
            this.recipeRepository = new Mock<IRepository<Recipe, int>>();
            this.ingredientRepository = new Mock<IRepository<Ingredient, int>>();
            this.commentRepository = new Mock<IRepository<Comment, int>>();
            this.ratingRepository = new Mock<IRepository<Rating, int>>();
            this.cookbookRepository = new Mock<IRepository<Cookbook, int>>();
            this.categoryRepository = new Mock<IRepository<Category, int>>();
            this.recipeIngredientRepository = new Mock<IRepository<RecipeIngredient, object>>();
            this.ingredientService = new Mock<IIngredientService>();
            this.categoryService = new Mock<ICategoryService>();
            this.favoriteService = new Mock<IFavoriteService>();
            this.ratingService = new Mock<IRatingService>();
            this.commentService = new Mock<ICommentService>();

            this.recipeService = new RecipeService(
                recipeRepository.Object,
                ingredientRepository.Object,
                commentRepository.Object,
                ratingRepository.Object,
                cookbookRepository.Object,
                categoryRepository.Object,
                recipeIngredientRepository.Object,
                ingredientService.Object,
                categoryService.Object,
                favoriteService.Object,
                ratingService.Object,
                commentService.Object
            );
        }

        [Test]
        public async Task AddRecipeAsync_ShouldCallAddOnRepositories()
        {
            // Arrange
            var model = new AddRecipeViewModel
            {
                Title = "Test Recipe",
                Description = "Test Description",
                Instructions = "Test Instructions",
                ImageUrl = "TestImageUrl",
                CategoryId = 1,
                UserId = "TestUserId",
                Ingredients = new List<IngredientViewModel>
        {
            new IngredientViewModel { IngredientId = 1, Quantity = 1, Unit = UnitOfMeasurement.Gram }
        }
            };

            // Act
            await recipeService.AddRecipeAsync(model);

            // Assert: check Recipe is added with matching fields
            recipeRepository.Verify(r => r.AddAsync(It.Is<Recipe>(r =>
                r.Title == model.Title &&
                r.Description == model.Description &&
                r.Instructions == model.Instructions &&
                r.ImageUrl == model.ImageUrl &&
                r.CategoryId == model.CategoryId &&
                r.UserId == model.UserId
            )), Times.Once);

            // Assert: at least one ingredient added
            recipeIngredientRepository.Verify(ri => ri.AddAsync(It.IsAny<RecipeIngredient>()), Times.Once);
        }

        [Test]
        public async Task GetRecipes_ShouldReturnNonDeletedRecipesOrderedByTitle()
        {
            // Arrange
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Title = "Banana Bread", IsDeleted = false },
                new Recipe { Id = 2, Title = "Apple Pie", IsDeleted = false },
                new Recipe { Id = 3, Title = "Deleted Recipe", IsDeleted = true }
            };

            var recipesMock = recipes.AsQueryable().BuildMock(); // Convert to mock IQueryable
            recipeRepository.Setup(r => r.GetAllAttached()).Returns(recipesMock);

            // Act
            IEnumerable<Recipe>? result = await recipeService.GetRecipesAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Title, Is.EqualTo("Apple Pie"));
        }

        [Test]
        public async Task GetRecipeByIdAsync_ShouldReturnCorrectRecipe()
        {// Arrange
            var recipes = new List<Recipe>
             {
                 new Recipe { Id = 1, Title = "Test Recipe", IsDeleted = false }
             }.AsQueryable().BuildMock();

            recipeRepository.Setup(r => r.GetAllAttached()).Returns(recipes);

            // Act
            Recipe? result = await recipeService.GetRecipeByIdAsync(1);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateRecipeAsync_ShouldUpdateRecipeAndReplaceIngredients()
        {
            // Arrange
            var recipe = new Recipe { Id = 1, Title = "Updated Recipe" };

            var existingIngredients = new List<RecipeIngredient>
             {
                 new RecipeIngredient { RecipeId = 1, IngredientId = 1 },
                 new RecipeIngredient { RecipeId = 1, IngredientId = 2 }
             }.AsQueryable().BuildMock();

            var updatedIngredients = new List<RecipeIngredient>
              {
                  new RecipeIngredient { IngredientId = 3 },
                  new RecipeIngredient { IngredientId = 4 }
              };

            recipeRepository.Setup(r => r.UpdateAsync(recipe)).Returns(Task.FromResult(true)); // Update recipe details
            recipeIngredientRepository.Setup(ri => ri.GetAllAttached())
                .Returns(existingIngredients); // Old ingredients
                                               //removing the old ingredients
            recipeIngredientRepository.Setup(ri => ri.DeleteAsync(It.IsAny<object[]>())).Returns(Task.FromResult(true));
            // add new ingredients
            recipeIngredientRepository.Setup(ri => ri.AddAsync(It.IsAny<RecipeIngredient>())).Returns(Task.CompletedTask);

            // Act
            await recipeService.UpdateRecipeAsync(recipe, updatedIngredients);

            // Assert
            recipeRepository.Verify(r => r.UpdateAsync(recipe), Times.Once);
            recipeIngredientRepository.Verify(ri => ri.DeleteAsync(It.IsAny<object[]>()), Times.Exactly(2)); // Deletes old ingredients
            recipeIngredientRepository.Verify(ri => ri.AddAsync(It.Is<RecipeIngredient>(ri => updatedIngredients.Contains(ri))), Times.Exactly(2)); // Adds new ingredients
        }

        [Test]
        public async Task DeleteRecipeAsync_ShouldMarkRecipeAsDeleted()
        {
            // Arrange
            var recipe = new Recipe { Id = 1, IsDeleted = false };
            recipeRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(recipe);

            // Act
            bool result = await recipeService.DeleteRecipeAsync(1);

            // Assert
            Assert.That(result, Is.True);
            Assert.That(recipe.IsDeleted, Is.True);
            recipeRepository.Verify(r => r.UpdateAsync(recipe), Times.Once);
        }

        [Test]
        public async Task SearchRecipesAsync_ShouldReturnMatchingRecipes()
        {
            int pageNumber = 1;
            int pageSize = 9;
            string userId = "TestUserId";

            // Arrange
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Title = "Apple Pie", Description = "Tasty dessert", IsDeleted = false },
                new Recipe { Id = 2, Title = "Banana Bread", Description = "Delicious bread", IsDeleted = false },
                new Recipe { Id = 3, Title = "Cherry Tart", Description = "Fruit dessert", IsDeleted = false },
                new Recipe { Id = 4, Title = "Coffee", Description = "start for the day", IsDeleted = true }
            }.AsQueryable().BuildMock();

            recipeRepository.Setup(r => r.GetAllAttached()).Returns(recipes);

            // Mock favoriteService to prevent null reference
            favoriteService.Setup(fs => fs.GetUserCookbooksAsync(userId))
                .ReturnsAsync(new List<Cookbook>());

            favoriteService.Setup(fs => fs.GetAllFavoriteRecipesIds(userId))
                .ReturnsAsync(new List<int> { 1 }); // Only recipe with Id=1 is liked

            // Act
            var (result, totalPages) = await recipeService.SearchRecipesAsync("apple", userId, pageNumber, pageSize);
            var (resultMore, totalPagesMore) = await recipeService.SearchRecipesAsync("a", userId, pageNumber, pageSize);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(1));
                Assert.That(result.First().Id, Is.EqualTo(1));
                Assert.That(result.First().Liked, Is.True);
            });

            Assert.That(resultMore.Count(), Is.EqualTo(3)); // Recipes 1, 2, 3
        }

    }
}
