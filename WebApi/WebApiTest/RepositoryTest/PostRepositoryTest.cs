using System;
using WebApi.Models.POCO;
using Xunit;
using WebApi.Database;
using Microsoft.EntityFrameworkCore;
using WebApi.Database.Repositories.Implementations;
using Moq;
using Autofac.Extras.Moq;
using WebApi.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebApi.Models.DTO.PostDTOs;

namespace WebApiTest
{
    public class PostRepositoryTest
    {
        void SeedPost(DatabaseContext dbContext)
        {
            dbContext.Add(new Post { PostID = 1, UserID = 1, CategoryID = 1, Title = "Title1", Content = "Content1 ", Date = DateTime.Now, IsPromoted = false, Localization = "Warszawa", ShopName = "U Romka" });
            dbContext.Add(new Post { PostID = 12, UserID = 8, CategoryID = 3, Date = DateTime.UtcNow, IsPromoted = true, Localization = "War sza w a", ShopName = "U Tomka" });
            dbContext.Add(new Post { PostID = 2, UserID = 5, CategoryID = 5, Title = "Title32321", Date = DateTime.Now, IsPromoted = false, Localization = "Krakówarszawa", ShopName = "U Pawła" });
            dbContext.SaveChanges();
        }
        [Fact]
        public void GetPostByIdAsync_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "GetPostByIdAsync_ValidCall").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                SeedPost(dbContext);
                int expectedID = 1;
                var expected = dbContext.Posts.Where(x => x.PostID == expectedID).FirstOrDefault();

                var cls = new PostRepository(dbContext);
                var actual = cls.GetPostByIdAsync(expectedID);

                Assert.True(actual != null);
                Assert.Equal(expected.PostID, actual.Result.PostID);
                Assert.Equal(expected.UserID, actual.Result.UserID);
                Assert.Equal(expected.CategoryID, actual.Result.CategoryID);
                Assert.Equal(expected.Date, actual.Result.Date);
                Assert.Equal(expected.Title, actual.Result.Title);
                Assert.Equal(expected.Content, actual.Result.Content);
                Assert.Equal(expected.Localization, actual.Result.Localization);
                Assert.Equal(expected.ShopName, actual.Result.ShopName);
                Assert.Equal(expected.IsPromoted, actual.Result.IsPromoted);

            }
        }

        [Fact]
        public void GetPostByIdAsync_InvalidCall_NoId()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "GetPostByIdAsync_InvalidCall_NoId").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                SeedPost(dbContext);
                int expectedID = 0;
                var expected = dbContext.Posts.Where(x => x.PostID == expectedID).FirstOrDefault();

                var cls = new PostRepository(dbContext);
                var actual = cls.GetPostByIdAsync(expectedID);

                Assert.True(actual != null);
                Assert.True(actual.Result == null);
            }
        }

        [Fact]
        public void EditPostAsync_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "EditPostAsync_ValidCall").Options;

            var editBody = new PostEditDTO
            {
                title = "newtitle",
                content = "newcontent",
                category = 5,
                dateTime = DateTime.UtcNow,
                isPromoted = true
            };

            using (var dbContext = new DatabaseContext(options))
            {
                SeedPost(dbContext);

                int changedPostID = 1;


                var expected = dbContext.Posts.Where(x => x.PostID == changedPostID).FirstOrDefault();
                expected.Title = editBody.title;
                expected.Content = editBody.content;
                expected.CategoryID = editBody.category;
                expected.Date = editBody.dateTime;
                expected.IsPromoted = editBody.isPromoted;



                var cls = new PostRepository(dbContext);
                var actual = cls.EditPostAsync(changedPostID, editBody);

                Assert.True(actual != null);
                Assert.Equal(expected.PostID, actual.Result.PostID);
                Assert.Equal(expected.UserID, actual.Result.UserID);
                Assert.Equal(expected.CategoryID, actual.Result.CategoryID);
                Assert.Equal(expected.Date, actual.Result.Date);
                Assert.Equal(expected.Title, actual.Result.Title);
                Assert.Equal(expected.Content, actual.Result.Content);
                Assert.Equal(expected.Localization, actual.Result.Localization);
                Assert.Equal(expected.ShopName, actual.Result.ShopName);
                Assert.Equal(expected.IsPromoted, actual.Result.IsPromoted);
            }
        }


        //TODO: Repair

        //[Fact]
        //public void EditPostAsync_InvalidCall_noId()
        //{
        //    var options = new DbContextOptionsBuilder<DatabaseContext>()
        //        .UseInMemoryDatabase(databaseName: "EditPostAsync_InvalidCall_noId").Options;

        //    var editBody = new PostEditDTO
        //    {
        //        title = "newtitle",
        //        content = "newcontent",
        //        category = 5,
        //        dateTime = DateTime.UtcNow,
        //        isPromoted = true
        //    };

        //    using (var dbContext = new DatabaseContext(options))
        //    {
        //        SeedPost(dbContext);

        //        int changedPostID = 0;

        //        var cls = new PostRepository(dbContext);
        //        var actual = cls.EditPostAsync(changedPostID, editBody);

        //        Assert.True(actual != null);
        //        Assert.True(actual.Result == null);
        //    }
        //}
    }
}
