using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_DAW.Data.Migrations
{
    public partial class Update_Friendships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserFriendship");

            migrationBuilder.DropTable(
                name: "ApplicationUserFriendship1");

            migrationBuilder.AddColumn<string>(
                name: "AdreseesId",
                table: "Friendship",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestersId",
                table: "Friendship",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_AdreseesId",
                table: "Friendship",
                column: "AdreseesId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_RequestersId",
                table: "Friendship",
                column: "RequestersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_AspNetUsers_AdreseesId",
                table: "Friendship",
                column: "AdreseesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_AspNetUsers_RequestersId",
                table: "Friendship",
                column: "RequestersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_AspNetUsers_AdreseesId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_AspNetUsers_RequestersId",
                table: "Friendship");

            migrationBuilder.DropIndex(
                name: "IX_Friendship_AdreseesId",
                table: "Friendship");

            migrationBuilder.DropIndex(
                name: "IX_Friendship_RequestersId",
                table: "Friendship");

            migrationBuilder.DropColumn(
                name: "AdreseesId",
                table: "Friendship");

            migrationBuilder.DropColumn(
                name: "RequestersId",
                table: "Friendship");

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
    }
}
