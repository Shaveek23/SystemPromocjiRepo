using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Posts");
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPromoted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                 name: "Posts");
        }
    }
}
