using MockQueryable;
using Moq;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data;
using RecipeApp.Web.ViewModels.FavoritesViewModels;

namespace RecipeApp.Services.Tests
{
    [TestFixture]
    public class FavoriteServiceTests
    {
        private Mock<IRepository<Cookbook, int>> cookbookRepository;
        private Mock<IRepository<RecipeCookbook, object>> recipeCookbookRepository;
        private FavoriteService favoriteService;

        [SetUp]
        public void SetUp()
        {
            this.cookbookRepository = new Mock<IRepository<Cookbook, int>>();
            this.recipeCookbookRepository = new Mock<IRepository<RecipeCookbook, object>>();

            this.favoriteService = new FavoriteService(
                cookbookRepository.Object,
                recipeCookbookRepository.Object
            );
        }

        [Test]
        public async Task CreateCookbookAsync_ShouldCallAddOnRepository()
        {
            // Arrange
            var model = new CookbookCreateViewModel { Title = "My Cookbook", Description = "A great cookbook" };
            string userId = "user123";

            // Act
            await favoriteService.CreateCookbookAsync(model, userId);

            // Assert
            cookbookRepository.Verify(r => r.AddAsync(It.Is<Cookbook>(c =>
                c.Title == model.Title &&
                c.Description == model.Description &&
                c.UserId == userId)), Times.Once);
        }

        [Test]
        public async Task GetUserCookbooksAsync_ShouldReturnUserCookbooks()
        {
            // Arrange
            var cookbooks = new List<Cookbook>
            {
                new Cookbook { Id = 1, UserId = "user123" },
                new Cookbook { Id = 2, UserId = "user123" },
                new Cookbook { Id = 3, UserId = "user456" }
            }.AsQueryable().BuildMock();

            cookbookRepository.Setup(r => r.GetAllAttached()).Returns(cookbooks);

            // Act
            List<Cookbook>? result = await favoriteService.GetUserCookbooksAsync("user123");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.All(c => c.UserId == "user123"), Is.True);
        }

        [Test]
        public async Task AddRecipeToCookbookAsync_ShouldAddRecipeIfNotExists()
        {
            // Arrange
            int cookbookId = 1, recipeId = 1;
            var cookbooks = new List<Cookbook>
            {
                new Cookbook { Id = 1, RecipeCookbooks = new List<RecipeCookbook>() }
            }.AsQueryable().BuildMock();

            cookbookRepository.Setup(r => r.GetAllAttached()).Returns(cookbooks);

            // Act
            await favoriteService.AddRecipeToCookbookAsync(cookbookId, recipeId);

            // Assert
            recipeCookbookRepository.Verify(r => r.AddAsync(It.Is<RecipeCookbook>(rc =>
                rc.CookbookId == cookbookId && rc.RecipeId == recipeId)), Times.Once);
        }

        [Test]
        public async Task RemoveRecipeFromCookbookAsync_ShouldCallDeleteIfExists()
        {
            // Arrange
            int cookbookId = 1, recipeId = 1;
            var recipeCookbook = new RecipeCookbook { CookbookId = cookbookId, RecipeId = recipeId };

            recipeCookbookRepository.Setup(r => r.GetByIdAsync(new object[] { recipeId, cookbookId }))
                .ReturnsAsync(recipeCookbook);

            // Act
            await favoriteService.RemoveRecipeFromCookbookAsync(cookbookId, recipeId);

            // Assert
            recipeCookbookRepository.Verify(r => r.DeleteAsync(new object[] { recipeId, cookbookId }), Times.Once);
        }

        [Test]
        public async Task GetCookbookWithRecipesAsync_ShouldReturnCookbookWithRecipes()
        {
            // Arrange
            int cookbookId = 1;
            var cookbook = new Cookbook
            {
                Id = cookbookId,
                RecipeCookbooks = new List<RecipeCookbook> { new RecipeCookbook { RecipeId = 1 } }
            };

            var cookbooks = new List<Cookbook> { cookbook }.AsQueryable().BuildMock();

            cookbookRepository.Setup(r => r.GetAllAttached()).Returns(cookbooks);

            // Act
            Cookbook? result = await favoriteService.GetCookbookWithRecipesAsync(cookbookId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(cookbookId));
            Assert.That(result.RecipeCookbooks.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveCookbookAsync_ShouldDeleteCookbookAndRecipes()
        {
            // Arrange
            int cookbookId = 1;
            var cookbook = new Cookbook
            {
                Id = cookbookId,
                RecipeCookbooks = new List<RecipeCookbook>
                {
                    new RecipeCookbook { CookbookId = cookbookId, RecipeId = 1 },
                    new RecipeCookbook { CookbookId = cookbookId, RecipeId = 2 }
                }
            };

            var cookbooks = new List<Cookbook> { cookbook }.AsQueryable().BuildMock();

            cookbookRepository.Setup(r => r.GetAllAttached()).Returns(cookbooks);

            // Act
            bool result = await favoriteService.RemoveCookbookAsync(cookbookId);

            // Assert
            Assert.That(result, Is.True);
            recipeCookbookRepository.Verify(r => r.DeleteAsync(It.IsAny<object[]>()), Times.Exactly(2));
            cookbookRepository.Verify(r => r.DeleteAsync(cookbookId), Times.Once);
        }
    }
}
