using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeyeMusic.Data.Migrations
{
    public partial class AddedSecurityStampToSeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("23343982-be97-42a3-932a-88ea79a787e5"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9550db9f-c038-4e5a-b7bc-7ce4ca67c760"));

            migrationBuilder.AlterColumn<string>(
                name: "Lyrics",
                table: "Songs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "749faae4-e120-4c49-8eeb-7641b0207e2d", "AQAAAAEAACcQAAAAEHDt7l+p02kEZci4rbq0p7QJ8z28gqtInRq0TvU2CmRkai//fGNSW1RPDWeZXNLb5Q==", "86d5504f-d6d4-4244-aa4a-91adb232a434" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("26a6aa9b-8875-4710-8df7-81fddff69c43"), 0, "da9576d6-3d9b-48ef-86a9-3caf52eaeb1e", "guest@redeye.com", false, "Guest", "Guestov", false, null, "GUEST@REDEYE.COM", "GUEST@REDEYE.COM", "AQAAAAEAACcQAAAAEF4I4uL440rR/UfRhIkzu9UDwSU5HVZdmuPCiABmxxOwLk5L8IzVDvMdgrLusGy9NQ==", null, false, "48f2a383-47ee-430d-8fad-e172bfe7a006", false, "guest@redeye.com" },
                    { new Guid("a7b1ecad-7870-4d98-85dd-4e2bb4952fe2"), 0, "6ae97da4-58f7-49e8-8027-678c9f01b1b5", "artist@redeye.com", false, "Artist", "Artistov", false, null, "ARTIST@REDEYE.COM", "ARTIST@REDEYE.COM", "AQAAAAEAACcQAAAAEHICVF9VFMlUO80SeQY8CniXilqwLCXenAmpT1UCtChYK4z4FjjOuCo1tztXRLzpVw==", null, false, "12026709-4d15-49bc-9026-5da249682855", false, "artist@redeye.com" }
                });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Lyrics",
                value: "SampleSongLyrics");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Lyrics",
                value: "SampleSong2Lyrics");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Lyrics",
                value: "SampleSong3Lyrics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("26a6aa9b-8875-4710-8df7-81fddff69c43"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a7b1ecad-7870-4d98-85dd-4e2bb4952fe2"));

            migrationBuilder.AlterColumn<string>(
                name: "Lyrics",
                table: "Songs",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "564e5645-cca2-4fd7-8f75-8c332443f786", "AQAAAAEAACcQAAAAENujntg94efD0ivDAk9QHLatKpIFwMlBzEyIUKaUqizT+4aIGEaqgygb2U3q3jolew==", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("23343982-be97-42a3-932a-88ea79a787e5"), 0, "6ac5206c-564e-4012-8f3c-89047ae8685b", "guest@redeye.com", false, "Guest", false, "Guestov", false, null, "GUEST@REDEYE.COM", "GUEST@REDEYE.COM", "AQAAAAEAACcQAAAAECX60IVxqttDNMR83YKt7lZA1dYMuZe2uLsedk410HhPqxrhR6JzvIuXG78wlE/4Hg==", null, false, null, false, "guest@redeye.com" },
                    { new Guid("9550db9f-c038-4e5a-b7bc-7ce4ca67c760"), 0, "f83510a4-9a48-4e1f-a5bb-28d2fbd031ec", "artist@redeye.com", false, "Artist", false, "Artistov", false, null, "ARTIST@REDEYE.COM", "ARTIST@REDEYE.COM", "AQAAAAEAACcQAAAAEDrsAYeXoDVkj37kgb+6iYOc1YMR6lcZu9IQTVI9lyXvs2NATkhmwlx3LXmj1FkLbA==", null, false, null, false, "artist@redeye.com" }
                });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Lyrics",
                value: null);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Lyrics",
                value: null);

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Lyrics",
                value: null);
        }
    }
}
