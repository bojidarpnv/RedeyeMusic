using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Services.Data.Models.Song;
using RedeyeMusic.Web.ViewModels.Genre;
using RedeyeMusic.Web.ViewModels.Home;
using RedeyeMusic.Web.ViewModels.Song;


namespace RedeyeMusic.Services.Data
{
    public class SongService : ISongService
    {
        private readonly RedeyeMusicDbContext dbContext;


        public SongService(RedeyeMusicDbContext dbContext)
        {
            this.dbContext = dbContext;


        }

        public Task AddMp3File(AddSongFormModel songModel)
        {
            throw new NotImplementedException();
        }

        public async Task AddSongAsync(AddSongFormModel songModel, int artistId)
        {

            Song song = new Song()
            {
                Title = songModel.Title,
                Lyrics = songModel.Lyrics,
                ImageUrl = songModel.ImageUrl,
                FilePath = songModel.FilePath,
                GenreId = songModel.GenreId,
                ArtistId = artistId,
                AlbumId = songModel.AlbumId,
            };
            this.dbContext.Add(song);
            this.dbContext.SaveChanges();
        }

        public async Task<IEnumerable<IndexViewModel>> GetAll()
        {
            IEnumerable<IndexViewModel> songs = await this.dbContext
                .Songs
                .OrderByDescending(s => s.ListenCount)
                .Take(100)
                .Select(s => new IndexViewModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Duration = s.Duration,
                    ImageUrl = s.ImageUrl,
                    ListenCount = s.ListenCount,
                    Mp3FilePath = s.FilePath
                })
                .ToArrayAsync();
            return songs;
        }
        public async Task<ICollection<GenreSelectViewModel>> SelectGenresAsync()
        {
            ICollection<GenreSelectViewModel> allGenres = await this.dbContext
                .Genres
                .Select(g => new GenreSelectViewModel()
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToArrayAsync();
            return allGenres;
        }
        public async Task AddFirstSongAsync(AddFirstSongFormModel songModel, int artistId)
        {
            Album album = new Album()
            {
                Name = songModel.AlbumName,
                Description = songModel.AlbumDescription,
                ArtistId = artistId,
                //GenreId = songModel.GenreId,
            };

            await this.dbContext.Albums.AddAsync(album);
            await this.dbContext.SaveChangesAsync();

            Song song = new Song()
            {
                Title = songModel.Title,
                Lyrics = songModel.Lyrics,
                ImageUrl = songModel.ImageUrl,
                FilePath = songModel.FilePath,
                GenreId = songModel.GenreId,
                ArtistId = artistId,
                AlbumId = album.Id,
            };

            await this.dbContext.Songs.AddAsync(song);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<AllSongsSearchedModel> SearchSongsAsync(AllSongsQueryModel queryModel)
        {
            AllSongsSearchedModel searchResult = new AllSongsSearchedModel();
            IQueryable<Song> songsQuery = this.dbContext
                .Songs
                .AsQueryable();

            //if(!string.IsNullOrWhiteSpace(queryModel.SearchString)) 
            //{ 
            //    string wildCard = $"%{queryModel.SearchString.ToLower()}%";
            //    searchResult.SongsByTitle = await songsQuery.Where(s => EF.Functions.Like(s.Title,wildCard)).ToListAsync();
            //}
            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                searchResult.SongsByTitle = await songsQuery
                    .Where(s => EF.Functions.Like(s.Title, wildCard))
                    .Select(s => new IndexViewModel()
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Duration = s.Duration,
                        ImageUrl = s.ImageUrl,
                        ListenCount = s.ListenCount,
                        Mp3FilePath = s.FilePath

                    })
                    .ToListAsync();
                searchResult.SongsByArtist = await songsQuery
                    .Where(s => EF.Functions.Like(s.Artist.Name, wildCard))
                    .Select(s => new IndexViewModel()
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Duration = s.Duration,
                        ImageUrl = s.ImageUrl,
                        ListenCount = s.ListenCount,
                        Mp3FilePath = s.FilePath

                    })
                    .ToListAsync();
                searchResult.SongsByGenre = await songsQuery
                    .Where(s => EF.Functions.Like(s.Genre.Name, wildCard))
                    .Select(s => new IndexViewModel()
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Duration = s.Duration,
                        ImageUrl = s.ImageUrl,
                        ListenCount = s.ListenCount,
                        Mp3FilePath = s.FilePath

                    }).ToListAsync();
                searchResult.SongsByLyrics = await songsQuery
                    .Where(s => EF.Functions.Like(s.Lyrics, wildCard))
                    .Select(s => new IndexViewModel()
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Duration = s.Duration,
                        ImageUrl = s.ImageUrl,
                        ListenCount = s.ListenCount,
                        Mp3FilePath = s.FilePath

                    })
                    .ToListAsync();
            }

            return searchResult;
        }


    }
}
