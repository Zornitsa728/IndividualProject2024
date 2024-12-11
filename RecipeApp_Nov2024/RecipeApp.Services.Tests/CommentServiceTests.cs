using MockQueryable;
using Moq;
using RecipeApp.Data.Models;
using RecipeApp.Data.Repository.Interfaces;
using RecipeApp.Services.Data;
using RecipeApp.Web.ViewModels.CommentViewModels;

namespace RecipeApp.Services.Tests
{
    [TestFixture]
    public class CommentServiceTests
    {
        private Mock<IRepository<Comment, int>> commentRepository;
        private CommentService commentService;

        [SetUp]
        public void Setup()
        {
            commentRepository = new Mock<IRepository<Comment, int>>();
            commentService = new CommentService(commentRepository.Object);
        }

        [Test]
        public async Task GetCommentsAsync_ShouldReturnCommentsForRecipe()
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
            IEnumerable<Comment>? result = await commentService.GetCommentsAsync(1);

            // Assert
            Assert.That(result.Any(), Is.True);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task AddCommentAsync_ShouldAddComment()
        {
            // Arrange
            var model = new CommentViewModel { RecipeId = 1, Content = "Great recipe!", DatePosted = System.DateTime.UtcNow };
            var userId = "user123";

            // Act
            Comment? result = await commentService.AddCommentAsync(model, userId);

            // Assert
            commentRepository.Verify(c => c.AddAsync(It.IsAny<Comment>()), Times.Once);
            Assert.That(result.Content, Is.EqualTo("Great recipe!"));
        }

        [Test]
        public async Task DeleteCommentAsync_ShouldMarkCommentAsDeleted()
        {
            // Arrange
            var comment = new Comment { Id = 1, RecipeId = 1, UserId = "user123", IsDeleted = false };
            commentRepository.Setup(c => c.GetByIdAsync(1)).ReturnsAsync(comment);

            // Act
            bool result = await commentService.DeleteCommentAsync(1, 1, "user123");

            // Assert
            commentRepository.Verify(c => c.UpdateAsync(comment), Times.Once);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True);
                Assert.That(comment.IsDeleted, Is.True);
            });
        }
    }
}
