using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Data.SQL.Migrations
{
    public partial class UserProfileImgTypeUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfileImg",
                table: "UsersProfiles");

            migrationBuilder.AddColumn<string>(
                name: "UserProfileImgBlobLink",
                table: "UsersProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserProfileImgBlobLink",
                table: "UsersProfiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "UserProfileImg",
                table: "UsersProfiles",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
