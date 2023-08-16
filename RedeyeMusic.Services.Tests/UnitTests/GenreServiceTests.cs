using RedeyeMusic.Services.Data;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Genre;

namespace RedeyeMusic.Services.Tests.UnitTests
{
    public class GenreServiceTests : UnitTestsBase
    {
        private IGenreService genreService;
        [OneTimeSetUp]
        public void SetUp()
        {
            this.genreService = new GenreService(dbContext);
            dbContext.Database.EnsureCreated();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
        }
        [Test]
        public async Task CreateGenreAsync_ShouldProperlyCreateGenre()
        {
            //Arrange
            GenreFormModel formModel = new GenreFormModel()
            {
                Name = "name",
            };
            var countBefore = this.dbContext.Genres.Count();
            //Act
            await this.genreService.CreateGenreAsync(formModel);
            var countAfter = this.dbContext.Genres.Count();
            //Assert
            Assert.That(countAfter, Is.EqualTo(countBefore + 1));
        }
        [Test]
        public async Task SelectGenresAsync_ShouldReturnCorrectNumberOfGenres()
        {
            //Arrange
            var expectedCount = this.dbContext.Genres.Count();
            //Act
            var genres = await this.genreService.SelectGenresAsync();
            var actualCount = genres.Count();
            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
    }
}
