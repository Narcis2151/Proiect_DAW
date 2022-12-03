using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proiect_DAW.Data.Migrations
{
    public partial class Updated_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_AspNetUsers_AdreseesId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_AspNetUsers_RequestersId",
                table: "Friendship");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecipient_AspNetUsers_ApplicationUserId",
                table: "MessageRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecipient_Groups_GroupId",
                table: "MessageRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecipient_Messages_MessageId",
                table: "MessageRecipient");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageRecipient",
                table: "MessageRecipient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship");

            migrationBuilder.DropColumn(
                name: "ProfileFirstName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "ProfileLastName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PostText",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "GroupCreateDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CommentText",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "MessageRecipient",
                newName: "MessageRecipients");

            migrationBuilder.RenameTable(
                name: "Friendship",
                newName: "Friendships");

            migrationBuilder.RenameColumn(
                name: "PostLikes",
                table: "Posts",
                newName: "Likes");

            migrationBuilder.RenameColumn(
                name: "PostCreateDate",
                table: "Posts",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ApplicationUserId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameColumn(
                name: "GroupDescription",
                table: "Groups",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Groups",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CommentCreateDate",
                table: "Comments",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Comments",
                newName: "CreatorUserId");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                newName: "IX_Comments_CreatorUserId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "MessageRecipients",
                newName: "GroupReceiverId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "MessageRecipients",
                newName: "UserReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageRecipient_MessageId",
                table: "MessageRecipients",
                newName: "IX_MessageRecipients_MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageRecipient_GroupId",
                table: "MessageRecipients",
                newName: "IX_MessageRecipients_GroupReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageRecipient_ApplicationUserId",
                table: "MessageRecipients",
                newName: "IX_MessageRecipients_UserReceiverId");

            migrationBuilder.RenameColumn(
                name: "RequestersId",
                table: "Friendships",
                newName: "RequesterId");

            migrationBuilder.RenameColumn(
                name: "AdreseesId",
                table: "Friendships",
                newName: "AdreseeId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_RequestersId",
                table: "Friendships",
                newName: "IX_Friendships_RequesterId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendship_AdreseesId",
                table: "Friendships",
                newName: "IX_Friendships_AdreseeId");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageRecipients",
                table: "MessageRecipients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ApplicationUserGroups",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGroups", x => new { x.ApplicationUserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroups_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGroups_GroupId",
                table: "ApplicationUserGroups",
                column: "GroupId");


            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CreatorUserId",
                table: "Comments",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_AdreseeId",
                table: "Friendships",
                column: "AdreseeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_RequesterId",
                table: "Friendships",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecipients_AspNetUsers_UserReceiverId",
                table: "MessageRecipients",
                column: "UserReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecipients_Groups_GroupReceiverId",
                table: "MessageRecipients",
                column: "GroupReceiverId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecipients_Messages_MessageId",
                table: "MessageRecipients",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CreatorUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_AdreseeId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_RequesterId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecipients_AspNetUsers_UserReceiverId",
                table: "MessageRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecipients_Groups_GroupReceiverId",
                table: "MessageRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageRecipients_Messages_MessageId",
                table: "MessageRecipients");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "ApplicationUserGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageRecipients",
                table: "MessageRecipients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "MessageRecipients",
                newName: "MessageRecipient");

            migrationBuilder.RenameTable(
                name: "Friendships",
                newName: "Friendship");

            migrationBuilder.RenameColumn(
                name: "Likes",
                table: "Posts",
                newName: "PostLikes");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Posts",
                newName: "PostCreateDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Posts",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Messages",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                newName: "IX_Messages_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Groups",
                newName: "GroupDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Groups",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "Comments",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Comments",
                newName: "CommentCreateDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CreatorUserId",
                table: "Comments",
                newName: "IX_Comments_ApplicationUserId");


            migrationBuilder.RenameColumn(
                name: "UserReceiverId",
                table: "MessageRecipient",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "GroupReceiverId",
                table: "MessageRecipient",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageRecipients_UserReceiverId",
                table: "MessageRecipient",
                newName: "IX_MessageRecipient_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageRecipients_MessageId",
                table: "MessageRecipient",
                newName: "IX_MessageRecipient_MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_MessageRecipients_GroupReceiverId",
                table: "MessageRecipient",
                newName: "IX_MessageRecipient_GroupId");

            migrationBuilder.RenameColumn(
                name: "RequesterId",
                table: "Friendship",
                newName: "RequestersId");

            migrationBuilder.RenameColumn(
                name: "AdreseeId",
                table: "Friendship",
                newName: "AdreseesId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_RequesterId",
                table: "Friendship",
                newName: "IX_Friendship_RequestersId");

            migrationBuilder.RenameIndex(
                name: "IX_Friendships_AdreseeId",
                table: "Friendship",
                newName: "IX_Friendship_AdreseesId");

            migrationBuilder.AddColumn<string>(
                name: "ProfileFirstName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileLastName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostText",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "GroupCreateDate",
                table: "Groups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentText",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageRecipient",
                table: "MessageRecipient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendship",
                table: "Friendship",
                column: "Id");


            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecipient_AspNetUsers_ApplicationUserId",
                table: "MessageRecipient",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecipient_Groups_GroupId",
                table: "MessageRecipient",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageRecipient_Messages_MessageId",
                table: "MessageRecipient",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId",
                table: "Messages",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_ApplicationUserId",
                table: "Posts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
