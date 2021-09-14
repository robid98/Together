using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Data.SQL.Migrations
{
    public partial class UserModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileModelUserProfileId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersAuthInfos",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAuthInfos", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UsersProfiles",
                columns: table => new
                {
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileImg = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserPostsNumber = table.Column<int>(type: "int", nullable: false),
                    UserFriendsNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProfiles", x => x.UserProfileId);
                    table.ForeignKey(
                        name: "FK_UsersProfiles_UsersAuthInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersAuthInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserProfileModelUserProfileId",
                table: "Posts",
                column: "UserProfileModelUserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAuthInfos_Email",
                table: "UsersAuthInfos",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsersProfiles_UserId",
                table: "UsersProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UsersProfiles_UserProfileModelUserProfileId",
                table: "Posts",
                column: "UserProfileModelUserProfileId",
                principalTable: "UsersProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UsersProfiles_UserProfileModelUserProfileId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "UsersProfiles");

            migrationBuilder.DropTable(
                name: "UsersAuthInfos");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserProfileModelUserProfileId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserProfileModelUserProfileId",
                table: "Posts");
        }
    }
}
