using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orichalcum.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGameCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_GameSessionData_GameSessionId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionData_Users_GameMasterId",
                table: "GameSessionData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionData",
                table: "GameSessionData");

            migrationBuilder.RenameTable(
                name: "GameSessionData",
                newName: "GameSessions");

            migrationBuilder.RenameIndex(
                name: "IX_GameSessionData_GameMasterId",
                table: "GameSessions",
                newName: "IX_GameSessions_GameMasterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessions",
                table: "GameSessions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GameCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCards_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BodyText = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    IsVisibleToPlayers = table.Column<bool>(type: "bit", nullable: false),
                    GameCardId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameNotes_GameCards_GameCardId",
                        column: x => x.GameCardId,
                        principalTable: "GameCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNotes_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCards_OwnerId",
                table: "GameCards",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNotes_AuthorId",
                table: "GameNotes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNotes_GameCardId",
                table: "GameNotes",
                column: "GameCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_GameSessions_GameSessionId",
                table: "Applications",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessions_Users_GameMasterId",
                table: "GameSessions",
                column: "GameMasterId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_GameSessions_GameSessionId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessions_Users_GameMasterId",
                table: "GameSessions");

            migrationBuilder.DropTable(
                name: "GameNotes");

            migrationBuilder.DropTable(
                name: "GameCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessions",
                table: "GameSessions");

            migrationBuilder.RenameTable(
                name: "GameSessions",
                newName: "GameSessionData");

            migrationBuilder.RenameIndex(
                name: "IX_GameSessions_GameMasterId",
                table: "GameSessionData",
                newName: "IX_GameSessionData_GameMasterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionData",
                table: "GameSessionData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_GameSessionData_GameSessionId",
                table: "Applications",
                column: "GameSessionId",
                principalTable: "GameSessionData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionData_Users_GameMasterId",
                table: "GameSessionData",
                column: "GameMasterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
