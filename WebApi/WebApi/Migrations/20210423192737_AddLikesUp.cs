using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddLikesUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 4, 23, 21, 27, 36, 654, DateTimeKind.Local).AddTicks(2456));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2021, 4, 23, 21, 27, 36, 656, DateTimeKind.Local).AddTicks(7769));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2021, 4, 23, 21, 27, 36, 656, DateTimeKind.Local).AddTicks(7810));

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
                value: new DateTime(2021, 4, 23, 21, 25, 16, 132, DateTimeKind.Local).AddTicks(1534));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2021, 4, 23, 21, 25, 16, 135, DateTimeKind.Local).AddTicks(8276));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2021, 4, 23, 21, 25, 16, 135, DateTimeKind.Local).AddTicks(8343));

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
