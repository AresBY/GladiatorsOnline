using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gladiators.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRemainingStatBoosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainingStatBoosts",
                table: "PlayerSlaves",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingStatBoosts",
                table: "PlayerSlaves");
        }
    }
}
