using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscussionForum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCommentRatingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentRating_Comments_CommentId",
                table: "CommentRating");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentRating_Users_UserId",
                table: "CommentRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentRating",
                table: "CommentRating");

            migrationBuilder.RenameTable(
                name: "CommentRating",
                newName: "CommentRatings");

            migrationBuilder.RenameIndex(
                name: "IX_CommentRating_UserId",
                table: "CommentRatings",
                newName: "IX_CommentRatings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentRating_CommentId",
                table: "CommentRatings",
                newName: "IX_CommentRatings_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentRatings",
                table: "CommentRatings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentRatings_Comments_CommentId",
                table: "CommentRatings",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentRatings_Users_UserId",
                table: "CommentRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentRatings_Comments_CommentId",
                table: "CommentRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentRatings_Users_UserId",
                table: "CommentRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentRatings",
                table: "CommentRatings");

            migrationBuilder.RenameTable(
                name: "CommentRatings",
                newName: "CommentRating");

            migrationBuilder.RenameIndex(
                name: "IX_CommentRatings_UserId",
                table: "CommentRating",
                newName: "IX_CommentRating_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentRatings_CommentId",
                table: "CommentRating",
                newName: "IX_CommentRating_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentRating",
                table: "CommentRating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentRating_Comments_CommentId",
                table: "CommentRating",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentRating_Users_UserId",
                table: "CommentRating",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
