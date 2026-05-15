using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orichalcum.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGameCardToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameCardId",
                table: "GameSessions",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameCardId",
                table: "GameSessions");
        }
    }
}
