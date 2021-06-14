using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallProject.Controllers;
using WallProject.Models;
using WallProject.Services;
using WallProject.Services.Services_Interfaces;
using Xunit;

namespace WallProjectTest.ControllersTest
{
    public class CommentControllerTest
    {

        [Theory]
        [InlineData("test",1, 1 )]
        [InlineData("test2",2, 2)]
        [InlineData("test3",3, 3)]
        public   void AddNewCommnetTest(string commentText, int postId, int userId)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object);
            mockCommentService.Setup(x => x.AddNewComment(commentText, postId, userId)).Returns(Task.Run(()=>new ServiceResult<bool>(true)));
            var result = (controller.AddNewComment(commentText, postId, userId).Result) as ViewResult;

            Assert.NotNull(result);
            Assert.Null(result.ViewData.Model);




        }
        [Theory]
        [InlineData("test", 1, 1)]
        [InlineData("test2", 2, 2)]
        [InlineData("test3", 3, 3)]
        public  void EditCommnetTest(string commentText, int conmentId, int userId)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object);
            mockCommentService.Setup(x => x.Edit( userId, conmentId, commentText)).Returns(Task.Run(() => new ServiceResult<bool>(true)));
            var result = (controller.EditComment(userId, conmentId, commentText).Result) as ViewResult;
            Assert.NotNull(result);
            Assert.Null(result.ViewData.Model);
        }
    
        [Theory]
        [InlineData(true, 1, 1)]
        [InlineData(false, 2, 2)]
        [InlineData(true, 3, 3)]
        public  void EditCommnetLikeStatusTest(bool like, int conmentId, int userId)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object);
            mockCommentService.Setup(x => x.EditLikeStatus(userId, conmentId,like)).Returns(Task.Run(() => new ServiceResult<bool>(true)));
            var result = (controller.EditCommentLikeStatus(userId, conmentId, like).Result) as ViewResult;
            Assert.NotNull(result);
            Assert.Null(result.ViewData.Model);
        }
    }
}
