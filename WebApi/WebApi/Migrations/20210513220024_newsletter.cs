using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class newsletter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newsletter",
                columns: table => new
                {
                    NewsletterID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletter", x => x.NewsletterID);
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
                name: "Newsletter");

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
