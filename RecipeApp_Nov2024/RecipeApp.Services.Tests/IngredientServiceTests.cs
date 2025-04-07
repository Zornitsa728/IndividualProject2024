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
        public void GetAllIngredients_ShouldReturnAllIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Sugar" },
                new Ingredient { Id = 2, Name = "Flour" }
            };

            ingredientRepository.Setup(i => i.GetAll()).Returns(ingredients.AsQueryable());

            // Act
            var result = ingredientService.GetAllIngredientsAsync().Result;

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("Sugar"));
        }
    }
}
