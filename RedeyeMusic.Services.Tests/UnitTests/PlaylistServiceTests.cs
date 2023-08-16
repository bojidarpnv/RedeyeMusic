using RedeyeMusic.Services.Data;
using RedeyeMusic.Services.Data.Interfaces;

namespace RedeyeMusic.Services.Tests.UnitTests
{
    public class PlaylistServiceTests : UnitTestsBase
    {
        private IPlaylistService playlistService;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.playlistService = new PlaylistService(dbContext);
        }
        [Test]
        public async Task AddSongToPlaylistAsync_ShouldAddEntryToPlaylistsSongs()
        {
            //Arrange
            var songId = SeededSong2.Id;
            var playlistId = SeededPlaylist.Id;
            var playlistsSongsCountBefore = this.dbContext.PlaylistsSongs.Count();
            //Act
            await this.playlistService.AddSongToPlaylistAsync(songId, playlistId);
            var playlistsSongsCountAfter = this.dbContext.PlaylistsSongs.Count();
            //Assert
            Assert.That(playlistsSongsCountAfter, Is.EqualTo(playlistsSongsCountBefore + 1));
        }
        [Test]
        public async Task CreatePlaylistAsync_ShouldReturnTheNewlyAddedPlaylistId()
        {
            //Arrange
            var playlistName = "TestPlaylist";
            int songId = SeededSong2.Id;
            string userId = GuestUser.Id.ToString();
            int expectedResult = this.dbContext.Playlists.Count() + 1;
            //Act
            var result = await this.playlistService.CreatePlaylistAsync(playlistName, songId, userId);
            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        [Test]
        public async Task DeletePlaylistWithIdAsync_ShouldSetThePlaylistIsDeletedToTrue_AndRemovePlaylistsSongsEntry()
        {
            //Arrange
            var playlistId = SeededPlaylist.Id;
            var playlistsSongsCountBefore = this.dbContext.PlaylistsSongs.Count();
            var playlistIsDeletedBefore = SeededPlaylist.IsDeleted;
            //Act
            await this.playlistService.DeletePlaylistWithIdAsync(playlistId);
            var playlistsSongsCountAfter = this.dbContext.PlaylistsSongs.Count();
            var playlistIsDeletedAfter = SeededPlaylist.IsDeleted;
            //Assert
            Assert.That(playlistsSongsCountAfter, Is.EqualTo(playlistsSongsCountBefore - 1));
            Assert.That(playlistIsDeletedAfter, Is.EqualTo(!playlistIsDeletedBefore));
        }
        [Test]
        public async Task GetAllPlaylistsByUserIdAsync_ShouldReturnCorrectNumberOfPlaylists()
        {
            //Arrange
            var userId = GuestUser.Id.ToString();
            var expectedResult = this.dbContext.Playlists
                .Where(p => p.ApplicationUserId.ToString() == userId)
                .Count();
            //Act
            var playlists = await this.playlistService.GetAllPlaylistsByUserIdAsync(userId);
            var actualResult = playlists.Count();
            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        [Test]
        public async Task GetPlaylistByIdAsync_ShouldReturnCorrectDataForPlaylist()
        {
            // Arrange
            var playlisId = SeededPlaylist.Id;
            var playlistName = SeededPlaylist.Name;
            //Act
            var result = await this.playlistService.GetPlaylistByIdAsync(playlisId);
            //Assert
            var playlistInDb = this.dbContext.Playlists.Find(playlisId);
            Assert.That(result.Id, Is.EqualTo(playlistInDb.Id));
            Assert.That(result.Name, Is.EqualTo(playlistInDb.Name));
        }
        [Test]
        public async Task GetSongToAddToPlaylistByIdAsync_ShouldReturnCorrectSongData()
        {
            //Arrange
            var songId = SeededSong.Id;
            var songTitle = SeededSong.Title;
            //Act
            var result = await this.playlistService.GetSongToAddToPlaylistByIdAsync(songId);
            //Assert
            var songInDb = this.dbContext.Songs.Find(songId);
            Assert.That(result.Id, Is.EqualTo(songInDb.Id));
            Assert.That(result.Title, Is.EqualTo(songInDb.Title));
        }
        [Test]
        public async Task IsUserOwnerOfPlaylist_ShouldReturnTrue()
        {
            //Arrange
            var userId = GuestUser.Id.ToString();
            var playlistId = SeededPlaylist.Id;
            //Act
            var result = await this.playlistService.IsUserOwnerOfPlaylist(playlistId, userId);
            //Assert
            Assert.That(result, Is.True);
        }
        [Test]
        public async Task IsUserOwnerOfPlaylist_ShouldReturnFalse()
        {
            //Arrange
            var userId = SeededArtist.Id.ToString();
            var playlistId = SeededPlaylist.Id;
            //Act
            var result = await this.playlistService.IsUserOwnerOfPlaylist(playlistId, userId);
            //Assert
            Assert.That(result, Is.False);
        }
        [Test]
        public async Task RemoveSongFromPlaylist_ShouldRemoveEntryFromPlaylistsSongs()
        {
            //Arrange
            var playlistsSongsCountBefore = this.dbContext.PlaylistsSongs.Count();
            var songId = SeededSong.Id;
            var playlistId = SeededPlaylist.Id;
            //Act
            await this.playlistService.RemoveSongFromPlaylist(playlistId, songId);
            var playlistsSongsCountAfter = this.dbContext.PlaylistsSongs.Count();
            //Assert
            Assert.That(playlistsSongsCountAfter, Is.EqualTo(playlistsSongsCountBefore - 1));
        }
    }
}
