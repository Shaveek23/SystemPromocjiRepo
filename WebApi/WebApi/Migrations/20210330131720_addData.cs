using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class addData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentID", "Content", "DateTime", "PostID", "UserID" },
                values: new object[] { 1, "tralalala ", new DateTime(2021, 3, 30, 15, 17, 20, 418, DateTimeKind.Local).AddTicks(6952), 1, 1 });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentID", "Content", "DateTime", "PostID", "UserID" },
                values: new object[] { 2, "tralalala pararara", new DateTime(2021, 3, 30, 15, 17, 20, 422, DateTimeKind.Local).AddTicks(2920), 2, 1 });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentID", "Content", "DateTime", "PostID", "UserID" },
                values: new object[] { 3, "tu ti tu rum tu tu", new DateTime(2021, 3, 30, 15, 17, 20, 422, DateTimeKind.Local).AddTicks(2972), 1, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 3);
        }
    }
}
