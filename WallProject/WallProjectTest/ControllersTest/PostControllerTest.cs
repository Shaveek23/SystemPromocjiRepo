using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallProject.Controllers;
using WallProject.Services;
using WallProject.Services.Services_Interfaces;
using Xunit;

namespace WallProjectTest.ControllersTest
{
    public class PostControllerTest
    {

        [Theory]
        [InlineData("test", 1, 1, "title")]
        [InlineData("test2", 2, 2, "title2")]
        [InlineData("test3", 3, 3, "title3")]
        public void AddNewPost(string postText, int userId, int categoryId, string title)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new PostController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object);
            mockPostService.Setup(x => x.AddNewPost(postText, userId, categoryId, title)).Returns(Task.Run(() => new ServiceResult<bool>(true)));
            var result = (controller.AddNewPost(postText, userId, categoryId, title).Result) as ViewResult;

            Assert.NotNull(result);
            Assert.Null(result.ContentType);




        }
        [Theory]
        [InlineData("test", 1, 1, "title",1,1)]
        [InlineData("test2", 2, 2, "title2",3,0)]
        [InlineData("test3", 3, 3, "title3",2,1)]
        public void EditPostTest(string content, int userID, int postID, string title, int categoryID, int isPromoted)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new PostController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object);
            bool ispromoted = isPromoted == 1 ? true : false;
            mockPostService.Setup(x => x.EditPost(userID, postID, title, content, categoryID, ispromoted)).Returns(Task.Run(() => new ServiceResult<bool>(true)));
            var result = (controller.EditPost(userID, postID, title, content, categoryID, isPromoted).Result) as ViewResult;

            Assert.NotNull(result);
            Assert.Null(result.ViewData.Model);
        }

        [Theory]
        [InlineData(true, 1, 1)]
        [InlineData(false, 2, 2)]
        [InlineData(true, 3, 3)]
        public void EditPostLikeStatusTest(bool like, int postId, int userId)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new PostController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object);
            mockPostService.Setup(x => x.EditLikeStatus(userId, postId, like)).Returns(Task.Run(() => new ServiceResult<bool>(true)));
            var result = (controller.EditPostLikeStatus(userId, postId, like).Result) as ViewResult;
            Assert.NotNull(result);
            Assert.Null(result.ViewData.Model);
        }

        [Theory]
        [InlineData( 1, 1)]
        [InlineData( 2, 2)]
        [InlineData( 3, 3)]
        public void DeletePostTest( int postId, int userId)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new PostController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object);
            mockPostService.Setup(x => x.DeletePost(userId,postId)).Returns(Task.Run(() => new ServiceResult<bool>(true)));
            var result = (RedirectToActionResult)(controller.DeletePost(userId, postId).Result);
           
            Assert.NotNull(result);
            Assert.Equal("UserWall", result.ActionName);
            Assert.Equal("Home", result.ControllerName);

        }

    }
}
