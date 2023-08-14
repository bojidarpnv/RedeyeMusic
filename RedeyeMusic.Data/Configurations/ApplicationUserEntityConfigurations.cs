using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations
{
    public class ApplicationUserEntityConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        private ApplicationUser AdminUser { get; set; }
        private ApplicationUser GuestUser { get; set; }
        private ApplicationUser ArtistUser { get; set; }
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder
               .Property(i => i.IsDeleted)
               .HasDefaultValue(false);
            builder
                .Property(u => u.FirstName)
                .HasDefaultValue("Test");
            builder
                .Property(u => u.LastName)
                .HasDefaultValue("Testov");
            builder.HasData(this.SeedUsers());

        }
        private IList<ApplicationUser> SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            List<ApplicationUser> users = new List<ApplicationUser>();
            AdminUser = new ApplicationUser()
            {
                Id = Guid.Parse("3CDFC504-E0E3-41CD-BD90-7C711143FE69"),
                Email = "admin@redeye.com",
                NormalizedEmail = "ADMIN@REDEYE.COM",
                UserName = "admin@redeye.com",
                NormalizedUserName = "ADMIN@REDEYE.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "Admin"
            };

            AdminUser.PasswordHash = hasher.HashPassword(AdminUser, "123456");
            users.Add(AdminUser);
            GuestUser = new ApplicationUser()
            {
                Id = Guid.NewGuid(),
                Email = "guest@redeye.com",
                NormalizedEmail = "GUEST@REDEYE.COM",
                UserName = "guest@redeye.com",
                NormalizedUserName = "GUEST@REDEYE.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Guest",
                LastName = "Guestov"
            };

            GuestUser.PasswordHash = hasher.HashPassword(GuestUser, "123456");
            users.Add(GuestUser);
            ArtistUser = new ApplicationUser()
            {
                Id = Guid.Parse("A7B1ECAD-7870-4D98-85DD-4E2BB4952FE2"),
                Email = "artist@redeye.com",
                NormalizedEmail = "ARTIST@REDEYE.COM",
                UserName = "artist@redeye.com",
                NormalizedUserName = "ARTIST@REDEYE.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Artist",
                LastName = "Artistov"
            };

            ArtistUser.PasswordHash = hasher.HashPassword(ArtistUser, "123456");
            users.Add(ArtistUser);

            return users;
        }
    }
}
