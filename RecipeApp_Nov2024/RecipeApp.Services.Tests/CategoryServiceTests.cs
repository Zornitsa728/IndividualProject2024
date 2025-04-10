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

        [Test]
        public async Task GetCategoriesViewModelAsync_ShouldMapCorrectly()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Breakfast", ImageUrl = "url1" },
                new Category { Id = 2, Name = "Lunch", ImageUrl = "url2" }
            }.AsQueryable().BuildMock();

            categoryRepository.Setup(c => c.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await categoryService.GetCategoriesViewModelAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Breakfast"));
            Assert.That(result[1].ImageUrl, Is.EqualTo("url2"));
        }

        [Test]
        public async Task GetCurrPageRecipesForCategoryAsync_ShouldReturnCorrectRecipesWithoutUser()
        {
            // Arrange
            var categoryId = 1;
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Title = "Recipe A", CategoryId = 1, IsDeleted = false },
                new Recipe { Id = 2, Title = "Recipe B", CategoryId = 1, IsDeleted = false },
                new Recipe { Id = 3, Title = "Deleted Recipe", CategoryId = 1, IsDeleted = true },
                new Recipe { Id = 4, Title = "Wrong Category", CategoryId = 2, IsDeleted = false }
            };

            // Act
            var (currPage, totalPages) = await categoryService.GetCurrPageRecipesForCategoryAsync(
                recipes, categoryId, null, pageNumber: 1, pageSize: 2);

            // Assert
            Assert.That(currPage.Count, Is.EqualTo(2));
            Assert.That(totalPages, Is.EqualTo(1)); // Only 2 valid recipes
            Assert.That(currPage.All(r => r.Liked == false));
        }

        [Test]
        public async Task GetCurrPageRecipesForCategoryAsync_ShouldMarkFavorites_WhenUserIdIsProvided()
        {
            // Arrange
            var userId = "user123";
            var categoryId = 1;

            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Title = "Recipe A", CategoryId = 1, IsDeleted = false },
                new Recipe { Id = 2, Title = "Recipe B", CategoryId = 1, IsDeleted = false },
                new Recipe { Id = 3, Title = "Recipe C", CategoryId = 1, IsDeleted = false },
            };

            var userCookbooks = new List<Cookbook>
            {
            new Cookbook
            {
                RecipeCookbooks = new List<RecipeCookbook>
                {
                new RecipeCookbook { RecipeId = 1 },
                new RecipeCookbook { RecipeId = 3 }
                }
                }
            };

            favoriteService.Setup(f => f.GetUserCookbooksAsync(userId)).ReturnsAsync(userCookbooks);

            // Act
            var (currPage, totalPages) = await categoryService.GetCurrPageRecipesForCategoryAsync(
                recipes, categoryId, userId, pageNumber: 1, pageSize: 3);

            // Assert
            Assert.That(currPage.Count, Is.EqualTo(3));
            Assert.That(currPage.First(r => r.Id == 1).Liked, Is.True);
            Assert.That(currPage.First(r => r.Id == 2).Liked, Is.False);
            Assert.That(currPage.First(r => r.Id == 3).Liked, Is.True);
            Assert.That(totalPages, Is.EqualTo(1));
        }


        [Test]
        public async Task GetCurrPageRecipesForCategoryAsync_ShouldReturnCorrectPagination()
        {
            // Arrange
            var recipes = new List<Recipe>();

            for (int i = 1; i <= 10; i++)
            {
                recipes.Add(new Recipe { Id = i, Title = $"Recipe {i}", CategoryId = 1, IsDeleted = false });
            }

            // Act
            var (page2, totalPages) = await categoryService.GetCurrPageRecipesForCategoryAsync(
                recipes, categoryId: 1, userId: null, pageNumber: 2, pageSize: 3);

            // Assert
            Assert.That(totalPages, Is.EqualTo(4)); // 10 / 3 = 3.33 => 4 pages
            Assert.That(page2.Count, Is.EqualTo(3));
            Assert.That(page2.First().Title, Is.EqualTo("Recipe 4"));
        }

    }
}
