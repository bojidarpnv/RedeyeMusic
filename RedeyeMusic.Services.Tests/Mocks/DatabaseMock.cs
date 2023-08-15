using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;

namespace RedeyeMusic.Services.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static RedeyeMusicDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<RedeyeMusicDbContext>()
                    .UseInMemoryDatabase("RedeyeMusicMockDb")
                    .Options;
                return new RedeyeMusicDbContext(dbContextOptions, false);
            }
        }

    }
}
