using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackStar.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInWatchlist",
                table: "Series",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStarred",
                table: "Series",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInWatchlist",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStarred",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInWatchlist",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "IsStarred",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "IsInWatchlist",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "IsStarred",
                table: "Movies");
        }
    }
}
