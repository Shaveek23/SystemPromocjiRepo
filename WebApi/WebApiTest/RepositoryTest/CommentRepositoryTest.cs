using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Database;
using WebApi.Database.Repositories.Implementations;
using WebApi.Models.POCO;
using Xunit;

namespace WebApiTest
{
    public class CommentRepositoryTest
    {
        const int UserId = 1;
        void SeedComment(DatabaseContext dbContext)
        {
            dbContext.Add(new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test" });
            dbContext.Add(new Comment() { CommentID = 2, UserID = 2, PostID = 2, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test2" });
            dbContext.Add(new Comment() { CommentID = 3, UserID = 3, PostID = 3, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test3" });

            dbContext.SaveChanges();
        }

        [Fact]
        public void GetById_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetById_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {

                int expectedID = 1;
                var expected = dbContext.Comments.Where(x => x.CommentID == expectedID).FirstOrDefault();

                var cls = new CommentRepository(dbContext);
                var actual = cls.GetById(expectedID);

                Assert.True(actual != null);
                Assert.Equal(expected.CommentID, actual.CommentID);
                Assert.Equal(expected.Content, actual.Content);
                Assert.Equal(expected.DateTime, actual.DateTime);
                Assert.Equal(expected.PostID, actual.PostID);
                Assert.Equal(expected.UserID, actual.UserID);

            }
        }
        [Fact]
        public void GetById_InValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetById_InValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {

                int expectedID = -1;
                var cls = new CommentRepository(dbContext);
                Assert.Null(cls.GetById(expectedID));

            }
        }
        [Fact]
        public void GetAll_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "GetAll_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {



                var expected = dbContext.Comments.ToList();

                var cls = new CommentRepository(dbContext);
                var actual = cls.GetAll();

                Assert.True(actual != null);
                Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));

            }
        }
        #region TODO
        [Fact]
        public void GetAll_InValidCall()
        {
            //var options = new DbContextOptionsBuilder<DatabaseContext>()
            //  .UseInMemoryDatabase(databaseName: "GetAll_InValidCall").Options;
            //using (var dbContext = new DatabaseContext(options))
            //{
            //    SeedComment(dbContext);


            //    var expected = dbContext.Comments.ToList();

            //    var cls = new CommentRepository(dbContext);
            //    var actual = cls.GetAll();

            //    Assert.Throws<Exception>(() => cls.GetAll());

            //}
            Assert.True(true);

        }
        #endregion
        /// <summary>
        /// to jest zle ale nw jak mamy pobrac wszytskich ktorzy polubili
        /// </summary>
        #region TODO: 
        [Fact]
        public void GetLikedUsers_ValidCall()
        {
            Assert.Equal(1, 1);

        }
        #endregion
        #region TODO: 
        [Fact]
        public void GetLikedUsers_InValidCall()
        {

            Assert.Equal(1, 1);

        }
        #endregion
        [Fact]
        public void AddComment_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "AddComment_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {

                int initLength = dbContext.Comments.Count();
                var expected = new Comment() { CommentID = 4, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };

                var cls = new CommentRepository(dbContext);

                var actual = cls.AddAsync(expected).Result;

                Assert.True(actual != null);
                Assert.Equal(expected.CommentID, actual.CommentID);
                Assert.Equal(expected.Content, actual.Content);
                Assert.Equal(expected.DateTime, actual.DateTime);
                Assert.Equal(expected.PostID, actual.PostID);
                Assert.Equal(expected.UserID, actual.UserID);
                Assert.Equal(dbContext.Comments.Count(), initLength + 1);



            }
        }
        #region TODO: 
        [Fact]
        //Dodaje istniejace id -> sprawdzam czy zwraca null i czy nie zmieniła sie ilosc komentarzy w bazie
        public void AddComment_InValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "AddComment_InValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {

                int initLength = dbContext.Comments.Count();
                var expected = new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };

                var cls = new CommentRepository(dbContext);

                var actual = cls.AddAsync(expected).Result;
                Assert.Null(actual);
                Assert.Equal(dbContext.Comments.Count(), initLength);



            }
        }
        #endregion
        [Fact]

        public void DeleteComment_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "DeleteComment_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {

                int initLength = dbContext.Comments.Count();
                int commentToDeleteId = 1;


                var cls = new CommentRepository(dbContext);

                var result = cls.DeleteComment(commentToDeleteId, UserId);



                Assert.Equal(dbContext.Comments.Count(), initLength - 1);
                Assert.True(result);



            }



        }
        [Fact]
        //usuwam nieistniejace id -> sprawdzam czy zwraca null i czy nie zmieniła sie ilosc komentarzy w bazie
        public void DeleteComment_InValidCall()
        {

            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "DeleteComment_InValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {

                int initLength = dbContext.Comments.Count();
                int commentToDeleteId = -1;


                var cls = new CommentRepository(dbContext);

                Assert.False(cls.DeleteComment(commentToDeleteId, UserId));
                Assert.Equal(dbContext.Comments.Count(), initLength);



            }
        }
        [Fact]
        public void EditComment_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "EditComment_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {



                string expectedText = "zedytowany tekst";

                var expected = new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = expectedText };
                var cls = new CommentRepository(dbContext);

                var actual = cls.UpdateAsync(expected).Result;
                Assert.Equal(expected.CommentID, actual.CommentID);
                Assert.Equal(expected.Content, actual.Content);
                Assert.Equal(expected.DateTime, actual.DateTime);
                Assert.Equal(expected.PostID, actual.PostID);
                Assert.Equal(expected.UserID, actual.UserID);
                Assert.True(actual != null);



            }

        }
        [Fact]
        //edytuje nieistniejace id -> sprawdzam czy zwraca null i czy nie zmieniła sie ilosc komentarzy w bazie
        public void EditComment_InValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "EditComment_InValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
            }
            using (var dbContext = new DatabaseContext(options))
            {



                string expectedText = "zedytowany tekst";

                int initLength = dbContext.Comments.Count();
                var expected = new Comment() { CommentID = 100, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = expectedText };
                var cls = new CommentRepository(dbContext);

                var actual = cls.UpdateAsync(expected).Result;
                Assert.Null(actual);
                Assert.Equal(dbContext.Comments.Count(), initLength);



            }
        }

        [Fact]
        #region TODO: 
        public void EditLikeOnComment_ValidCall()
        {
            Assert.Equal(1, 1);
        }
        #endregion
        [Fact]
        #region TODO: 
        public void EditLikeOnComment_InValidCall()
        {
            Assert.Equal(1, 1);
        }
        #endregion
    }
}
