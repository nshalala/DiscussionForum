using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscussionForum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedDiscussionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_Communities_CommunityId",
                table: "Discussion");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussion_Users_UserId",
                table: "Discussion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussion",
                table: "Discussion");

            migrationBuilder.RenameTable(
                name: "Discussion",
                newName: "Discussions");

            migrationBuilder.RenameIndex(
                name: "IX_Discussion_UserId",
                table: "Discussions",
                newName: "IX_Discussions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Discussion_CommunityId",
                table: "Discussions",
                newName: "IX_Discussions_CommunityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussions",
                table: "Discussions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Communities_CommunityId",
                table: "Discussions",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussions_Users_UserId",
                table: "Discussions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Communities_CommunityId",
                table: "Discussions");

            migrationBuilder.DropForeignKey(
                name: "FK_Discussions_Users_UserId",
                table: "Discussions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discussions",
                table: "Discussions");

            migrationBuilder.RenameTable(
                name: "Discussions",
                newName: "Discussion");

            migrationBuilder.RenameIndex(
                name: "IX_Discussions_UserId",
                table: "Discussion",
                newName: "IX_Discussion_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Discussions_CommunityId",
                table: "Discussion",
                newName: "IX_Discussion_CommunityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discussion",
                table: "Discussion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussion_Communities_CommunityId",
                table: "Discussion",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discussion_Users_UserId",
                table: "Discussion",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
