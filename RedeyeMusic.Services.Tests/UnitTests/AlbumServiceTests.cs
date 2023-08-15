using RedeyeMusic.Services.Data;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Tests.UnitTests
{
    public class AlbumServiceTests : UnitTestsBase
    {
        private IAlbumService albumService;
        [OneTimeSetUp]
        public void SetUp()
            => this.albumService = new AlbumService(dbContext);



        [Test]
        public async Task GetAlbumIdShouldReturnCorrectId()
        {
            AddSongFormModel songFormModel = new AddSongFormModel()
            {
                AlbumName = SeededAlbum.Name,
                AlbumDescription = SeededAlbum.Description,
            };
            int albumId = await this.albumService.GetAlbumId(songFormModel);
            Assert.That(albumId, Is.EqualTo(SeededAlbum.Id));
        }
        [Test]
        public async Task ExistsByIdShouldReturnTrue()
        {
            bool result = await this.albumService.ExistsById(SeededAlbum.Id);
            Assert.That(result, Is.True);
        }
        [Test]
        public async Task ExistsByIdShouldReturnFalse()
        {
            bool result = await this.albumService.ExistsById(2);
            Assert.That(result, Is.False);
        }
        [Test]
        public async Task GetDetailsByIdAsyncShouldReturnCorrectDietails()
        {
            var albumId = SeededAlbum.Id;
            var result = await this.albumService.GetDetailsByIdAsync(SeededAlbum.Id);

            Assert.IsNotNull(result);

            var albumInDb = this.dbContext.Albums.Find(albumId);
            Assert.That(result.Id, Is.EqualTo(albumInDb.Id));
            Assert.That(result.Name, Is.EqualTo(albumInDb.Name));
        }
    }
}
