using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeyeMusic.Data.Migrations
{
    public partial class RenameFilePathToMp3FilePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("50b29c32-294d-495b-9035-ba909f23ef9f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ca9700e4-2046-4b6b-950e-ba6d762ac6b1"));

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Songs",
                newName: "Mp3FilePath");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "564e5645-cca2-4fd7-8f75-8c332443f786", "AQAAAAEAACcQAAAAENujntg94efD0ivDAk9QHLatKpIFwMlBzEyIUKaUqizT+4aIGEaqgygb2U3q3jolew==" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("23343982-be97-42a3-932a-88ea79a787e5"), 0, "6ac5206c-564e-4012-8f3c-89047ae8685b", "guest@redeye.com", false, "Guest", "Guestov", false, null, "GUEST@REDEYE.COM", "GUEST@REDEYE.COM", "AQAAAAEAACcQAAAAECX60IVxqttDNMR83YKt7lZA1dYMuZe2uLsedk410HhPqxrhR6JzvIuXG78wlE/4Hg==", null, false, null, false, "guest@redeye.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9550db9f-c038-4e5a-b7bc-7ce4ca67c760"), 0, "f83510a4-9a48-4e1f-a5bb-28d2fbd031ec", "artist@redeye.com", false, "Artist", "Artistov", false, null, "ARTIST@REDEYE.COM", "ARTIST@REDEYE.COM", "AQAAAAEAACcQAAAAEDrsAYeXoDVkj37kgb+6iYOc1YMR6lcZu9IQTVI9lyXvs2NATkhmwlx3LXmj1FkLbA==", null, false, null, false, "artist@redeye.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("23343982-be97-42a3-932a-88ea79a787e5"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9550db9f-c038-4e5a-b7bc-7ce4ca67c760"));

            migrationBuilder.RenameColumn(
                name: "Mp3FilePath",
                table: "Songs",
                newName: "FilePath");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "168db782-afdd-4202-ade8-5c86d165b4b5", "AQAAAAEAACcQAAAAEPbp7AL4DPnzMC0uEBpKIgfCCin2qX7butG9qgtZZDepW1ZZrFjnCO8fOzIChVZRag==" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("50b29c32-294d-495b-9035-ba909f23ef9f"), 0, "84ec6a4e-c79f-4082-a8d9-e245ff340fa7", "artist@redeye.com", false, "Artist", false, "Artistov", false, null, "ARTIST@REDEYE.COM", "ARTIST@REDEYE.COM", "AQAAAAEAACcQAAAAEP0bTl5TMv7IQACgVhw9QvMwSDkeXV2+y5ILyn+w28k03fgTQtwXlbrQjRZKgaVsvw==", null, false, null, false, "artist@redeye.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("ca9700e4-2046-4b6b-950e-ba6d762ac6b1"), 0, "1dccb269-23d0-4cf5-bd04-688554e2ae4b", "guest@redeye.com", false, "Guest", false, "Guestov", false, null, "GUEST@REDEYE.COM", "GUEST@REDEYE.COM", "AQAAAAEAACcQAAAAEInYyKxX1z1n8k7sC2+E5KygUXSHxMWq+VHFPU0t5SkvZ8kTz917E7fjUwgm4Tjchg==", null, false, null, false, "guest@redeye.com" });
        }
    }
}
