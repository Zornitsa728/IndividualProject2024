using MockQueryable;
using Moq;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data;
using RecipeApp.Services.Data.Interfaces;

namespace RecipeApp.Services.Tests
{
    [TestFixture]
    public class CategoryServiceTests
    {
        private Mock<IRepository<Category, int>> categoryRepository;
        private CategoryService categoryService;
        private Mock<IFavoriteService> favoriteService;

        [SetUp]
        public void Setup()
        {
            categoryRepository = new Mock<IRepository<Category, int>>();
            this.favoriteService = new Mock<IFavoriteService>();

            this.categoryService = new CategoryService(
                categoryRepository.Object,
                favoriteService.Object
                );
        }

        [Test]
        public async Task GetAllCategoriesAsync_ShouldReturnAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Desserts" },
                new Category { Id = 2, Name = "Main Dishes" }
            }.AsQueryable().BuildMock();

            categoryRepository.Setup(c => c.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await categoryService.GetAllCategoriesAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetCategoryAsync_ShouldReturnCategoryById()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Desserts" };

            categoryRepository.Setup(c => c.GetByIdAsync(1)).ReturnsAsync(category);

            // Act
            var result = await categoryService.GetCategoryAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(1));
        }
    }
}
