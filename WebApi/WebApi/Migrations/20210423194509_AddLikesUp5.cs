using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddLikesUp5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentLike",
                columns: table => new
                {
                    CommentLikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentLike", x => x.CommentLikeID);
                });

            migrationBuilder.CreateTable(
                name: "PostLike",
                columns: table => new
                {
                    PostLikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLike", x => x.PostLikeID);
                });

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 4, 16, 22, 30, 20, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentLike");

            migrationBuilder.DropTable(
                name: "PostLike");

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2021, 4, 16, 22, 30, 20, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified));
        }
    }
}
