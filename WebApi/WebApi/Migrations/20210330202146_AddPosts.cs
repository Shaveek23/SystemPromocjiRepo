using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class AddPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    Address = table.Column<string>(maxLength: 30, nullable: false),
                    City = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Localization = table.Column<string>(maxLength: 50, nullable: false),
                    ShopName = table.Column<string>(maxLength: 50, nullable: true),
                    IsPromoted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonID", "Address", "City", "FirstName", "LastName" },
                values: new object[] { 1, "ul. Koszykowa 57A/7", "Warszawa", "Adam", "Nowak" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "CategoryID", "Content", "Date", "IsPromoted", "Localization", "ShopName", "Title", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Oto mój pierwszy post!", new DateTime(2021, 3, 30, 22, 21, 46, 588, DateTimeKind.Local).AddTicks(5085), false, "Warszawa", "Sklep1", "tytuł 1", 1 },
                    { 2, 1, "Oto mój drugi post!", new DateTime(2021, 3, 30, 22, 21, 46, 590, DateTimeKind.Local).AddTicks(8303), false, "Kraków", "Sklep2", "tytuł 2", 2 },
                    { 3, 1, "Oto mój trzeci post!", new DateTime(2021, 3, 30, 22, 21, 46, 590, DateTimeKind.Local).AddTicks(8366), false, "Poznań", "Sklep3", "tytuł 3", 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
