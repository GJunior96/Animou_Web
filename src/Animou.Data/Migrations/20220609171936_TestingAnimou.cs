using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Animou.Data.Migrations
{
    public partial class TestingAnimou : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Animou");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Animou",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false),
                    Avatar = table.Column<string>(type: "varchar(100)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Watched = table.Column<int>(type: "int", nullable: false),
                    Watching = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animes",
                schema: "Animou",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Animou",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                schema: "Animou",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", nullable: false),
                    AnimeId = table.Column<string>(type: "varchar(36)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Animes_Id",
                        column: x => x.Id,
                        principalSchema: "Animou",
                        principalTable: "Animes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_Id",
                        column: x => x.Id,
                        principalSchema: "Animou",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deslikes",
                schema: "Animou",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", nullable: false),
                    CommentId = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deslikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deslikes_Comments_Id",
                        column: x => x.Id,
                        principalSchema: "Animou",
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Deslikes_Users_Id",
                        column: x => x.Id,
                        principalSchema: "Animou",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "likes",
                schema: "Animou",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", nullable: false),
                    CommentId = table.Column<string>(type: "varchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_likes_Comments_Id",
                        column: x => x.Id,
                        principalSchema: "Animou",
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_likes_Users_Id",
                        column: x => x.Id,
                        principalSchema: "Animou",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animes_UserId",
                schema: "Animou",
                table: "Animes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deslikes",
                schema: "Animou");

            migrationBuilder.DropTable(
                name: "likes",
                schema: "Animou");

            migrationBuilder.DropTable(
                name: "Comments",
                schema: "Animou");

            migrationBuilder.DropTable(
                name: "Animes",
                schema: "Animou");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Animou");
        }
    }
}
