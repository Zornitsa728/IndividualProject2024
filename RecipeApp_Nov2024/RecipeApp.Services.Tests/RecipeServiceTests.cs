using MockQueryable;
using Moq;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data;

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

            this.recipeService = new RecipeService(
                    recipeRepository.Object,
                    ingredientRepository.Object,
                    commentRepository.Object,
                    ratingRepository.Object,
                    cookbookRepository.Object,
                    categoryRepository.Object,
                    recipeIngredientRepository.Object
                );
        }

        [Test]
        public async Task AddRecipeAsync_ShouldCallAddOnRepositories()
        {
            // Arrange
            var recipe = new Recipe { Id = 1, Title = "Test Recipe" };
            var ingredients = new List<RecipeIngredient> { new RecipeIngredient { IngredientId = 1 } };

            // Act
            await recipeService.AddRecipeAsync(recipe, ingredients);

            // Assert

            //storing the recipe
            recipeRepository.Verify(r => r.AddAsync(recipe), Times.Once);

            //When a recipe is added with its ingredients, the system should call the ingredient repository to store the relationship between the recipe and its ingredients.
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

            recipeRepository.Setup(r => r.GetAllAttached()).Returns(recipes.AsQueryable());

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
        public async Task GetUserCookbooksAsync_ShouldReturnCookbooksForUser()
        {
            // Arrange
            var userId = "123";
            var userCookbooks = new List<Cookbook>
            {
                new Cookbook { Id = 1, UserId = userId, Title = "My Cookbook 1" },
                new Cookbook { Id = 2, UserId = userId, Title = "My Cookbook 2" }
            }.AsQueryable().BuildMock();

            cookbookRepository.Setup(cr => cr.GetAllAttached())
                .Returns(userCookbooks);

            // Act
            IEnumerable<Cookbook>? result = await recipeService.GetUserCookbooksAsync(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.All(cb => cb.UserId == userId), Is.True);
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
        public async Task GetAverageRatingAsync_ShouldReturnCorrectAverage()
        {
            // Arrange
            int recipeId = 1;

            var ratings = new List<Rating>
            {
                new Rating { RecipeId = recipeId, Score = 5 },
                new Rating { RecipeId = recipeId, Score = 3 }
            }.AsQueryable().BuildMock();

            ratingRepository.Setup(r => r.GetAllAttached()).Returns(ratings);

            // Act
            double result = await recipeService.GetAverageRatingAsync(recipeId);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public async Task GetCommentsAsync_ShouldReturnAllCommentsForRecipe()
        {
            // Arrange
            var comments = new List<Comment>
            {
                new Comment { Id = 1, RecipeId = 1, IsDeleted = false },
                new Comment { Id = 2, RecipeId = 1, IsDeleted = false },
                new Comment { Id = 3, RecipeId = 2, IsDeleted = false }
            }.AsQueryable().BuildMock();

            commentRepository.Setup(c => c.GetAllAttached()).Returns(comments);

            // Act
            List<Comment>? result = await recipeService.GetCommentsAsync(1);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllIngredientsAsync_ShouldReturnAllIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1 },
                new Ingredient { Id = 2 }
            };

            ingredientRepository.Setup(i => i.GetAllAsync()).ReturnsAsync(ingredients);

            // Act
            IEnumerable<Ingredient>? result = await recipeService.GetAllIngredientsAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task SearchRecipesAsync_ShouldReturnMatchingRecipes()
        {
            // Arrange
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Title = "Apple Pie", Description = "Tasty dessert", IsDeleted = false },
                new Recipe { Id = 2, Title = "Banana Bread", Description = "Delicious bread", IsDeleted = false },
                new Recipe { Id = 3, Title = "Cherry Tart", Description = "Fruit dessert", IsDeleted = false },
                new Recipe { Id = 4, Title = "Coffee", Description = "start for the day", IsDeleted = true }
            }.AsQueryable().BuildMock();

            recipeRepository.Setup(r => r.GetAllAttached()).Returns(recipes);

            // Act
            var result = await recipeService.SearchRecipesAsync("apple", new List<int> { 1 });

            var resultMore = await recipeService.SearchRecipesAsync("a", new List<int> { 1 });
            //query and list with favorite recipes so the heart icon can be red

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(1));
                Assert.That(result.First().Id, Is.EqualTo(1));
                Assert.That(result.First().Liked, Is.True);
            });

            Assert.That(resultMore.Count(), Is.EqualTo(3));
        }
    }
}