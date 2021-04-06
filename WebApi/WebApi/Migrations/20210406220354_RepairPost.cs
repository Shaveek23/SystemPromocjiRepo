using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class RepairPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "CategoryID", "Content", "Date", "IsPromoted", "Title", "UserID" },
                values: new object[] { 1, 1, "Oto mój pierwszy post!", new DateTime(2021, 3, 11, 12, 23, 46, 0, DateTimeKind.Unspecified), false, "tytuł 1", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "CategoryID", "Content", "Date", "IsPromoted", "Title", "UserID" },
                values: new object[] { 2, 1, "Oto mój drugi post!", new DateTime(2021, 6, 21, 11, 2, 44, 0, DateTimeKind.Unspecified), false, "tytuł 2", 2 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "CategoryID", "Content", "Date", "IsPromoted", "Title", "UserID" },
                values: new object[] { 3, 1, "Oto mój trzeci post!", new DateTime(2021, 4, 11, 1, 21, 4, 0, DateTimeKind.Unspecified), false, "tytuł 3", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
