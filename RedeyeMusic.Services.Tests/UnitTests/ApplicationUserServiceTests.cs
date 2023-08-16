using Microsoft.AspNetCore.Identity;
using Moq;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data;
using RedeyeMusic.Services.Data.Interfaces;

namespace RedeyeMusic.Services.Tests.UnitTests
{
    public class ApplicationUserServiceTests : UnitTestsBase
    {
        private IApplicationUserService userService;

        [OneTimeSetUp]
        public void SetUp()
        {
            var userId = GuestUser.Id.ToString();
            ApplicationUser user = GuestUser;

            var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new UserManager<ApplicationUser>(mockUserStore.Object, null, null, null, null, null, null, null, null);

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);

            this.userService = new ApplicationUserService(mockUserManager.Object, dbContext);
        }
        [Test]
        public async Task GetAllAsync_ShouldReturnCorrectNumberOfUsers()
        {
            //Arrange
            var expectedCount = this.dbContext.Users.Count();
            //Act
            var actualUsers = await this.userService.GetAllAsync();
            var actualCount = actualUsers.Count();
            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }
        [Test]
        public async Task GetFullNameByEmailAsync_ShouldReturnCorrectEmail()
        {
            //Arange
            var userId = GuestUser.Id;
            var user = dbContext.Users.Find(userId);
            var expectedResult = user.FirstName + " " + user.LastName;
            //Act
            var actualResult = await this.userService.GetFullNameByEmailAsync(user.Email);
            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        [Test]
        public async Task GetFullNameByEmailAsync_ShouldReturnEmptyStringIfUserIsNotFound()
        {
            //Arange
            var randomEmail = "randomemail@email.com";
            var expectedResult = string.Empty;
            //Act
            var actualResult = await this.userService.GetFullNameByEmailAsync(randomEmail);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
        [Test]
        public async Task GetFullNameByIdAsync_ShouldReturnCorrectFullName()
        {
            //Arange
            var userId = GuestUser.Id;
            var user = dbContext.Users.Find(userId);
            var expectedResult = user.FirstName + " " + user.LastName;
            //Act
            var actualResult = await this.userService.GetFullNameByIdAsync(userId.ToString());
            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

        }
        [Test]
        public async Task GetFullNameByIdAsync_ShouldReturnEmptyStringIfUserIsNotFound()
        {
            //Arange
            var randomId = Guid.NewGuid().ToString();
            var expectedResult = string.Empty;
            //Act
            var actualResult = await this.userService.GetFullNameByIdAsync(randomId);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

    }
}
