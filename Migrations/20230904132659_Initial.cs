using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot_OpenAI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReg = table.Column<bool>(type: "bit", nullable: false),
                    RegestrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAction = table.Column<int>(type: "int", nullable: false),
                    IdInvited = table.Column<int>(type: "int", nullable: true),
                    CountReferals = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Users_IdInvited",
                        column: x => x.IdInvited,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "GeneratedImages",
                columns: table => new
                {
                    IdImage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGenerated = table.Column<int>(type: "int", nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateGenerated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedImages", x => x.IdImage);
                    table.ForeignKey(
                        name: "FK_GeneratedImages_Users_IdGenerated",
                        column: x => x.IdGenerated,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneratedTexts",
                columns: table => new
                {
                    IdText = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextOutput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdGenerated = table.Column<int>(type: "int", nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateGenerated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedTexts", x => x.IdText);
                    table.ForeignKey(
                        name: "FK_GeneratedTexts_Users_IdGenerated",
                        column: x => x.IdGenerated,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedImages_IdGenerated",
                table: "GeneratedImages",
                column: "IdGenerated");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedTexts_IdGenerated",
                table: "GeneratedTexts",
                column: "IdGenerated");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdInvited",
                table: "Users",
                column: "IdInvited");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedImages");

            migrationBuilder.DropTable(
                name: "GeneratedTexts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
