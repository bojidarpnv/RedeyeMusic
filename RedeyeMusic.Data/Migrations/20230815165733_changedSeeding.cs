using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeyeMusic.Data.Migrations
{
    public partial class changedSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("26a6aa9b-8875-4710-8df7-81fddff69c43"));

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://images.nightcafe.studio/jobs/nrOcbGDvjDgIZPO7rbA6/nrOcbGDvjDgIZPO7rbA6.jpg?tr=w-1600,c-at_max");

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://images.nightcafe.studio/jobs/nrOcbGDvjDgIZPO7rbA6/nrOcbGDvjDgIZPO7rbA6.jpg?tr=w-1600,c-at_max");

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: -1,
                column: "Name",
                value: "Admin");

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "ApplicationUserId", "Name" },
                values: new object[] { -2, new Guid("a7b1ecad-7870-4d98-85dd-4e2bb4952fe2"), "Artist" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49d569d1-e4ac-445c-8749-f1886b3a4415", "AQAAAAEAACcQAAAAEDu7pZEPv3Vm3gGZESDHsjZL21v+FQRAC4GyxElR2BuH+tG8wUykT8Ded4YNoQg5Kg==", "2d0fb04e-5195-44d6-87ca-6e164e473633" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a7b1ecad-7870-4d98-85dd-4e2bb4952fe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d72b674d-c860-47fb-98ee-7c91a54e8d66", "AQAAAAEAACcQAAAAEK+kzKkV8nJymIFum/hdWVPUVWo8icdHTLMVlqtIiFXv+EgkBztjimdTjecpnsbR2A==", "b5574065-612b-4ecf-89cc-359feb7ddec7" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("0b106195-cde9-4c57-95e5-bcb4fc884b0f"), 0, "fd33eac2-7626-4d16-afb6-cdcac6884d92", "guest@redeye.com", false, "Guest", "Guestov", false, null, "GUEST@REDEYE.COM", "GUEST@REDEYE.COM", "AQAAAAEAACcQAAAAEDnRCkgE65V9LLQt6E6PwsYVgnidVk5YW+L/d3ACDUigx7b9Vkw5gs1gwLqcW+qUsg==", null, false, "1c7cc273-61c6-42cd-8cd9-a1af5bb1443f", false, "guest@redeye.com" });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Mp3FilePath",
                value: "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Mp3FilePath",
                value: "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Mp3FilePath",
                value: "songs/Mp3s/2410a001-848d-449f-bb02-abbafdda6447test.mp3");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("0b106195-cde9-4c57-95e5-bcb4fc884b0f"));

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: -1,
                column: "Name",
                value: "Artist");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "749faae4-e120-4c49-8eeb-7641b0207e2d", "AQAAAAEAACcQAAAAEHDt7l+p02kEZci4rbq0p7QJ8z28gqtInRq0TvU2CmRkai//fGNSW1RPDWeZXNLb5Q==", "86d5504f-d6d4-4244-aa4a-91adb232a434" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a7b1ecad-7870-4d98-85dd-4e2bb4952fe2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ae97da4-58f7-49e8-8027-678c9f01b1b5", "AQAAAAEAACcQAAAAEHICVF9VFMlUO80SeQY8CniXilqwLCXenAmpT1UCtChYK4z4FjjOuCo1tztXRLzpVw==", "12026709-4d15-49bc-9026-5da249682855" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("26a6aa9b-8875-4710-8df7-81fddff69c43"), 0, "da9576d6-3d9b-48ef-86a9-3caf52eaeb1e", "guest@redeye.com", false, "Guest", false, "Guestov", false, null, "GUEST@REDEYE.COM", "GUEST@REDEYE.COM", "AQAAAAEAACcQAAAAEF4I4uL440rR/UfRhIkzu9UDwSU5HVZdmuPCiABmxxOwLk5L8IzVDvMdgrLusGy9NQ==", null, false, "48f2a383-47ee-430d-8fad-e172bfe7a006", false, "guest@redeye.com" });

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Mp3FilePath",
                value: "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Mp3FilePath",
                value: "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Mp3FilePath",
                value: "https://file-examples.com/storage/fee472ce6e64b122ba0c8b3/2017/11/file_example_MP3_1MG.mp3");
        }
    }
}
