using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class RepairPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 4, 1, 10, 0, 47, 511, DateTimeKind.Local).AddTicks(7697));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 4, 1, 10, 0, 47, 514, DateTimeKind.Local).AddTicks(6888));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 4, 1, 10, 0, 47, 514, DateTimeKind.Local).AddTicks(6953));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 3, 30, 22, 21, 46, 588, DateTimeKind.Local).AddTicks(5085));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2021, 3, 30, 22, 21, 46, 590, DateTimeKind.Local).AddTicks(8303));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostID",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2021, 3, 30, 22, 21, 46, 590, DateTimeKind.Local).AddTicks(8366));
        }
    }
}
