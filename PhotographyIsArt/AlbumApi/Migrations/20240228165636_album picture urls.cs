using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbumApi.Migrations
{
    /// <inheritdoc />
    public partial class albumpictureurls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumPicturesUrls",
                table: "Albums",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumPicturesUrls",
                table: "Albums");
        }
    }
}
