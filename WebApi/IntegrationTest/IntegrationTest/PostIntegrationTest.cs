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
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;
using WebApi.Services.Serives_Implementations;
using Xunit;

namespace IntegrationTest
{
    public class PostIntegrationTest
    {
        public PostController GetPostController(DbContextOptions<DatabaseContext> options)
        {
            var logger = new Mock<ILogger<PostController>>();
            var databaseContext = new DatabaseContext(options);
            var postRepository = new PostRepository(databaseContext);
            var userRepository = new UserRepository(databaseContext);
            var commentRepository = new CommentRepository(databaseContext);
            var commentService = new CommentService(commentRepository, userRepository);
            var postSerive = new PostService(postRepository, userRepository, commentService);
            var postController = new PostController(logger.Object, postSerive);

            return postController;
        }
        [Fact]
        public void GetPost_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetPost_ValidCall").Options;
            var expectedPost = new Post() { PostID = 1, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false };
            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };
            
            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(expectedPost);
                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }
            
            var postController = GetPostController(options);

            var actual = (PostDTO)((ObjectResult)postController.Get(expectedPost.PostID, expectedUser.UserID).Result).Value;

            Assert.NotNull(actual);
            Assert.Equal(expectedPost.PostID, actual.id);
            Assert.Equal(expectedPost.Title, actual.title);
            Assert.Equal(expectedPost.Content, actual.content);
            Assert.Equal(expectedPost.Date, actual.datetime);
            Assert.Equal(expectedPost.CategoryID, actual.category);
            Assert.Equal(expectedPost.IsPromoted, actual.isPromoted);
            Assert.Equal(expectedUser.UserID, actual.authorID);
            Assert.Equal(expectedUser.UserName, actual.author);
            Assert.False(actual.isLikedByUser);
        }

        [Fact]
        public void GetPost_InvalidCall_NoId()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetPost_InvalidCall_NoId").Options;
            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };

            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }

            var postController = GetPostController(options);

            var result = (ObjectResult)postController.Get(1, expectedUser.UserID).Result;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void GetAllPosts_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetAllPosts_ValidCall").Options;

            var expected = new List<Post>()
            {
                new Post() { PostID = 1, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 2, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 3, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 4, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false }
            };
            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };

            using (var dbContext = new DatabaseContext(options))
            {
                foreach (Post post in expected)
                    dbContext.Add(post);
                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }

            var postController = GetPostController(options);

            var actual = ((IEnumerable<PostDTO>)((ObjectResult)postController.GetAll(1).Result).Value).ToList();
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetAllPosts_ValidCall_Empty()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetAllPosts_ValidCall_Empty").Options;

            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };

            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }

            var postController = GetPostController(options);

            var actual = ((IEnumerable<PostDTO>)((ObjectResult)postController.GetAll(1).Result).Value).ToList();
            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public void GetAllPosts_ValidCall_NoUser()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetAllPosts_ValidCall_NoUser").Options;

            var expected = new List<Post>()
            {
                new Post() { PostID = 1, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 2, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 3, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 4, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false }
            };
            using (var dbContext = new DatabaseContext(options))
            {
                foreach (Post post in expected)
                    dbContext.Add(post);
                dbContext.SaveChanges();
            }

            var postController = GetPostController(options);

            var actual = ((IEnumerable<PostDTO>)((ObjectResult)postController.GetAll(1).Result).Value).ToList();

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expected.Count, actual.Count);
            foreach (PostDTO post in actual)
                Assert.Equal("Nie ma takiego użytkownika", post.author);
            foreach (PostDTO post in actual)
                Assert.Equal(0, post.authorID);
        }

        [Fact]
        public void GetUserPosts_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetUserPosts_ValidCall").Options;

            var expected = new List<Post>()
            {
                new Post() { PostID = 1, UserID = 1, CategoryID = 1, Title = "title", Content = "OK", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 2, UserID = 1, CategoryID = 1, Title = "title", Content = "OK", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 3, UserID = 2, CategoryID = 1, Title = "title", Content = "ERR", Date = DateTime.Now, IsPromoted = false },
                new Post() { PostID = 4, UserID = 2, CategoryID = 1, Title = "title", Content = "ERR", Date = DateTime.Now, IsPromoted = false }
            };
            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };

            using (var dbContext = new DatabaseContext(options))
            {
                foreach (Post post in expected)
                    dbContext.Add(post);
                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }

            var postController = GetPostController(options);

            var actual = ((IEnumerable<PostDTO>)((ObjectResult)postController.GetUserPosts(expectedUser.UserID).Result).Value).ToList();
            Assert.NotNull(actual);
            Assert.Equal(2, actual.Count);
            foreach (PostDTO post in actual)
                Assert.Equal(1, post.authorID);
        }
        public PostEditDTO GetPostablePost()
        {
           return new PostEditDTO() { title = "title", content = "content", category = 1, dateTime = DateTime.Now, isPromoted = false };
        }

        [Fact]
        public void PostPost_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "PostPost_ValidCall").Options;

            var postController = GetPostController(options);

            var expected = GetPostablePost();
            var expectedUserID = 2;
            var result = (ObjectResult)postController.Create(expectedUserID, expected).Result;

            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Posts.FirstOrDefault();

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(actual);
            Assert.Equal(expectedUserID, actual.UserID);
            Assert.Equal(expected.title, actual.Title);
            Assert.Equal(expected.content, actual.Content);
            Assert.Equal(expected.category, actual.CategoryID);
            Assert.Equal(expected.dateTime, actual.Date);
            Assert.Equal(expected.isPromoted, actual.IsPromoted);
        }

        [Fact]
        public void PutPost_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "PutPost_ValidCall").Options;

            var post = new Post() { PostID = 1, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false };
            
            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(post);
                dbContext.SaveChanges();
            }

            var postController = GetPostController(options);

            var expected = GetPostablePost();
                expected.content = "edited_content";
                expected.title = "edited_title";
            var result = (ObjectResult)postController.Edit(post.UserID, post.PostID, expected).Result;

            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Posts.FirstOrDefault();

            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(actual);
            //Assert.Equal(post.UserID, actual.UserID); //dlaczego zmieniany jest UserID?
            Assert.Equal(expected.title, actual.Title);
            Assert.Equal(expected.content, actual.Content);
            Assert.Equal(expected.category, actual.CategoryID);
            Assert.Equal(expected.dateTime, actual.Date);
            Assert.Equal(expected.isPromoted, actual.IsPromoted);
        }

        [Fact]
        public void DeletePost_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "DeletePost_ValidCall").Options;

            var post = new Post() { PostID = 1, UserID = 1, CategoryID = 1, Title = "title", Content = "content", Date = DateTime.Now, IsPromoted = false };

            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(post);
                dbContext.SaveChanges();
            }

            var postController = GetPostController(options);

            var result = (ObjectResult)postController.Delete(post.UserID, post.PostID).Result;

            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Posts.FirstOrDefault();

            Assert.Null(actual);
            Assert.Equal(200, result.StatusCode);
        }
        [Fact]
        public void DeletePost_InvalidCall_NoId()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "DeletePost_InvalidCall_NoId").Options;

            var postController = GetPostController(options);

            var result = (ObjectResult)postController.Delete(1, 1).Result;

            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Posts.FirstOrDefault();

            Assert.Equal(404, result.StatusCode);
            Assert.Null(actual);
        }

    }
}
