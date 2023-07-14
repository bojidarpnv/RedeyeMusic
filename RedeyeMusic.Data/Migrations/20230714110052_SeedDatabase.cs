using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeyeMusic.Data.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "FirstGenre" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "SecondGenre" });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "ArtistId", "Description", "GenreId", "Name" },
                values: new object[] { 1, 1, "Mauris suscipit, nunc sit amet sollicitudin varius, nisl eros consectetur diam, nec.", 1, "FirstAlbum" });

            migrationBuilder.InsertData(
                table: "Albums",
                columns: new[] { "Id", "ArtistId", "Description", "GenreId", "Name" },
                values: new object[] { 2, 1, "Nunc vitae imperdiet felis. Integer ac finibus turpis. Praesent ipsum arcu, placerat.", 2, "SecondAlbum" });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "ArtistId", "Duration", "FilePath", "GenreId", "ImageUrl", "ListenCount", "Lyrics", "PlaylistId", "Title" },
                values: new object[] { 1, 1, 1, 0, "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3", 1, "https://images.theconversation.com/files/258026/original/file-20190208-174861-nms2kt.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=926&fit=clip", 0, null, null, "SampleSong" });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "ArtistId", "Duration", "FilePath", "GenreId", "ImageUrl", "ListenCount", "Lyrics", "PlaylistId", "Title" },
                values: new object[] { 2, 2, 1, 0, "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3", 2, "https://mir-s3-cdn-cf.behance.net/project_modules/1400/fe529a64193929.5aca8500ba9ab.jpg", 0, null, null, "SampleSong2" });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "AlbumId", "ArtistId", "Duration", "FilePath", "GenreId", "ImageUrl", "ListenCount", "Lyrics", "PlaylistId", "Title" },
                values: new object[] { 3, 1, 1, 0, "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3", 1, "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/149562217/original/fc77d96de1229ad6ca6f83289fd2d4b4c068a568/make-album-and-song-covers.jpg", 0, null, null, "SampleSong3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
