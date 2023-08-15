using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data.Configurations;
using RedeyeMusic.Data.Configurations.SeedersConfigurations;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data
{
    public class RedeyeMusicDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly bool seedDb;
        public RedeyeMusicDbContext(DbContextOptions<RedeyeMusicDbContext> options, bool seed = true)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
            else
            {
                Database.EnsureCreated();
            }

            this.seedDb = seed;
        }

        public DbSet<Song> Songs { get; set; } = null!;
        public DbSet<Playlist> Playlists { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<PlaylistsSongs> PlaylistsSongs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Applying Configuration from SongEntityConfigurations
            //Assembly configAssembly = Assembly.GetAssembly(typeof(RedeyeMusicDbContext)) ?? Assembly.GetExecutingAssembly();
            //builder.ApplyConfigurationsFromAssembly(configAssembly);
            builder.ApplyConfiguration(new AlbumEntityConfigurations());
            builder.ApplyConfiguration(new ApplicationUserEntityConfigurations());
            builder.ApplyConfiguration(new ArtistEntityConfigurations());
            builder.ApplyConfiguration(new GenreEntityConfigurations());
            builder.ApplyConfiguration(new PlaylistEntityConfigurations());
            builder.ApplyConfiguration(new PlaylistsSongsEntityConfigurations());
            builder.ApplyConfiguration(new SongEntityConfigurations());
            if (this.seedDb)
            {
                builder.ApplyConfiguration(new SeedAlbumEntityConfigurations());
                builder.ApplyConfiguration(new SeedApplicationUserEntityConfigurations());
                builder.ApplyConfiguration(new SeedArtistEntityConfigurations());
                builder.ApplyConfiguration(new SeedGenreEntityConfigurations());
                builder.ApplyConfiguration(new SeedSongEntityConfigurations());
            }
            //Remove these when in production


            base.OnModelCreating(builder);
        }

    }
}