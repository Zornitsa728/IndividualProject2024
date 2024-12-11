using MockQueryable;
using Moq;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data;

namespace RecipeApp.Services.Tests
{
    [TestFixture]
    public class RatingServiceTests
    {
        private Mock<IRepository<Rating, int>> ratingRepository;
        private RatingService ratingService;

        [SetUp]
        public void Setup()
        {
            ratingRepository = new Mock<IRepository<Rating, int>>();
            ratingService = new RatingService(ratingRepository.Object);
        }

        [Test]
        public async Task GetRatingsAsync_ShouldReturnRatingsForRecipe()
        {
            // Arrange
            var ratings = new List<Rating>
            {
                new Rating { Id = 1, RecipeId = 1, Score = 5 },
                new Rating { Id = 2, RecipeId = 1, Score = 4 },
                new Rating { Id = 3, RecipeId = 2, Score = 3 }
            }.AsQueryable()
            .BuildMock();

            ratingRepository.Setup(r => r.GetAllAttached()).Returns(ratings);

            // Act
            IEnumerable<Rating>? result = await ratingService.GetRatingsAsync(1);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task AddRatingAsync_ShouldAddRating()
        {
            // Arrange
            var recipeId = 1;
            var score = 5;
            var userId = "123";

            // Act
            Rating? result = await ratingService.AddRatingAsync(recipeId, score, userId);

            // Assert
            ratingRepository.Verify(r => r.AddAsync(It.IsAny<Rating>()), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.That(result.Score, Is.EqualTo(score));
                Assert.That(result.UserId, Is.EqualTo(userId));
            });
        }

        [Test]
        public async Task GetAverageRatingAsync_ShouldReturnCorrectAverage()
        {
            // Arrange
            var ratings = new List<Rating>
            {
                new Rating { RecipeId = 1, Score = 5 },
                new Rating { RecipeId = 1, Score = 3 }
            }.AsQueryable()
            .BuildMock();

            ratingRepository.Setup(r => r.GetAllAttached()).Returns(ratings);

            // Act
            var result = await ratingService.GetAverageRatingAsync(1);

            // Assert
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void CheckRecipeUserRating_ShouldReturnFalseIfUserHasRated()
        {
            // Arrange
            var ratings = new List<Rating>
            {
                new Rating { RecipeId = 1, UserId = "123" }
            }.AsQueryable()
            .BuildMock();

            ratingRepository.Setup(r => r.GetAllAttached()).Returns(ratings);

            // Act
            bool result = ratingService.CheckRecipeUserRating(1, "123");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateRatingAsync_ShouldUpdateExistingRating()
        {
            // Arrange
            var recipeId = 1;
            var score = 4;
            var userId = "123";

            var ratings = new List<Rating>
            {
                new Rating
                {
                    RecipeId = recipeId,
                    UserId = userId,
                    Score = 5
                }
            }.AsQueryable()
            .BuildMock();

            ratingRepository.Setup(r => r.GetAllAttached()).Returns(ratings);

            // Act
            Rating? result = await ratingService.UpdateRatingAsync(recipeId, score, userId);

            // Assert
            Assert.That(result.Score, Is.EqualTo(score));
            ratingRepository.Verify(r => r.UpdateAsync(It.IsAny<Rating>()), Times.Once);
        }
    }
}
