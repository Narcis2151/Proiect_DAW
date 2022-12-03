using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_DAW.Data.Migrations
{
    public partial class Create_Friendships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GroupDescription",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GroupCreateDate",
                table: "Groups",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserFriendship",
                columns: table => new
                {
                    AdreseesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendshipsReveivedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserFriendship", x => new { x.AdreseesId, x.FriendshipsReveivedId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserFriendship_AspNetUsers_AdreseesId",
                        column: x => x.AdreseesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserFriendship_Friendship_FriendshipsReveivedId",
                        column: x => x.FriendshipsReveivedId,
                        principalTable: "Friendship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserFriendship1",
                columns: table => new
                {
                    FriendshipsSentId = table.Column<int>(type: "int", nullable: false),
                    RequestersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserFriendship1", x => new { x.FriendshipsSentId, x.RequestersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserFriendship1_AspNetUsers_RequestersId",
                        column: x => x.RequestersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserFriendship1_Friendship_FriendshipsSentId",
                        column: x => x.FriendshipsSentId,
                        principalTable: "Friendship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFriendship_FriendshipsReveivedId",
                table: "ApplicationUserFriendship",
                column: "FriendshipsReveivedId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFriendship1_RequestersId",
                table: "ApplicationUserFriendship1",
                column: "RequestersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserFriendship");

            migrationBuilder.DropTable(
                name: "ApplicationUserFriendship1");

            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.AlterColumn<string>(
                name: "GroupDescription",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "GroupCreateDate",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
