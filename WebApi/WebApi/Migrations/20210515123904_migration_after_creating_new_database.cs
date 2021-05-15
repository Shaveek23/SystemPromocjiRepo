using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class migration_after_creating_new_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    PostID = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Content = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                });

            migrationBuilder.CreateTable(
                name: "CommentLike",
                columns: table => new
                {
                    CommentLikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentLike", x => x.CommentLikeID);
                });

            migrationBuilder.CreateTable(
                name: "Newsletter",
                columns: table => new
                {
                    NewsletterID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletter", x => x.NewsletterID);
                });

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
                name: "PostLike",
                columns: table => new
                {
                    PostLikeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLike", x => x.PostLikeID);
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
                    IsPromoted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                });

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
                table: "Category",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { 1, "Polityka" },
                    { 2, "Stomatologia" },
                    { 3, "Kaczor Donald" }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentID", "Content", "DateTime", "PostID", "UserID" },
                values: new object[,]
                {
                    { 1, "tralalala ", new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, "tralalala pararara", new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, "tu ti tu rum tu tu", new DateTime(2021, 4, 13, 12, 30, 20, 0, DateTimeKind.Unspecified), 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonID", "Address", "City", "FirstName", "LastName" },
                values: new object[] { 1, "ul. Koszykowa 57A/7", "Warszawa", "Adam", "Nowak" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostID", "CategoryID", "Content", "Date", "IsPromoted", "Title", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Oto mój pierwszy post!", new DateTime(2021, 3, 11, 12, 23, 46, 0, DateTimeKind.Unspecified), false, "tytuł 1", 1 },
                    { 2, 1, "Oto mój drugi post!", new DateTime(2021, 6, 21, 11, 2, 44, 0, DateTimeKind.Unspecified), false, "tytuł 2", 2 },
                    { 3, 1, "Oto mój trzeci post!", new DateTime(2021, 4, 11, 1, 21, 4, 0, DateTimeKind.Unspecified), false, "tytuł 3", 3 }
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
                name: "Category");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CommentLike");

            migrationBuilder.DropTable(
                name: "Newsletter");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "PostLike");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
