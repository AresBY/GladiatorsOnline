using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gladiators.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Сначала удаляем старую таблицу Users, если она существует
            migrationBuilder.DropTable(
                name: "Users");

            // Создаём таблицу заново с Id типа Guid
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true)
                    // Добавь остальные поля User здесь
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // При откате — удаляем таблицу Users
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
