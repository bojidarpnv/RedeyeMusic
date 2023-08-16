using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Album;
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
            bool result = await this.albumService.ExistsById(100);
            Assert.That(result, Is.False);
        }
        [Test]
        public async Task GetDetailsByIdAsyncShouldReturnCorrectDetails()
        {
            var albumId = SeededAlbum.Id;
            var result = await this.albumService.GetDetailsByIdAsync(SeededAlbum.Id);

            Assert.IsNotNull(result);

            var albumInDb = this.dbContext.Albums.Find(albumId);
            Assert.That(result.Id, Is.EqualTo(albumInDb.Id));
            Assert.That(result.Name, Is.EqualTo(albumInDb.Name));
        }
        [Test]
        public async Task GetAlbumDescriptionAndNameAndUrlByIdShouldReturnCorrectData()
        {
            var albumId = SeededAlbum.Id;
            var expectedSongModel = new AddSongFormModel()
            {
                AlbumDescription = SeededAlbum.Description,
                AlbumName = SeededAlbum.Name,
                AlbumImageUrl = SeededAlbum.ImageUrl,
            };
            var actualSongModel = new AddSongFormModel();
            actualSongModel = await this.albumService.GetAlbumDescriptionAndNameAndUrlById(albumId, actualSongModel);
            Assert.That(actualSongModel.AlbumDescription, Is.EqualTo(expectedSongModel.AlbumDescription));
            Assert.That(actualSongModel.AlbumName, Is.EqualTo(expectedSongModel.AlbumName));
            Assert.That(actualSongModel.AlbumImageUrl, Is.EqualTo(expectedSongModel.AlbumImageUrl));
        }
        [Test]
        public async Task GetAlbumForEditAsyncShouldReturnTheCorrectDetailsForTheAlbum()
        {
            var albumid = SeededAlbum.Id;
            var expectedData = new EditAlbumFormModel()
            {
                Id = SeededAlbum.Id,
                Name = SeededAlbum.Name,
                Description = SeededAlbum.Description,
            };

            EditAlbumFormModel resultData = await this.albumService.GetAlbumForEditAsync(albumid);
            Assert.That(expectedData.Id, Is.EqualTo(resultData.Id));
            Assert.That(expectedData.Name, Is.EqualTo(resultData.Name));
            Assert.That(expectedData.Description, Is.EqualTo(resultData.Description));
        }
        [Test]
        public async Task AddAlbum_ShouldAddTheAlbumToTheDb()
        {
            var albumsInDbBefore = dbContext.Albums.Count();
            var artistId = SeededAlbum.ArtistId;
            var albumFormModel = new AlbumFormModel()
            {
                Name = "TestingUnitTests",
                Description = "Testing",
                ImageUrl = SeededAlbum.ImageUrl,
            };
            int newAlbumid = await this.albumService.AddAlbum(albumFormModel, artistId);
            var albumsInDbAfter = dbContext.Albums.Count();
            Assert.That(albumsInDbAfter, Is.EqualTo(albumsInDbBefore + 1));
            var newAlbumInDb = dbContext.Albums.Find(newAlbumid);
            Assert.That(newAlbumInDb.ImageUrl, Is.EqualTo(albumFormModel.ImageUrl));

        }
        [Test]
        public async Task DeleteAlbumByIdAsync_ShouldDeleteAlbumSuccessfully()
        {
            var album = new Album()
            {
                Id = 5,
                Name = "Delete",
                Description = "Delete",
                ImageUrl = SeededAlbum.ImageUrl,
                IsDeleted = false,
                Songs = new List<Song>()
            };
            album.Songs.Add(SeededSong2);
            await this.dbContext.Albums.AddAsync(album);
            await this.dbContext.SaveChangesAsync();



            await this.albumService.DeleteAlbumByIdAsync(album.Id);
            Assert.That(album.IsDeleted, Is.EqualTo(true));
            var songInAlbum = album.Songs.First(s => s.Id == 2);
            Assert.That(songInAlbum.IsDeleted, Is.EqualTo(true));
        }
        [Test]
        public async Task UpdateAlbumAsync_ShouldEditAlbumCorrectly()
        {
            Album album = new Album()
            {

                Name = "TestAlbum",
                Description = "AlbumDescription",
                ImageUrl = SeededAlbum.ImageUrl,
                IsDeleted = false,
                Songs = new List<Song>()
            };
            album.Songs.Add(SeededSong);
            dbContext.Albums.Add(album);
            dbContext.SaveChanges();
            int albumId = album.Id;
            var albumFormModel = new EditAlbumFormModel()
            {
                Id = albumId,
                Name = album.Name,
                Description = album.Description,
                ImageUrl = SeededAlbum.ImageUrl,
                SelectedSongIds = { 1 },
            };
            var changedDescription = "ChangedDescription";
            albumFormModel.Description = changedDescription;
            await this.albumService.UpdateAlbumAsync(albumFormModel);

            var newAlbumInDb = dbContext.Albums.FirstOrDefault(a => a.Id == albumId);
            Assert.IsNotNull(newAlbumInDb);
            Assert.That(newAlbumInDb.Name, Is.EqualTo(album.Name));
            Assert.That(newAlbumInDb.Description, Is.EqualTo(changedDescription));
        }

    }
}
