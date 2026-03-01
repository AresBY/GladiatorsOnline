using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gladiators.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dexterity",
                table: "Gladiators");

            migrationBuilder.DropColumn(
                name: "Intuition",
                table: "Gladiators");

            migrationBuilder.DropColumn(
                name: "PortraitID",
                table: "Gladiators");

            migrationBuilder.DropColumn(
                name: "Stamina",
                table: "Gladiators");

            migrationBuilder.RenameColumn(
                name: "Strength",
                table: "Gladiators",
                newName: "HPMax");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSlaves_OwnerId",
                table: "PlayerSlaves",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketSlaves_PlayerId",
                table: "MarketSlaves",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketSlaves_Users_PlayerId",
                table: "MarketSlaves",
                column: "PlayerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSlaves_Users_OwnerId",
                table: "PlayerSlaves",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketSlaves_Users_PlayerId",
                table: "MarketSlaves");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSlaves_Users_OwnerId",
                table: "PlayerSlaves");

            migrationBuilder.DropIndex(
                name: "IX_PlayerSlaves_OwnerId",
                table: "PlayerSlaves");

            migrationBuilder.DropIndex(
                name: "IX_MarketSlaves_PlayerId",
                table: "MarketSlaves");

            migrationBuilder.RenameColumn(
                name: "HPMax",
                table: "Gladiators",
                newName: "Strength");

            migrationBuilder.AddColumn<int>(
                name: "Dexterity",
                table: "Gladiators",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intuition",
                table: "Gladiators",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PortraitID",
                table: "Gladiators",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stamina",
                table: "Gladiators",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
