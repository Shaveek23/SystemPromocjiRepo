using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using WebApi.Controllers;
using WebApi.Database;
using WebApi.Database.Repositories.Implementations;
using WebApi.Models.DTO;
using WebApi.Models.POCO;
using WebApi.Services.Serives_Implementations;
using Xunit;

namespace IntegrationTest.IntegrationTest
{
    public class CommentIntegrationTest
    {
        public CommentController GetCommentController(DbContextOptions<DatabaseContext> options)
        {
            var logger = new Mock<ILogger<CommentController>>();
            var databaseContext = new DatabaseContext(options);
            var commentRepository = new CommentRepository(databaseContext);
            var userRepository = new UserRepository(databaseContext);
            var commentService = new CommentService(commentRepository, userRepository);
            var commentController = new CommentController(logger.Object, commentService);
            return commentController;
        }
        [Fact]

        public void GetAllComments_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "GetAllComments_ValidCall").Options;

            var expected = new List<Comment>()
            {
               new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test" },
               new Comment() { CommentID = 2, UserID = 1, PostID = 2, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test2" },
               new Comment() { CommentID = 3, UserID = 2, PostID = 3, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test3" }
             };

            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };

            using (var dbContext = new DatabaseContext(options))
            {
                foreach (Comment comment in expected)
                    dbContext.Add(comment);
                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }

            var commentController = GetCommentController(options);
            var actual = ((IEnumerable<CommentDTOOutput>)((ObjectResult)commentController.GetAll(expectedUser.UserID).Result).Value).ToList();
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);

        }
        [Fact]
        public void GetAllComments_ValidCallEmpty()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "GetAllComments_ValidCallEmpty").Options;



            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };

            using (var dbContext = new DatabaseContext(options))
            {

                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }

            var commentController = GetCommentController(options);
            var actual = ((IEnumerable<CommentDTOOutput>)((ObjectResult)commentController.GetAll(expectedUser.UserID).Result).Value).ToList();
            Assert.NotNull(actual);
            Assert.Empty(actual);


        }

        [Fact]
        public void GetCommentById_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "GetCommentById_ValidCall").Options;
            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };

            var expectedComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, Content = "test" };

            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(expectedComment);
                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }
            var commentController = GetCommentController(options);
            var actual = ((CommentDTOOutput)((ObjectResult)commentController.GetById(expectedComment.CommentID, expectedUser.UserID).Result).Value);
            Assert.NotNull(actual);
            Assert.Equal(expectedComment.CommentID, actual.id);
            Assert.Equal(expectedComment.Content, actual.content);
            Assert.Equal(expectedComment.PostID, actual.postId);
            Assert.Equal(expectedComment.UserID, actual.authorID);


        }

        [Fact]
        public void GetCommentById_NoId()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
              .UseInMemoryDatabase(databaseName: "GetCommentById_NoId").Options;
            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };



            using (var dbContext = new DatabaseContext(options))
            {

                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }
            var commentController = GetCommentController(options);
            var actual = (ObjectResult)commentController.GetById(1, expectedUser.UserID).Result;
            Assert.Equal(404, actual.StatusCode);




        }
        [Fact]
        public void GetAllUsersComments_ValidCall()
        {

        }
        [Fact]
        public void AddComment_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
             .UseInMemoryDatabase(databaseName: "AddComment_ValidCall").Options;
            var commentController = GetCommentController(options);
            var expectedUserID = 1;
            var expectedComment = new CommentDTONew() { PostID = 1, Content = "test" };
            var result = commentController.AddComment(expectedComment, expectedUserID).Result.Value;

            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Comments.FirstOrDefault();


            Assert.NotNull(actual);
            Assert.Equal(expectedComment.PostID, actual.PostID);
            Assert.Equal(expectedComment.Content, actual.Content);
        }
        [Fact]
        public void DeleteComment_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
           .UseInMemoryDatabase(databaseName: "DeleteComment_ValidCall").Options;
            var commentController = GetCommentController(options);
            var expectedComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, Content = "test" };

            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(expectedComment);
                dbContext.SaveChanges();
            }
            var result = (bool)((ObjectResult)commentController.DeleteComment(expectedComment.CommentID, expectedComment.UserID).Result).Value;
            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Comments.FirstOrDefault();

            Assert.Null(actual);
            Assert.True(result);



        }
        [Fact]
        public void DeleteComment_NoId()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
           .UseInMemoryDatabase(databaseName: "DeleteComment_NoID").Options;
            var commentController = GetCommentController(options);
            var expectedComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, Content = "test" };


            var result = (ObjectResult)commentController.DeleteComment(expectedComment.CommentID, expectedComment.UserID).Result;
            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Comments.FirstOrDefault();
            Assert.Equal(400, result.StatusCode);
            Assert.Null(actual);




        }
        [Fact]
        public void EditComment_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
          .UseInMemoryDatabase(databaseName: "EditComment_ValidCall").Options;
            var commentController = GetCommentController(options);
            var expectedComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, Content = "test" };

            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(expectedComment);
                dbContext.SaveChanges();
            }
            var expected = new CommentDTOEdit()
            { Content = "zmienione test" };
            var result = (bool)((ObjectResult)commentController.EditComment(expectedComment.CommentID, expectedComment.UserID, expected).Result.Result).Value;
            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Comments.FirstOrDefault();
            Assert.NotNull(actual);
            Assert.True(result);
            Assert.Equal(expected.Content, actual.Content);

        }
      
        [Fact]
        public void GetCommentLikes_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
        .UseInMemoryDatabase(databaseName: "GetCommentLikes_ValidCall").Options;
            var commentController = GetCommentController(options);
            var expectedComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, Content = "test" };
            var expectedLikes = new List<CommentLike>
            {
              new CommentLike(){ UserID=1,CommentID=1,CommentLikeID=1} ,
             new CommentLike(){ UserID=2,CommentID=1,CommentLikeID=2}

            };
            var otherCommentsLike = new CommentLike() { UserID = 1, CommentID = 2, CommentLikeID = 3 };
            using (var dbContext = new DatabaseContext(options))
            {
                foreach (var like in expectedLikes)
                {
                    dbContext.Add(like);
                }
                dbContext.Add(otherCommentsLike);
                dbContext.Add(expectedComment);
                dbContext.SaveChanges();
            }

            var actual = ((IQueryable<LikerDTO>)((ObjectResult)commentController.GetCommentLikes(expectedComment.CommentID).Result).Value).ToList<LikerDTO>();
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expectedLikes.Count, actual.Count);

        }
        [Fact]
        public void GetCommentLikes_Empty()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
        .UseInMemoryDatabase(databaseName: "GetCommentLikes_Empty").Options;
            var commentController = GetCommentController(options);
            var expectedComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, Content = "test" };
          
    
            using (var dbContext = new DatabaseContext(options))
            {
             
                dbContext.Add(expectedComment);
                dbContext.SaveChanges();
            }

            var actual = ((IQueryable<LikerDTO>)((ObjectResult)commentController.GetCommentLikes(expectedComment.CommentID).Result).Value).ToList<LikerDTO>();
            Assert.NotNull(actual);
            Assert.Empty(actual);
            

        }
        [Fact]
        public void EditLikeStatus_ValidCallRemoveOne()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
       .UseInMemoryDatabase(databaseName: "EditLikeStatus_ValidCallRemoveOne").Options;
            var commentController = GetCommentController(options);
            var expectedComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, Content = "test" };
            var expectedLikes = new List<CommentLike>
            {
              new CommentLike(){ UserID=1,CommentID=1,CommentLikeID=1} ,
             new CommentLike(){ UserID=2,CommentID=1,CommentLikeID=2}

            };
            var like = new LikeDTO() { like = true };
            var dbContext = new DatabaseContext(options);
            foreach (var l in expectedLikes)
            {
                dbContext.Add(l);
            }
            dbContext.Add(expectedComment);
            dbContext.SaveChanges();

            //usuwam pierwszy
            var result = commentController.EditLikeStatus(expectedComment.UserID, expectedLikes.ElementAt(0).CommentLikeID, like);
            var actual = dbContext.CommentLikes.Where(x => x.CommentID == expectedComment.CommentID).ToList();

            Assert.NotNull(actual);
            Assert.Equal(expectedLikes.Count - 1, actual.Count);

        }
        [Fact]
        public void EditLikeStatus_ValidCallAddOne()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
       .UseInMemoryDatabase(databaseName: "EditLikeStatus_ValidCallAddOne").Options;
            var commentController = GetCommentController(options);
            var expectedComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, Content = "test" };
            var expectedLikes = new List<CommentLike>
            {
              new CommentLike(){ UserID=1,CommentID=1,CommentLikeID=1} ,
             new CommentLike(){ UserID=2,CommentID=1,CommentLikeID=2}

            };
            var like = new LikeDTO() { like = true };
            var dbContext = new DatabaseContext(options);
            foreach (var l in expectedLikes)
            {
                dbContext.Add(l);
            }
            dbContext.Add(expectedComment);
            dbContext.SaveChanges();

            //dodaje nowy
            var result = commentController.EditLikeStatus(3, expectedComment.CommentID, like);
            var actual = dbContext.CommentLikes.Where(x => x.CommentID == expectedComment.CommentID).ToList();

            Assert.NotNull(actual);
            Assert.Equal(expectedLikes.Count + 1, actual.Count);

        }
    }
}
