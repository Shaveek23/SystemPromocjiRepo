using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class updatePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Localization",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ShopName",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Localization",
                table: "Posts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShopName",
                table: "Posts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "CategoryID", "Content", "Date", "IsPromoted", "Localization", "ShopName", "Title", "UserID" },
                values: new object[] { 1, 1, "Oto mój pierwszy post!", new DateTime(2021, 3, 30, 22, 21, 46, 588, DateTimeKind.Local).AddTicks(5085), false, "Warszawa", "Sklep1", "tytuł 1", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "CategoryID", "Content", "Date", "IsPromoted", "Localization", "ShopName", "Title", "UserID" },
                values: new object[] { 2, 1, "Oto mój drugi post!", new DateTime(2021, 3, 30, 22, 21, 46, 590, DateTimeKind.Local).AddTicks(8303), false, "Kraków", "Sklep2", "tytuł 2", 2 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "CategoryID", "Content", "Date", "IsPromoted", "Localization", "ShopName", "Title", "UserID" },
                values: new object[] { 3, 1, "Oto mój trzeci post!", new DateTime(2021, 3, 30, 22, 21, 46, 590, DateTimeKind.Local).AddTicks(8366), false, "Poznań", "Sklep3", "tytuł 3", 3 });
        }
    }
}
