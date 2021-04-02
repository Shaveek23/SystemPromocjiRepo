using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localization",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ShopName",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 3, 11, 12, 23, 46, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 6, 21, 11, 2, 44, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 4, 11, 1, 21, 4, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1,
                columns: new[] { "Date", "Localization", "ShopName" },
                values: new object[] { new DateTime(2021, 3, 30, 22, 21, 46, 588, DateTimeKind.Local).AddTicks(5085), "Warszawa", "Sklep1" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 2,
                columns: new[] { "Date", "Localization", "ShopName" },
                values: new object[] { new DateTime(2021, 3, 30, 22, 21, 46, 590, DateTimeKind.Local).AddTicks(8303), "Kraków", "Sklep2" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 3,
                columns: new[] { "Date", "Localization", "ShopName" },
                values: new object[] { new DateTime(2021, 3, 30, 22, 21, 46, 590, DateTimeKind.Local).AddTicks(8366), "Poznań", "Sklep3" });
        }
    }
}
