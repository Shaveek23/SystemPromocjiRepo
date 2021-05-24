using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
using WebApi.Models.POCO;
using WebApi.Services;
using WebApi.Services.Serives_Implementations;
using Xunit;

namespace WebApiTest.ServiceTest
{
    public class CommentServiceTest
    {

        const int UserId = 1;
        List<Comment> comments = new List<Comment>
        {
            new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime =  new DateTime(2008, 3, 1, 7, 0, 0), Content = "test" },
             new Comment() { CommentID = 2, UserID = 2, PostID = 2, DateTime =  new DateTime(2008, 3, 1, 7, 0, 0), Content = "test2" },
            new Comment() { CommentID = 3, UserID = 3, PostID = 3, DateTime =  new DateTime(2008, 3, 1, 7, 0, 0), Content = "test3" }
        };

        User mockUser = new User
        {
            UserID = UserId,
            UserName = "Testowy User",
            UserEmail = "user@test.com",
            Timestamp = DateTime.Now,
            Active = true,
            IsAdmin = false,
            IsEnterprenuer = false,
            IsVerified = true
        };




        [Fact]

        public void GetById_ValidCall()
        {
            // arrange:
            int expectedID = 1;
            var expected = comments.Where(x => x.CommentID == expectedID).FirstOrDefault();
            List<CommentLike> likes = new List<CommentLike>();
            likes.Add(new CommentLike { CommentID = expected.CommentID, UserID = mockUser.UserID, CommentLikeID = 1 });

            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.GetById(expectedID)).Returns(new ServiceResult<Comment>(expected));
            mockICommentRepository.Setup(x => x.GetLikes(expectedID)).Returns(new ServiceResult<IQueryable<CommentLike>>(likes.AsQueryable<CommentLike>()));

            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetById(mockUser.UserID)).Returns(new ServiceResult<User>(mockUser));

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);

            // act:
            var actual = commentService.GetById(expectedID, UserId).Result;


            Assert.True(actual != null);
            Assert.Equal(expected.CommentID, actual.id);
            Assert.Equal(expected.Content, actual.content);
            Assert.Equal(expected.DateTime, actual.date);
            Assert.Equal(expected.PostID, actual.postId);
            Assert.Equal(mockUser.UserID, actual.authorID);
            Assert.Equal(mockUser.UserName, actual.authorName);

        }

        [Fact]
        public void GetById_InValidCall()
        {
            // arrange:
            int expectedID = 0;
            var expected = comments.Where(x => x.CommentID == expectedID).FirstOrDefault();

            List<CommentLike> likes = new List<CommentLike>();
            likes.Add(new CommentLike { CommentID = expectedID, UserID = mockUser.UserID, CommentLikeID = 1 });

            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.GetLikes(expectedID)).Returns(new ServiceResult<IQueryable<CommentLike>>(likes.AsQueryable<CommentLike>()));
            mockICommentRepository.Setup(x => x.GetById(expectedID)).Returns(new ServiceResult<Comment>(null, System.Net.HttpStatusCode.BadRequest, "Something went wrong"));

            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetById(mockUser.UserID)).Returns(new ServiceResult<User>(mockUser));

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);

            // act:
            var actual = commentService.GetById(expectedID, UserId);


            Assert.Null(actual.Result);
            Assert.Equal(400, (int)actual.Code);
            Assert.Equal("Something went wrong", actual.Message);

        }

        [Fact]
        public void GetAll_ValidCall()
        {
            var expected = comments;
            List<CommentLike> likes = new List<CommentLike>();
            likes.Add(new CommentLike { CommentID = 0, UserID = mockUser.UserID, CommentLikeID = 1 });

            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.GetAll()).Returns(new ServiceResult<IQueryable<Comment>>(expected.AsQueryable()));
            mockICommentRepository.Setup(x => x.GetLikes(It.IsAny<int>())).Returns(new ServiceResult<IQueryable<CommentLike>>(likes.AsQueryable<CommentLike>()));

            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(new ServiceResult<User>(mockUser));

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);

            var actual = commentService.GetAll(UserId).Result.ToList();


            Assert.True(expected.All(item => actual.Any(actualItem => item.CommentID == actualItem.id &&
              item.UserID == actualItem.authorID && item.Content == actualItem.content &&
              item.PostID == actualItem.postId)));

        }

        [Fact]
        #region TODO
        public void GetAll_InValidCall()
        {
            Assert.True(true);


        }
        #endregion
        [Fact]
        #region TODO
        public void GetLikedUsers_ValidCall()
        {

            Assert.True(true);
        }
        #endregion
        [Fact]
        #region TODO

        public void GetLikedUsers_InValidCall()
        {



            Assert.True(true);
        }
        #endregion
        [Fact]


        public void AddComment_ValidCall()
        {

            int initLength = comments.Count();
            var expected = comments;

            var newCommentDTO = new CommentDTONew() { PostID = 1, Content = "testNowy" };
            var newComment = new CommentDTOOutput() { id = 4, authorID = 1, postId = 1, date = new DateTime(2008, 3, 1, 7, 0, 0), content = "testNowy" };
            expected.Add(new Comment { CommentID = 4, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" });
            var mockICommentRepository = new Mock<ICommentRepository>();

            mockICommentRepository.Setup(x => x.AddAsync(It.IsAny<Comment>())).Returns(Task.Run(() => new ServiceResult<Comment>(
                 new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" }
            )));

            var mockIUserRepository = new Mock<IUserRepository>();

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);
            var actual = commentService.AddCommentAsync(UserId, newCommentDTO).Result.Result;

            Assert.Equal(newComment.postId, actual.id);

        }

        [Fact]
        #region TODO tak jak w edit

        public void AddComment_InValidCall()
        {
            var newCommentDTO = new CommentDTONew() { PostID = 1, Content = "testNowy" };
            var newComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };

            var mockICommentRepository = new Mock<ICommentRepository>();

            mockICommentRepository.Setup(x => x.AddAsync(It.IsAny<Comment>())).Returns(Task.Run(() => new ServiceResult<Comment>(null, System.Net.HttpStatusCode.NotFound, "Not found")));

            var r = mockICommentRepository.Object.AddAsync(newComment);
            var mockIUserRepository = new Mock<IUserRepository>();

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);
            var actual = commentService.AddCommentAsync(UserId, newCommentDTO).Result;

            Assert.Null(actual.Result);
            Assert.Equal(404, (int)actual.Code);

        }
        #endregion
        [Fact]
        //istniejace id
        public void DeleteComment_ValidCall()
        {
            var mockICommentRepository = new Mock<ICommentRepository>();
            var id = comments[0].CommentID;
            mockICommentRepository.Setup(x => x.Delete(id, UserId)).Returns(new ServiceResult<bool>(true));

            var mockIUserRepository = new Mock<IUserRepository>();

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);
            var actual = commentService.DeleteComment(id, UserId).Result;

            Assert.True(actual);
        }

        [Fact]
        //Nieistneijacy id
        public void DeleteComment_InValidCall()
        {
            var mockICommentRepository = new Mock<ICommentRepository>();
            var id = 1000;
            mockICommentRepository.Setup(x => x.Delete(id, UserId)).Returns(new ServiceResult<bool>(false, System.Net.HttpStatusCode.BadRequest, "Server Internal error"));

            var mockIUserRepository = new Mock<IUserRepository>();

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);
            var actual = commentService.DeleteComment(id, UserId).Result;

            Assert.False(actual);

        }

        [Fact]

        public void EditComment_ValidCall()
        {
            var newCommentDTO = new CommentDTOEdit() { Content = "testNowy" };
            var newComment = new Comment() { CommentID = 2, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };

            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.UpdateAsync(It.IsAny<Comment>())).Returns(Task.Run(() => new ServiceResult<Comment>(newComment)));
            mockICommentRepository.Setup(x => x.GetById(newComment.CommentID)).Returns(new ServiceResult<Comment>(newComment));

            var mockIUserRepository = new Mock<IUserRepository>();

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);
            var actual = commentService.EditCommentAsync(newComment.CommentID, UserId, newCommentDTO).Result.Result;

            Assert.True(actual);

        }

        [Fact]

        public void EditComment_InValidCall()
        {
            var newCommentDTO = new CommentDTOEdit() { Content = "testNowy" };
            var newComment = new Comment() { CommentID = 200, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };

            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.UpdateAsync(It.IsAny<Comment>())).Returns(Task.Run(() => new ServiceResult<Comment>(null, System.Net.HttpStatusCode.BadRequest, "Bad request")));
            mockICommentRepository.Setup(x => x.GetById(newComment.CommentID)).Returns(new ServiceResult<Comment>(newComment));

            var mockIUserRepository = new Mock<IUserRepository>();

            var commentService = new CommentService(mockICommentRepository.Object, mockIUserRepository.Object);
            var actual = commentService.EditCommentAsync(newComment.CommentID, UserId, newCommentDTO).Result;

            Assert.NotNull(actual.Message);
        }

        [Fact]
        #region TODO
        public void EditLikeOnComment_ValidCall()
        {
            Assert.True(true);
        }
        #endregion
        [Fact]
        #region TODO
        public void EditLikeOnComment_InValidCall()
        {
            Assert.True(true);
        }
        #endregion

    }

}