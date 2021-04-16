using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    UserEmail = table.Column<string>(maxLength: 30, nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsEnterprenuer = table.Column<bool>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });


            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserID", "Active", "IsAdmin", "IsEnterprenuer", "IsVerified", "Timestamp", "UserEmail", "UserName" },
                values: new object[,]
                {
                    { 1, true, false, true, true, new DateTime(2021, 4, 16, 22, 30, 20, 0, DateTimeKind.Unspecified), "jaroslaw@kaczyslaw.pl", "jaroslawpolsezbaw" },
                    { 2, true, false, false, false, new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified), "antoni@kaczyslaw.pl", "tobrzozawybuchla" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2021, 3, 30, 15, 17, 20, 418, DateTimeKind.Local).AddTicks(6952));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2021, 3, 30, 15, 17, 20, 422, DateTimeKind.Local).AddTicks(2920));

            migrationBuilder.UpdateData(
                table: "Comment",
                keyColumn: "CommentID",
                keyValue: 3,
                column: "DateTime",
                value: new DateTime(2021, 3, 30, 15, 17, 20, 422, DateTimeKind.Local).AddTicks(2972));
        }
    }
}
