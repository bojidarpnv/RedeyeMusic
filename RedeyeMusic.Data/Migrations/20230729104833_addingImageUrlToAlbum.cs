using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedeyeMusic.Data.Migrations
{
    public partial class addingImageUrlToAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Albums");
        }
    }
}
