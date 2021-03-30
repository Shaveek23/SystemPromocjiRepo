using Moq;
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
        List<Comment> comments = new List<Comment>
        {
            new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "test" },
             new Comment() { CommentID = 2, UserID = 2, PostID = 2, DateTime = DateTime.Today, Content = "test2" },
            new Comment() { CommentID = 3, UserID = 3, PostID = 3, DateTime = DateTime.Today, Content = "test3" }

        };

        [Fact]
        public void GetById_ValidCall()
        {
            int expectedID = 1;
            var expected = comments.Where(x => x.CommentID == expectedID).FirstOrDefault();
            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.GetById(expectedID)).Returns(expected);

            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.GetById(expectedID);

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
            mockICommentRepository.Setup(x => x.GetById(expectedID)).Returns(new Comment { });

            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.GetById(expectedID);

            Assert.True(actual.CommentID == 0);
            Assert.Null(actual.Content);
            ;


        }
        [Fact]
        public void GetAll_ValidCall()
        {
            var expected = comments;
            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.GetAll()).Returns(expected.AsQueryable());

            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.GetAll().ToList();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
            Assert.True(expected.All(item => actual.Any(actualItem => item.CommentID == actualItem.CommentID &&
              item.UserID == actualItem.UserID && item.Content == actualItem.Content &&
              item.PostID == actualItem.PostID)));



        }
        [Fact]
        public void GetAll_InValidCall()
        {
            throw new NotImplementedException();
            

        }

        [Fact]
        public void GetLikedUsers_ValidCall()
        {

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

            int initLength = comments.Count();
            var expected = comments;
            var newCommentDTO = new CommentDTO() { CommentID = 4, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "testNowy" };
            var newComment = new Comment() { CommentID = 4, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "testNowy" };
            expected.Add(new Comment { CommentID = 4, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "testNowy" });
            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.AddAsync(newComment)).Returns(Task.Run(() => newComment));          


            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.AddCommentAsync(newCommentDTO).Result;

            Assert.True(actual != null);
            Assert.Equal(newComment.CommentID, actual.CommentID);
            Assert.Equal(newComment.Content, actual.Content);
            Assert.Equal(newComment.DateTime, actual.DateTime);
            Assert.Equal(newComment.PostID, actual.PostID);
            Assert.Equal(newComment.UserID, actual.UserID);
            //DLUGOSC




        }
    
    [Fact]
    
    public void AddComment_InValidCall()
    {
        throw new NotImplementedException();
    }
    [Fact]
    public void DeleteComment_ValidCall()
    {


            throw new NotImplementedException();

        }
    [Fact]
    public void DeleteComment_InValidCall()
    {
        throw new NotImplementedException();
    }
    [Fact]
    public void EditComment_ValidCall()
    {
           
           
            var newCommentDTO = new CommentDTO() { CommentID = 4, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "testNowy" };
            var newComment = new Comment() { CommentID = 4, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "testNowy" };
          
            var mockICommentRepository = new Mock<ICommentRepository>();
            mockICommentRepository.Setup(x => x.UpdateAsync(newComment)).Returns(Task.Run(() => newComment));


            var commentService = new CommentService(mockICommentRepository.Object);
            var actual = commentService.EditCommentAsync(newCommentDTO).Result;

            Assert.True(actual != null);
            Assert.Equal(newComment.CommentID, actual.CommentID);
            Assert.Equal(newComment.Content, actual.Content);
            Assert.Equal(newComment.DateTime, actual.DateTime);
            Assert.Equal(newComment.PostID, actual.PostID);
            Assert.Equal(newComment.UserID, actual.UserID);

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
