using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Web.ViewModels.Artist;
namespace RedeyeMusic.Services.Tests.UnitTests
{

    public class ArtistServiceTests : UnitTestsBase
    {

        private IArtistService artistService;


        [OneTimeSetUp]
        public void SetUp()
            => this.artistService = new ArtistService(dbContext);

        [Test]
        public async Task ArtistExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {

            string existingArtistId = ArtistUser.Id.ToString();

            bool result = await artistService.ArtistExistsByUserIdAsync(existingArtistId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task ArtistExistsByUserIdAsyncShouldReturnFalseWhenExists()
        {
            string guestUserId = GuestUser.Id.ToString();
            bool result = await artistService.ArtistExistsByUserIdAsync(guestUserId);

            Assert.IsFalse(result);
        }
        [Test]
        public async Task CreateArtistShouldAddToDbAndOtherMethodsShouldFindNewEntry()
        {
            int artistsCountBefore = dbContext.Artists.Count();
            BecomeArtistFormModel formModel = new BecomeArtistFormModel()
            {
                ArtistName = SeededArtist.Name,
            };
            await this.artistService.CreateAsync(SeededArtist.ApplicationUserId.ToString()!, formModel);
            var artistsCountAfter = dbContext.Artists.Count();
            Assert.That(artistsCountAfter, Is.EqualTo(artistsCountBefore + 1));

            int newArtistId = await this.artistService.GetArtistIdByUserIdAsync(SeededArtist.ApplicationUserId.ToString()!);
            bool newArtistNameExists = await this.artistService.ArtistNameExistsAsync(SeededArtist.Name);
            bool newArtistNameAlreadyExistsInDb = await this.artistService.ArtistNameAlreadyExists(SeededArtist.Name);
            string newArtistName = await this.artistService.GetArtistNameByUserIdAsync(SeededArtist.ApplicationUserId.ToString()!);
            Assert.That(newArtistId, Is.EqualTo(SeededArtist.Id));
            Assert.True(newArtistNameExists);
            Assert.True(newArtistNameAlreadyExistsInDb);
            Assert.That(newArtistName, Is.EqualTo(SeededArtist.Name));
        }

        [Test]
        public async Task ArtistNameExists_ShouldReturnFalseIfDoesNotExist()
        {
            bool artistNameExists = await this.artistService.ArtistNameExistsAsync("DoesNotExist");
            Assert.False(artistNameExists);
        }
        [Test]
        public async Task ArtistNameAlreadyExists_ShouldReturnFalseIfDoesNotExist()
        {
            bool artistNameExists = await this.artistService.ArtistNameExistsAsync("DoesNotExist");
            Assert.False(artistNameExists);
        }
        [Test]
        public async Task GetArtistNameByUserId_ShouldReturnEmptyStringIfDoesNotExist()
        {
            string randomGuid = Guid.NewGuid().ToString();
            string result = await this.artistService.GetArtistNameByUserIdAsync(randomGuid);
            Assert.IsEmpty(result);
        }
        [Test]
        public async Task IsArtistWithIdOwnerOfSongWithIdAsync_ShouldReturnTrueWithValidInformation()
        {
            bool result = await this.artistService.IsArtistWithIdOwnerOfSongWithIdAsync(SeededArtist.Id, SeededSong.Artist.Id);
            Assert.That(result, Is.EqualTo(true));

        }
        [Test]
        public async Task IsArtistWithIdOwnerOfSongWithIdAsync_ShouldReturnFalseWithInValidInformation()
        {
            int invalidId = 100;
            bool result = await this.artistService.IsArtistWithIdOwnerOfSongWithIdAsync(invalidId, SeededSong2.Id);
            Assert.That(result, Is.EqualTo(false));
            bool result2 = await this.artistService.IsArtistWithIdOwnerOfSongWithIdAsync(SeededArtist.Id, invalidId);
            Assert.That(result2, Is.EqualTo(false));
        }
        [Test]
        public async Task IsArtistWithIdOwnerOfAlbumWithIdAsync_ShouldReturnTrueWithValidInformation()
        {
            bool result = await this.artistService.IsArtistWithIdOwnerOfAlbumWithIdAsync(SeededArtist.Id, SeededAlbum.Id);
            Assert.True(result);

        }
        [Test]
        public async Task IsArtistWithIdOwnerOfAlbumWithIdAsync_ShouldReturnFalseWithInValidInformation()
        {
            int invalidId = 100;
            bool result = await this.artistService.IsArtistWithIdOwnerOfAlbumWithIdAsync(invalidId, SeededAlbum.Id);
            Assert.False(result);
            bool result2 = await this.artistService.IsArtistWithIdOwnerOfAlbumWithIdAsync(SeededArtist.Id, invalidId);
            Assert.False(result2);
        }
        [Test]
        public async Task DoesArtistHaveAnySongsAsync_ShouldReturnTrueIfThereAreSongsOfArtist()
        {
            dbContext.Songs.Add(SeededSong);
            dbContext.SaveChangesAsync();
            bool result = await this.artistService.DoesArtistHaveAnySongsAsync(SeededArtist.Id);
            Assert.True(result);
        }
        [Test]
        public async Task DoesArtistHaveAnySongsAsync_ShouldReturnFalseIfThereAreNoSongsOfArtist()
        {
            List<Song> songs = new List<Song>();
            songs.Add(SeededSong);
            songs.Add(SeededSong2);
            dbContext.Songs.RemoveRange(songs);
            dbContext.SaveChanges();
            bool result = await this.artistService.DoesArtistHaveAnySongsAsync(SeededArtist.Id);
            Assert.False(result);
        }

        [Test]
        public async Task GetArtistNameByIdAsync_ShouldReturnCorrectArtistName()
        {
            var artistId = SeededArtist.Id;

            var expectedResult = SeededArtist.Name;

            var result = await this.artistService.GetArtistNameByIdAsync(artistId);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }


}