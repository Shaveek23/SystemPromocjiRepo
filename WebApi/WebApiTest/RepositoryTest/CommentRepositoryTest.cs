﻿using Microsoft.EntityFrameworkCore;
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
        void SeedComment(DatabaseContext dbContext)
        {
            dbContext.Add(new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "test" });
            dbContext.Add(new Comment() { CommentID = 2, UserID = 2, PostID = 2, DateTime = DateTime.Today, Content = "test2" });
            dbContext.Add(new Comment() { CommentID = 3, UserID = 3, PostID = 3, DateTime = DateTime.Today, Content = "test3" });

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
                int expectedID = -1;
                // var expected = dbContext.Comments.Where(x => x.CommentID == expectedID).FirstOrDefault();

                var cls = new CommentRepository(dbContext);

                //TODO: Czy tu raczej nie powinein wyłapyywac 
                Assert.Throws<Exception>(() => cls.GetById(expectedID));

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


                var expected = dbContext.Comments.ToList();

                var cls = new CommentRepository(dbContext);
                var actual = cls.GetAll();

                Assert.True(actual != null);
                Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));

            }
        }
        [Fact]
        public void GetAll_InValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "GetAll_InValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);


                var expected = dbContext.Comments.ToList();

                var cls = new CommentRepository(dbContext);
                var actual = cls.GetAll();

                Assert.Throws<Exception>(() => cls.GetAll());

            }

        }
        /// <summary>
        /// to jest zle ale nw jak mamy pobrac wszytskich ktorzy polubili
        /// </summary>
        [Fact]
        async public void GetLikedUsers_ValidCall()
        {
            //var options = new DbContextOptionsBuilder<DatabaseContext>()
            // .UseInMemoryDatabase(databaseName: "GetLikedUsers_ValidCall").Options;
            //using (var dbContext = new DatabaseContext(options))
            //{
            //    SeedComment(dbContext);
            //    int expectedID = 1;

            //    var expected = dbContext.Comments.Select(x=>x.UserID).ToList();

            //    var cls = new CommentRepository(dbContext);
            //    var actual = await cls.GetLikedUsersAsync(expectedID);

            //    Assert.True(actual != null);
            //    Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));

            //}
            throw new NotImplementedException();
        }
        [Fact]
        public void GetLikedUsers_InValidCall()
        {


            throw new NotImplementedException();
        }
        [Fact]
        public void AddComment_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "AddComment_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
                int initLength = dbContext.Comments.Count();
                var expected = new Comment() { CommentID = 4, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "testNowy" };

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
        [Fact]
        public void AddComment_InValidCall()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void DeleteComment_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "DeleteComment_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
                int initLength = dbContext.Comments.Count();
                int commentToDeleteId = 1;


                var cls = new CommentRepository(dbContext);

                cls.DeleteComment(commentToDeleteId);
                

               
                Assert.Equal(dbContext.Comments.Count(), initLength - 1);



            }



        }
        [Fact]
        public void DeleteComment_InValidCall()
        {

            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "DeleteComment_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);
                int initLength = dbContext.Comments.Count();
                int commentToDeleteId = -1;


                var cls = new CommentRepository(dbContext);

                



                Assert.Throws<Exception>(()=> cls.DeleteComment(commentToDeleteId));



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


                string expectedText = "zedytowany tekst";

                var expected = new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = expectedText };
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
        public void EditComment_InValidCall()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void EditLikeOnComment_ValidCall()
        {
            throw new NotImplementedException();
        }
        [Fact]
        public void EditLikeOnComment_InValidCall()
        {
            throw new NotImplementedException();
        }
    }
}