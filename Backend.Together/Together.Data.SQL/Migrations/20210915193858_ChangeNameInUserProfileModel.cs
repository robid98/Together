using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Data.SQL.Migrations
{
    public partial class ChangeNameInUserProfileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserProfileImgBlobLink",
                table: "UsersProfiles",
                newName: "UserProfileImgGeneratedName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserProfileImgGeneratedName",
                table: "UsersProfiles",
                newName: "UserProfileImgBlobLink");
        }
    }
}
