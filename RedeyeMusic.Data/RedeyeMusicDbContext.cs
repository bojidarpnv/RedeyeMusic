using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data.Models;
using System.Reflection;

namespace RedeyeMusic.Data
{
    public class RedeyeMusicDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public RedeyeMusicDbContext(DbContextOptions<RedeyeMusicDbContext> options)
            : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; } = null!;
        public DbSet<Playlist> Playlists { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Applying Configuration from SongEntityConfigurations
            Assembly configAssembly = Assembly.GetAssembly(typeof(RedeyeMusicDbContext)) ?? Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);

            //Remove these when in production
            builder.Entity<ApplicationUser>()
                .Property(p => p.IsSubscribed)
                .HasDefaultValue(true);
            builder.Entity<ApplicationUser>()
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            base.OnModelCreating(builder);
        }

    }
}