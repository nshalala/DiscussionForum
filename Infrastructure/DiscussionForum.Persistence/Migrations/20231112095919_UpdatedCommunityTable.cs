using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscussionForum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCommunityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Users_AdminId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityUser_Communities_CommunitiesAsMemberId",
                table: "CommunityUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityUser_Users_MembersId",
                table: "CommunityUser");

            migrationBuilder.DropIndex(
                name: "IX_Communities_AdminId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Communities");

            migrationBuilder.RenameColumn(
                name: "MembersId",
                table: "CommunityUser",
                newName: "CommunitiesAsAdminId");

            migrationBuilder.RenameColumn(
                name: "CommunitiesAsMemberId",
                table: "CommunityUser",
                newName: "AdminUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityUser_MembersId",
                table: "CommunityUser",
                newName: "IX_CommunityUser_CommunitiesAsAdminId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Communities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommunityUser1",
                columns: table => new
                {
                    CommunitiesAsMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityUser1", x => new { x.CommunitiesAsMemberId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_CommunityUser1_Communities_CommunitiesAsMemberId",
                        column: x => x.CommunitiesAsMemberId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityUser1_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityUser1_MembersId",
                table: "CommunityUser1",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityUser_Communities_CommunitiesAsAdminId",
                table: "CommunityUser",
                column: "CommunitiesAsAdminId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityUser_Users_AdminUsersId",
                table: "CommunityUser",
                column: "AdminUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityUser_Communities_CommunitiesAsAdminId",
                table: "CommunityUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityUser_Users_AdminUsersId",
                table: "CommunityUser");

            migrationBuilder.DropTable(
                name: "CommunityUser1");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Communities");

            migrationBuilder.RenameColumn(
                name: "CommunitiesAsAdminId",
                table: "CommunityUser",
                newName: "MembersId");

            migrationBuilder.RenameColumn(
                name: "AdminUsersId",
                table: "CommunityUser",
                newName: "CommunitiesAsMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_CommunityUser_CommunitiesAsAdminId",
                table: "CommunityUser",
                newName: "IX_CommunityUser_MembersId");

            migrationBuilder.AddColumn<Guid>(
                name: "AdminId",
                table: "Communities",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Communities_AdminId",
                table: "Communities",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Users_AdminId",
                table: "Communities",
                column: "AdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityUser_Communities_CommunitiesAsMemberId",
                table: "CommunityUser",
                column: "CommunitiesAsMemberId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityUser_Users_MembersId",
                table: "CommunityUser",
                column: "MembersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
