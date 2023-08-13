using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeyeMusic.Data.Migrations
{
    public partial class SeedAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"), 0, "168db782-afdd-4202-ade8-5c86d165b4b5", "admin@redeye.com", false, "Admin", "Admin", false, null, "ADMIN@REDEYE.COM", "ADMIN@REDEYE.COM", "AQAAAAEAACcQAAAAEPbp7AL4DPnzMC0uEBpKIgfCCin2qX7butG9qgtZZDepW1ZZrFjnCO8fOzIChVZRag==", null, false, null, false, "admin@redeye.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("50b29c32-294d-495b-9035-ba909f23ef9f"), 0, "84ec6a4e-c79f-4082-a8d9-e245ff340fa7", "artist@redeye.com", false, "Artist", "Artistov", false, null, "ARTIST@REDEYE.COM", "ARTIST@REDEYE.COM", "AQAAAAEAACcQAAAAEP0bTl5TMv7IQACgVhw9QvMwSDkeXV2+y5ILyn+w28k03fgTQtwXlbrQjRZKgaVsvw==", null, false, null, false, "artist@redeye.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("ca9700e4-2046-4b6b-950e-ba6d762ac6b1"), 0, "1dccb269-23d0-4cf5-bd04-688554e2ae4b", "guest@redeye.com", false, "Guest", "Guestov", false, null, "GUEST@REDEYE.COM", "GUEST@REDEYE.COM", "AQAAAAEAACcQAAAAEInYyKxX1z1n8k7sC2+E5KygUXSHxMWq+VHFPU0t5SkvZ8kTz917E7fjUwgm4Tjchg==", null, false, null, false, "guest@redeye.com" });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "ApplicationUserId", "Name" },
                values: new object[] { -1, new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"), "Artist" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("50b29c32-294d-495b-9035-ba909f23ef9f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("ca9700e4-2046-4b6b-950e-ba6d762ac6b1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3cdfc504-e0e3-41cd-bd90-7c711143fe69"));
        }
    }
}
