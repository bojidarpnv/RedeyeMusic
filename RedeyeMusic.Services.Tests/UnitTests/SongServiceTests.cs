using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.Services.Tests.UnitTests
{
    public class SongServiceTests : UnitTestsBase
    {
        private ISongService songService;
        [OneTimeSetUp]
        public void SetUp()
        {
            this.songService = new SongService(dbContext);
        }

        [Test]
        public async Task GetDetailsByIdAsync_ShouldReturnCorrectDetails()
        {
            var songId = SeededSong.Id;

            var result = await this.songService.GetDetailsByIdAsync(songId);
            var songInDb = dbContext.Songs.FirstOrDefault(s => s.Id == songId);
            Assert.IsNotNull(songInDb);
            Assert.That(result.Title, Is.EqualTo(songInDb.Title));
        }
        [Test]
        public async Task ExistsById_ShouldReturnTrue()
        {
            var songId = SeededSong.Id;
            var result = await this.songService.ExistsById(songId);
            Assert.IsTrue(result);
        }
        [Test]
        public async Task ExistsById_ShouldReturnFalse()
        {
            var songId = 100;
            var result = await this.songService.ExistsById(songId);
            Assert.IsFalse(result);
        }
        [Test]
        public async Task GetSongForEditByIdAsync_ShouldReturnCorrectData()
        {
            var songId = SeededSong.Id;
            var expectedData = new EditSongFormModel()
            {

                Title = SeededSong.Title,
                Lyrics = SeededSong.Lyrics,
                ImageUrl = SeededSong.ImageUrl,
            };

            var resultData = await this.songService.GetSongForEditByIdAsync(songId);
            Assert.That(expectedData.Title, Is.EqualTo(resultData.Title));
            Assert.That(expectedData.Lyrics, Is.EqualTo(resultData.Lyrics));
            Assert.That(expectedData.ImageUrl, Is.EqualTo(resultData.ImageUrl));
        }
        [Test]
        public async Task EditSongByIdAndModel_ShouldEditSongCorrectly()
        {
            Song song = new Song()
            {
                Title = "TestSong",
                Lyrics = "Test",
                ArtistId = SeededSong.ArtistId,
                Duration = SeededSong.Duration,
                ImageUrl = SeededSong.ImageUrl,
                Genre = SeededSong.Genre,
                ListenCount = SeededSong.ListenCount,
                Mp3FilePath = SeededSong.Mp3FilePath,
            };

            dbContext.Songs.Add(song);
            dbContext.SaveChanges();
            int songId = song.Id;
            var songFormModel = new EditSongFormModel()
            {
                Title = song.Title,
                Lyrics = song.Lyrics,
            };
            var changedTitle = "ChangedTitle";
            songFormModel.Title = changedTitle;
            await this.songService.EditSongByIdAndModel(songId, songFormModel);

            var newSongInDb = dbContext.Songs.FirstOrDefault(s => s.Id == songId);
            Assert.IsNotNull(newSongInDb);
            Assert.That(newSongInDb.Lyrics, Is.EqualTo(song.Lyrics));
            Assert.That(newSongInDb.Title, Is.EqualTo(changedTitle));
        }
        [Test]
        public async Task GetListenCount_ShouldReturnCorrectListenCount()
        {
            var songId = SeededSong.Id;
            var expectedListenCount = SeededSong.ListenCount;
            var result = await this.songService.GetListenCountAsync(songId);
            Assert.That(expectedListenCount, Is.EqualTo(result.ListenCount));
        }
        [Test]
        public async Task DeleteSongByIdAsync_ShouldSetPropertyToTrue()
        {
            var songId = SeededSong2.Id;
            await this.songService.DeleteSongByIdAsync(songId);
            var songInDb = dbContext.Songs.FirstOrDefault(s => s.Id == songId);
            Assert.IsTrue(songInDb.IsDeleted);
        }
    }
}
