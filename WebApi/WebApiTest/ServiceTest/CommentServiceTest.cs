﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
using WebApi.Models.POCO;
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

        [Fact]
        public void GetById_ValidCall()
        {
            int expectedID = 1;
            var expected = comments.Where(x => x.CommentID == expectedID).FirstOrDefault();
            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.GetById(expectedID)).Returns(expected);

            var commentService = new CommentService(mockICommentRepository.Object);

            var actual = commentService.GetById(expectedID, UserId);


            Assert.True(actual != null);
            Assert.Equal(expected.CommentID, actual.CommentID);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.DateTime, actual.DateTime);
            Assert.Equal(expected.PostID, actual.PostID);
            Assert.Equal(expected.UserID, actual.UserID);


        }

        [Fact]
        public void GetById_InValidCall()
        {
            int expectedID = 0;
            var expected = comments.Where(x => x.CommentID == expectedID).FirstOrDefault();
            var mockICommentRepository = new Mock<ICommentRepository>();

            mockICommentRepository.Setup(x => x.GetById(expectedID)).Returns((Comment)(null));

            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.GetById(expectedID, UserId);


            Assert.Null(actual);




        }
        [Fact]
        public void GetAll_ValidCall()
        {
            var expected = comments;
            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.GetAll()).Returns(expected.AsQueryable());

            var commentService = new CommentService(mockICommentRepository.Object);

            var actual = commentService.GetAll(UserId).ToList();


            Assert.True(expected.All(item => actual.Any(actualItem => item.CommentID == actualItem.CommentID &&
              item.UserID == actualItem.UserID && item.Content == actualItem.Content &&
              item.PostID == actualItem.PostID)));



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

            var newCommentDTO = new CommentDTO() { UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };
            var newComment = new CommentDTOOutput() { CommentID = 4, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };
            expected.Add(new Comment { CommentID = 4, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" });
            var mockICommentRepository = new Mock<ICommentRepository>();

            mockICommentRepository.Setup(x => x.AddAsync(It.IsAny<Comment>())).Returns(Task.Run(() =>
            {
                return new Comment() { CommentID = 0, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };
            }));


            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.AddCommentAsync(UserId, newCommentDTO).Result;


            Assert.Equal(newComment.Content, actual.Content);
            Assert.Equal(newComment.DateTime, actual.DateTime);
            Assert.Equal(newComment.PostID, actual.PostID);
            Assert.Equal(newComment.UserID, actual.UserID);










        }

        [Fact]
        #region TODO tak jak w edit

        public void AddComment_InValidCall()
        {


            var newCommentDTO = new CommentDTO() { UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };
            var newComment = new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };

            var mockICommentRepository = new Mock<ICommentRepository>();

            mockICommentRepository.Setup(x => x.AddAsync(newComment)).Returns(Task.Run(() =>
            {
                return new Comment();
            }));


            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.AddCommentAsync(UserId, newCommentDTO).Result;

            Assert.True(actual == null);
        }
        #endregion
        [Fact]
        //istniejace id
        public void DeleteComment_ValidCall()
        {
            var mockICommentRepository = new Mock<ICommentRepository>();
            var id = comments[0].CommentID;
            mockICommentRepository.Setup(x => x.DeleteComment(id, UserId)).Returns(true);

            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.DeleteComment(id, UserId);

            Assert.True(actual);


        }

        [Fact]
        //Nieistneijacy id
        public void DeleteComment_InValidCall()
        {
            var mockICommentRepository = new Mock<ICommentRepository>();
            var id = 1000;
            mockICommentRepository.Setup(x => x.DeleteComment(id, UserId)).Returns(false);

            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.DeleteComment(id, UserId);

            Assert.False(actual);
        }

        [Fact]

        public void EditComment_ValidCall()
        {


            var newCommentDTO = new CommentDTO() { UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };
            var newComment = new Comment() { CommentID = 2, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };

            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.UpdateAsync(It.IsAny<Comment>())).Returns(Task.Run(() => newComment));


            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.EditCommentAsync(newComment.CommentID, UserId, newCommentDTO).Result;

            Assert.True(actual != null);

            Assert.Equal(newComment.Content, actual.Content);
            Assert.Equal(newComment.DateTime, actual.DateTime);
            Assert.Equal(newComment.PostID, actual.PostID);
            Assert.Equal(newComment.UserID, actual.UserID);

        }

        [Fact]
        #region TODO: Nw czy to moze byc w ten sposob, ale nw jak zwrocic null z taska
        public void EditComment_InValidCall()
        {
            var newCommentDTO = new CommentDTO() { UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };
            var newComment = new Comment() { CommentID = 200, UserID = 1, PostID = 1, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "testNowy" };

            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.UpdateAsync(newComment)).Returns(Task.Run(() => new Comment()));


            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.EditCommentAsync(newComment.CommentID, UserId, newCommentDTO).Result;

            Assert.Null(actual);
        }
        #endregion
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