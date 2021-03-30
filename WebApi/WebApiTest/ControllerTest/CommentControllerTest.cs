using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models.DTO;
using WebApi.Services.Services_Interfaces;
using Xunit;

namespace WebApiTest.ControllerTest
{
    public class CommentControllerTest
    {
        [Theory]
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]

        public void GetById_Test(int c_id, int u_id, int p_id, string content)
        {
            DateTime date = DateTime.Today;

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.GetById(0)).Returns(new CommentDTO
            {
                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            });

            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = new CommentDTO
            {
                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            };

            var actual = controller.GetById( 0);
            Assert.Equal(expected.CommentID, actual.CommentID);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.DateTime, actual.DateTime);
            Assert.Equal(expected.PostID, actual.PostID);
            Assert.Equal(expected.UserID, actual.UserID);


        }
        [Theory]
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]
        public void GetAll_Test(int c_id, int u_id, int p_id, string content)
        {
            DateTime date = DateTime.Today;


            List<CommentDTO> comments = new List<CommentDTO>();
            comments.Add(new CommentDTO
            {

                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            });
            comments.Add(new CommentDTO
            {

                CommentID = c_id + 1,
                UserID = u_id + 2,
                PostID = p_id + 3,
                DateTime = date,
                Content = content
            });
            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.GetAll()).Returns(comments.AsQueryable());


            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = comments;
            var actual = controller.GetAll().ToList();
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }

        [Theory]
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]
        public void AddCommentDTO_Test(int c_id, int u_id, int p_id, string content)
        {

            DateTime date = DateTime.Today;

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.AddCommentAsync(It.IsAny<CommentDTO>())).Returns(Task.Run(() =>
              {
                  return new CommentDTO
                  {
                      CommentID = c_id,
                      UserID = u_id,
                      PostID = p_id,
                      DateTime = date,
                      Content = content
                  };
              }));
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = new CommentDTO
            {
                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            };
            var actual = controller.AddComment(new CommentDTO
            {

                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content

            }).Result;
            Assert.Equal(expected.CommentID, actual.CommentID);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.DateTime, actual.DateTime);
            Assert.Equal(expected.PostID, actual.PostID);
            Assert.Equal(expected.UserID, actual.UserID);

        }


        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]

        public void GetLikedUsers_Test(int c_id, int u_id)
        {
            DateTime date = DateTime.Today;

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.GetLikedUsers(0)).Returns(

                (IQueryable<int>)new List<int>() { u_id }
            );


            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = new List<int> { u_id };
            var actual = controller.GetLikedUsers(c_id).ToList();
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }

        //NW Jak to zrobic 
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]

        public void DeleteComment_Test(int c_id, int u_id)
        {
            DateTime date = DateTime.Today;

            var mockService = new Mock<ICommentService>();


            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            mockService.Setup(x => x.DeleteComment(c_id));

        }

        [Theory]
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]
        public void EditCommentDTO_Test(int c_id, int u_id, int p_id, string content)
        {
            DateTime date = DateTime.Today;

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.EditCommentAsync(It.IsAny<CommentDTO>())).Returns(Task.Run(() =>
            {
                return new CommentDTO
                {
                    CommentID = c_id,
                    UserID = u_id,
                    PostID = p_id,
                    DateTime = date,
                    Content = content
                };
            }));
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = new CommentDTO
            {
                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            };
            var actual = controller.EditComment( new CommentDTO
            {

                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content

            }).Result;
            Assert.Equal(expected.CommentID, actual.CommentID);
            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.DateTime, actual.DateTime);
            Assert.Equal(expected.PostID, actual.PostID);
            Assert.Equal(expected.UserID, actual.UserID);
        }
        //Tu tez nw jak EDitLikeOnComment
    }
}
