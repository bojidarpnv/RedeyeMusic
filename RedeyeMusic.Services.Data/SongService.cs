using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedeyeMusic.Data;
using RedeyeMusic.Data.Models;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Services.Data.Models.Song;
using RedeyeMusic.Services.Mapping;
using RedeyeMusic.Web.ViewModels.Album;
using RedeyeMusic.Web.ViewModels.Artist;
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

        public async Task<int> AddSongAsync(AddSongFormModel songModel, int artistId, string fullFilePath)
        {

            Song song = new Song()
            {
                Title = songModel.Title,
                Lyrics = songModel.Lyrics,
                ImageUrl = songModel.ImageUrl,
                Mp3FilePath = songModel.FilePath!,
                GenreId = songModel.GenreId,
                ArtistId = artistId,
                AlbumId = songModel.AlbumId,

            };


            await this.dbContext.Songs.AddAsync(song);
            await this.dbContext.SaveChangesAsync();

            int duration = GetSongDuration(fullFilePath);
            song.Duration = duration;
            await this.dbContext.SaveChangesAsync();
            return song.Id;
        }

        public async Task<IEnumerable<IndexViewModel>> GetAll()
        {
            IEnumerable<IndexViewModel> songs = await this.dbContext
                .Songs
                .Where(s => s.IsDeleted == false)
                .OrderByDescending(s => s.ListenCount)
                .Take(100)
                .To<IndexViewModel>()
                .ToArrayAsync();
            return songs;
        }
        public async Task<ICollection<GenreSelectViewModel>> SelectGenresAsync()
        {
            ICollection<GenreSelectViewModel> allGenres = await this.dbContext
                .Genres
                .To<GenreSelectViewModel>()
                .ToArrayAsync();
            return allGenres;
        }
        public async Task<int> AddFirstSongAsync(AddFirstSongFormModel songModel, int artistId, string fullFilePath)
        {
            Album album = new Album()
            {
                Name = songModel.AlbumName,
                Description = songModel.AlbumDescription,
                ArtistId = artistId,
                ImageUrl = songModel.AlbumImageUrl
            };

            await this.dbContext.Albums.AddAsync(album);
            await this.dbContext.SaveChangesAsync();

            Song song = new Song()
            {
                Title = songModel.Title,
                Lyrics = songModel.Lyrics,
                ImageUrl = songModel.ImageUrl,
                Mp3FilePath = songModel.FilePath!,
                GenreId = songModel.GenreId,
                ArtistId = artistId,
                AlbumId = album.Id,
            };

            await this.dbContext.Songs.AddAsync(song);
            await this.dbContext.SaveChangesAsync();

            int duration = GetSongDuration(song.Mp3FilePath);
            song.Duration = duration;
            await this.dbContext.SaveChangesAsync();
            return song.Id;

        }
        public int GetSongDuration(string mp3FilePath)
        {
            try
            {

                // Load the MP3 file
                var file = TagLib.File.Create(mp3FilePath);

                // Get the duration in seconds
                int durationInSeconds = (int)file.Properties.Duration.TotalSeconds;

                return durationInSeconds;
            }
            catch (Exception)
            {
                throw;
            }
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
                    .Where(s => s.IsDeleted == false)
                    .Where(s => EF.Functions.Like(s.Title, wildCard))
                    .To<IndexViewModel>()
                    .ToListAsync();
                searchResult.SongsByArtist = await songsQuery
                    .Where(s => s.IsDeleted == false)
                    .Where(s => EF.Functions.Like(s.Artist.Name, wildCard))
                    .To<IndexViewModel>()
                    .ToListAsync();
                searchResult.SongsByGenre = await songsQuery
                    .Where(s => s.IsDeleted == false)
                    .Where(s => EF.Functions.Like(s.Genre.Name, wildCard))
                    .To<IndexViewModel>()
                    .ToListAsync();
                searchResult.SongsByLyrics = await songsQuery
                    .Where(s => s.IsDeleted == false)
                    .Where(s => EF.Functions.Like(s.Lyrics!, wildCard))
                    .To<IndexViewModel>()
                    .ToListAsync();
            }

            return searchResult;
        }

        public async Task<IEnumerable<IndexViewModel>> AllByArtistIdAsync(int artistId)
        {
            IEnumerable<IndexViewModel> allArtistSongs = await this.dbContext
                .Songs
                .Where(s => s.IsDeleted == false)
                .Where(s => s.ArtistId == artistId)
                .To<IndexViewModel>()
                .ToArrayAsync();

            return allArtistSongs;
        }

        public async Task<SongDetailsViewModel> GetDetailsByIdAsync(int songId)
        {
            Song song = await this.dbContext
                .Songs
                .Where(s => s.IsDeleted == false)
                .Include(s => s.Genre)
                .Include(s => s.Album)
                .Include(s => s.Artist)
                .ThenInclude(a => a.ApplicationUser)
                .FirstAsync(s => s.Id == songId);

            return new SongDetailsViewModel()
            {

                Id = song.Id,
                Title = song.Title,
                Lyrics = song.Lyrics!,
                Duration = song.Duration,
                GenreName = song.Genre.Name,
                ImageUrl = song.ImageUrl,
                ArtistName = song.Artist.Name,
                AlbumName = song.Album.Name,
                ListenCount = song.ListenCount,
                Mp3FilePath = song.Mp3FilePath,
                Artist = new ArtistInfoOnSongViewModel()
                {
                    Id = song.Artist.Id,
                    Name = song.Artist.Name,
                    Albums = await this.dbContext.Albums
                    .Where(a => a.IsDeleted == false)
                    .Where(a => a.Artist.Id == song.Artist.Id)
                    .Select(al => new AlbumSelectViewModel()
                    {
                        Id = al.Id,
                        Name = al.Name,
                        Description = al.Description,
                        ImageUrl = al.ImageUrl!
                    })

                    .ToListAsync()
                }
            };
        }

        public async Task<bool> ExistsById(int songId)
        {
            bool result = await this.dbContext
                .Songs
                .Where(s => s.IsDeleted == false)
                .AnyAsync(s => s.Id == songId);
            return result;
        }

        public async Task<EditSongFormModel> GetSongForEditByIdAsync(int songId)
        {
            Song song = await this.dbContext
                .Songs
                .Where(s => s.IsDeleted == false)
                .Include(s => s.Genre)
                .FirstAsync(s => s.Id == songId);

            return new EditSongFormModel()
            {
                Title = song.Title,
                Lyrics = song.Lyrics!,
                GenreId = song.GenreId,
                AlbumId = song.AlbumId,
                ImageUrl = song.ImageUrl,

            };
        }

        public async Task EditSongByIdAndModel(int songId, EditSongFormModel model)
        {

            Song song = await this.dbContext
                 .Songs
                 .Where(s => s.IsDeleted == false)
                 .FirstAsync(s => s.Id == songId);
            song.Title = model.Title;
            song.Lyrics = model.Lyrics;
            song.ImageUrl = model.ImageUrl;
            song.AlbumId = model.AlbumId;
            song.GenreId = model.GenreId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteSongByIdAsync(int songId)
        {
            Song song = await this.dbContext
                .Songs
                .Where(s => s.IsDeleted == false)
                .FirstAsync(s => s.Id == songId);
            song.IsDeleted = true;
            await this.dbContext.SaveChangesAsync();
        }
        public async Task<List<SelectListItem>> GetSongsDropdownItemsAsync(int artistId)
        {
            IEnumerable<Song> songs = await dbContext.Songs
                .Where(s => s.IsDeleted == false)
                .Where(s => s.ArtistId == artistId)
                .ToArrayAsync();

            return songs
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                })
                .ToList();
        }

        public async Task<ListenCountServiceModel> GetListenCountAsync(int songId)
        {
            var song = await this.dbContext
                .Songs
                .Where(s => s.IsDeleted == false)
                .FirstAsync(s => s.Id == songId);


            return new ListenCountServiceModel()
            {
                ListenCount = song.ListenCount,
            };
        }
    }
}
