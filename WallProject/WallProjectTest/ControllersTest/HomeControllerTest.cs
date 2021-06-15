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
    public class HomeControllerTest
    {
        [Fact]
        public void WallAsyncTest()
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockUserService = new Mock<IUserService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object,mockUserService.Object);
            mockWallService.Setup(x => x.getWall(1)).Returns(Task.Run(() => new ServiceResult<WallViewModel>(new WallViewModel())));
            var result = controller.WallAsync().Result as ViewResult;
            Assert.NotNull(result);
            Assert.NotNull(result.ViewData.Model);
            Assert.Equal("WallViewModel", result.ViewData.Model.GetType().Name);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void UserWallTest(int userId)
        {
            var mockUserService = new Mock<IUserService>();

            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object, mockUserService.Object);
            mockWallService.Setup(x => x.getWall(userId)).Returns(Task.Run(() => new ServiceResult<WallViewModel>(new WallViewModel())));
            var result = controller.UserWall(userId).Result as ViewResult;
            Assert.NotNull(result);
            Assert.NotNull(result.ViewData.Model);
            Assert.Equal("WallViewModel", result.ViewData.Model.GetType().Name);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void UserPostsTest(int userId)
        {
            var mockUserService = new Mock<IUserService>();

            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object, mockUserService.Object);
            mockWallService.Setup(x => x.getUserPosts(userId)).Returns(Task.Run(() => new ServiceResult<WallViewModel>(new WallViewModel())));
            var result = controller.UserPosts(userId).Result as ViewResult;
            Assert.NotNull(result);
            Assert.NotNull(result.ViewData.Model);
            Assert.Equal("WallViewModel", result.ViewData.Model.GetType().Name);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(100)]
        public void UserCommentsTest(int userId)
        {
            var mockUserService = new Mock<IUserService>();

            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object, mockUserService.Object);
            mockWallService.Setup(x => x.getUserComments(userId)).Returns(Task.Run(() => new ServiceResult<CommentListViewModel>(new CommentListViewModel())));
            var result = controller.UserComments(userId).Result as ViewResult;
            var actualName = result.ViewData.Model.GetType().Name;
            Assert.NotNull(result);
            Assert.NotNull(result.ViewData.Model);

            Assert.Equal("CommentListViewModel", actualName);

        }
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public async void ShowPostTest(int postId, int userId)
        {
            var mockUserService = new Mock<IUserService>();

            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object, mockUserService.Object);
            mockWallService.Setup(x => x.getPostToEdit(userId, postId)).Returns(Task.Run(() => new ServiceResult<PostEditViewModel>(new PostEditViewModel())));
            var actual = await controller.ShowPost(userId, postId);
            var result = actual as ViewResult;
            var actualName = result.ViewData.Model.GetType().Name;
            Assert.NotNull(result);
            Assert.NotNull(result.ViewData.Model);
            Assert.Equal("PostEditViewModel", actualName);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ChangeCategoryFilterStatusTest(int categoryId)
        {
            var mockUserService = new Mock<IUserService>();

            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockWallService = new Mock<IWallService>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object, mockWallService.Object, mockCommentService.Object, mockPostService.Object, mockUserService.Object);
            mockWallService.Setup(x => x.ChangeCategoryFilterStatus(categoryId));
            mockWallService.Setup(x => x.getWall(1)).Returns(Task.Run(() => new ServiceResult<WallViewModel>(new WallViewModel())));
            var result = controller.ChangeCategoryFilterStatus(categoryId).Result as ViewResult;
            Assert.NotNull(result);
            Assert.NotNull(result.ViewData.Model);
            Assert.Equal("WallViewModel", result.ViewData.Model.GetType().Name);
        }
    }
}
