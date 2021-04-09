using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WallProject.Controllers;
using WallProject.Models;
using WallProject.Services.Services_Interfaces;
using Xunit;

namespace WallProjectTests.ControllerTests
{
    public class PostControllerTests
    {
        [Theory]
        [InlineData(0,"cokolwiek", "Warszawa Centrum", false)]
        public void GetByPostId_Test(int postId,string content, string localization, bool isPromoted)
        {
            // Arrange
            var mockLogger = new Mock<ILogger<PostController>>();
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetByPostIdAsync(postId)).Returns(Task.FromResult(new PostViewModel
            {
                Content = content,
                IsPromoted = isPromoted,
                Localization = localization,
            }));

            var controller = new PostController(mockLogger.Object, mockService.Object);

            var expected = new PostViewModel
            {
                Content = content,
                IsPromoted = isPromoted,
                Localization = localization,
            };

            //Act
            var actual = controller.GetByPostIdAsync(postId).Result;
            Assert.Equal(actual, expected);

        }

        [Theory]
        [InlineData("cokolwiek", "Warszawa Centrum", false)]
        public void GetAll(string content, string localization, bool isPromoted)
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            posts.Add(new PostViewModel
            {
                Content = content,
                IsPromoted = isPromoted,
                Localization = localization,
            });

            posts.Add(new PostViewModel
            {
                Content = content+" quality",
                IsPromoted = !isPromoted,
                Localization = localization+ " Ratusz",
            });

            // Arrange
            var mockLogger = new Mock<ILogger<PostController>>();
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(posts));
            var controller = new PostController(mockLogger.Object, mockService.Object);
            var expected = posts;

            //Act
            var actual = controller.GetAllAsync().Result;

            //Assert
            Assert.Equal(expected,actual);
        }

        [Theory]
        [InlineData(0, "cokolwiek", "Warszawa Centrum", false)]
        public void GetByPostUserId_Test(int userId, string content, string localization, bool isPromoted)
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            posts.Add(new PostViewModel
            {
                Content = content,
                IsPromoted = isPromoted,
                Localization = localization,
            });

            posts.Add(new PostViewModel
            {
                Content = content + " quality",
                IsPromoted = !isPromoted,
                Localization = localization + " Ratusz",
            });

            // Arrange
            var mockLogger = new Mock<ILogger<PostController>>();
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetByUserIdAsync(userId)).Returns(Task.FromResult(posts));
            var controller = new PostController(mockLogger.Object, mockService.Object);
            var expected = posts;

            //Act
            var actual = controller.GetByUserIdAsync(userId).Result;

            //Assert
            Assert.Equal(expected, actual);

        }

    }
}
