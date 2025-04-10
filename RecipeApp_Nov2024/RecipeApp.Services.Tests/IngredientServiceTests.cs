using Moq;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data;

namespace RecipeApp.Services.Tests
{
    [TestFixture]
    public class IngredientServiceTests
    {
        private Mock<IRepository<Ingredient, int>> ingredientRepository;
        private IngredientService ingredientService;

        [SetUp]
        public void Setup()
        {
            ingredientRepository = new Mock<IRepository<Ingredient, int>>();
            ingredientService = new IngredientService(ingredientRepository.Object);
        }

        [Test]
        public async Task GetAllIngredientsAsync_ShouldReturnAllIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Sugar" },
                new Ingredient { Id = 2, Name = "Flour" }
            };

            ingredientRepository
                .Setup(i => i.GetAllAsync())
                .ReturnsAsync(ingredients);

            // Act
            var result = await ingredientService.GetAllIngredientsAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("Sugar"));
        }

        [Test]
        public async Task GetAllIngredientsAsync_WhenNoIngredientsExist_ShouldReturnEmptyList()
        {
            // Arrange
            ingredientRepository
                .Setup(i => i.GetAllAsync())
                .ReturnsAsync(new List<Ingredient>());

            // Act
            var result = await ingredientService.GetAllIngredientsAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }
    }
}
