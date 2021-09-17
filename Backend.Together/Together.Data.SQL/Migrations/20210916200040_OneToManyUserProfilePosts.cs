using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Data.SQL.Migrations
{
    public partial class OneToManyUserProfilePosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UsersProfiles_UserProfileModelUserProfileId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserProfileModelUserProfileId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserProfileModelUserProfileId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserProfileId",
                table: "Posts",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UsersProfiles_UserProfileId",
                table: "Posts",
                column: "UserProfileId",
                principalTable: "UsersProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UsersProfiles_UserProfileId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserProfileId",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileModelUserProfileId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserProfileModelUserProfileId",
                table: "Posts",
                column: "UserProfileModelUserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UsersProfiles_UserProfileModelUserProfileId",
                table: "Posts",
                column: "UserProfileModelUserProfileId",
                principalTable: "UsersProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
