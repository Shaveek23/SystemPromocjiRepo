using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddLikesUp3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 4, 23, 21, 34, 57, 601, DateTimeKind.Local).AddTicks(2181));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2021, 4, 23, 21, 34, 57, 603, DateTimeKind.Local).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2021, 4, 23, 21, 34, 57, 603, DateTimeKind.Local).AddTicks(7390));

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
