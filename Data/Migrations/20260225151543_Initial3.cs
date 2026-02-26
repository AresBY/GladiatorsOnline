using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gladiators.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Удаляем старую таблицу Users, если она существует
            migrationBuilder.DropTable(
                name: "Users");

            // Создаём новую таблицу Users под класс User
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаляем таблицу Users при откате миграции
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
