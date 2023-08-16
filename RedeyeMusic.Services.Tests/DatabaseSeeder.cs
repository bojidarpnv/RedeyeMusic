using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;


namespace RedeyeMusic.Services.Tests
{
    public static class DatabaseSeeder
    {
        public static ApplicationUser ArtistUser;
        public static ApplicationUser GuestUser;
        public static Artist SeededArtist;
        public static Song SeededSong;
        public static Song SeededSong2;
        public static Album SeededAlbum;
        public static Genre SeededGenre;
        public static Playlist SeededPlaylist;
        public static PlaylistsSongs SeededPlaylistSongs;
        public static void SeedDatabase(RedeyeMusicDbContext dbContext)
        {
            ArtistUser = new ApplicationUser()
            {
                IsDeleted = false,
                UserName = "artist@redeye.com",
                NormalizedUserName = "ARTIST@REDEYE.COM",
                Email = "artist@redeye.com",
                NormalizedEmail = "ARTIST@REDEYE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEHICVF9VFMlUO80SeQY8CniXilqwLCXenAmpT1UCtChYK4z4FjjOuCo1tztXRLzpVw==",
                SecurityStamp = "12026709-4d15-49bc-9026-5da249682855",
                ConcurrencyStamp = "6ae97da4-58f7-49e8-8027-678c9f01b1b5",
                TwoFactorEnabled = false,
                FirstName = "Artist",
                LastName = "Artistov"
            };
            GuestUser = new ApplicationUser()
            {
                IsDeleted = false,
                UserName = "guest@redeye.com",
                NormalizedUserName = "Guest@REDEYE.COM",
                Email = "guest@redeye.com",
                NormalizedEmail = "GUEST@REDEYE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEHICVF9VFMlUO80SeQY8CniXilqwLCXenAmpT1UCtChYK4z4FjjOuCo1tztXRLzpVw==",
                SecurityStamp = "48f2a383-47ee-430d-8fad-e172bfe7a006",
                ConcurrencyStamp = "da9576d6-3d9b-48ef-86a9-3caf52eaeb1e",
                TwoFactorEnabled = false,
                FirstName = "Guest",
                LastName = "Guestov"
            };
            SeededArtist = new Artist()
            {

                Name = "Artist",
                ApplicationUser = ArtistUser,
            };
            SeededGenre = new Genre()
            {
                Id = 1,
                Name = "SeededGenre"
            };

            SeededAlbum = new Album()
            {
                Name = "FirstAlbum",
                ArtistId = 1,
                ImageUrl = "https://images.nightcafe.studio/jobs/nrOcbGDvjDgIZPO7rbA6/nrOcbGDvjDgIZPO7rbA6.jpg?tr=w-1600,c-at_max",
                Description = "Mauris suscipit, nunc sit amet sollicitudin varius, nisl eros consectetur diam, nec.",
                Songs = new List<Song>()
                {
                    SeededSong,
                }
            };


            SeededSong = new Song()
            {
                Id = 1,
                Title = "SampleSong",
                Lyrics = "SampleSongLyrics",
                ImageUrl = "https://images.theconversation.com/files/258026/original/file-20190208-174861-nms2kt.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=926&fit=clip",
                Mp3FilePath = "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3",
                ArtistId = 1,
                AlbumId = 1,
                GenreId = 1,
            };
            SeededSong2 = new Song()
            {
                Id = 2,
                Title = "SampleSong2",
                Lyrics = "SampleSongLyrics2",
                ImageUrl = "https://images.theconversation.com/files/258026/original/file-20190208-174861-nms2kt.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=926&fit=clip",
                Mp3FilePath = "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3",
                ArtistId = 1,
                AlbumId = 1,
                GenreId = 1,
            };

            dbContext.Users.Add(ArtistUser);
            dbContext.Users.Add(GuestUser);
            dbContext.Artists.Add(SeededArtist);
            dbContext.Albums.Add(SeededAlbum);
            dbContext.Songs.Add(SeededSong);
            dbContext.Songs.Add(SeededSong2);
            dbContext.Genres.Add(SeededGenre);


            SeededPlaylist = new Playlist()
            {

                Id = 1,
                Name = "SeededPlaylist",
                ApplicationUserId = GuestUser.Id,

            };
            SeededPlaylistSongs = new PlaylistsSongs()
            {
                SongId = 1,
                PlaylistId = 1,
            };
            dbContext.Playlists.Add(SeededPlaylist);
            dbContext.PlaylistsSongs.Add(SeededPlaylistSongs);
            dbContext.SaveChanges();

        }

    }

}
