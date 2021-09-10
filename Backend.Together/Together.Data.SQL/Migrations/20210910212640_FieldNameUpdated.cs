using Microsoft.EntityFrameworkCore.Migrations;

namespace Together.Data.SQL.Migrations
{
    public partial class FieldNameUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReplyDeleted",
                table: "Replies",
                newName: "ReplyDeleted");

            migrationBuilder.RenameColumn(
                name: "IsPostDeleted",
                table: "Posts",
                newName: "PostDeleted");

            migrationBuilder.RenameColumn(
                name: "IsCommentDeleted",
                table: "Comments",
                newName: "CommentDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReplyDeleted",
                table: "Replies",
                newName: "IsReplyDeleted");

            migrationBuilder.RenameColumn(
                name: "PostDeleted",
                table: "Posts",
                newName: "IsPostDeleted");

            migrationBuilder.RenameColumn(
                name: "CommentDeleted",
                table: "Comments",
                newName: "IsCommentDeleted");
        }
    }
}
