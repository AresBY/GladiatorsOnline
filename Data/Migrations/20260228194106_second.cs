using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gladiators.Data.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "PlayerSlaves",
                newName: "Wins");

            migrationBuilder.AddColumn<int>(
                name: "Intuition",
                table: "PlayerSlaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortraitID",
                table: "PlayerSlaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intuition",
                table: "MarketSlaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortraitID",
                table: "MarketSlaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wins",
                table: "MarketSlaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intuition",
                table: "PlayerSlaves");

            migrationBuilder.DropColumn(
                name: "PortraitID",
                table: "PlayerSlaves");

            migrationBuilder.DropColumn(
                name: "Intuition",
                table: "MarketSlaves");

            migrationBuilder.DropColumn(
                name: "PortraitID",
                table: "MarketSlaves");

            migrationBuilder.DropColumn(
                name: "Wins",
                table: "MarketSlaves");

            migrationBuilder.RenameColumn(
                name: "Wins",
                table: "PlayerSlaves",
                newName: "Price");
        }
    }
}
