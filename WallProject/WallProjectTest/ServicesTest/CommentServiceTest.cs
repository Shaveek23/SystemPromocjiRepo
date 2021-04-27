using System;
using Xunit;
using Moq;
using System.Net.Http;
using System.Net;
using WallProject.Models.DTO;
using System.Threading.Tasks;
using Moq.Protected;
using System.Threading;
using WallProject.Services.Serives_Implementations;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using WallProject.Services.Services_Interfaces;
using WallProject.Services;
using WallProject.Models;
using System.Collections.Generic;
using WallProject.Models.MainView;
using Newtonsoft.Json;

namespace WallProjectTest.ServicesTest
{
    public class CommentServiceTest
    {
        [Theory]
        [InlineData("text", 1, 1)]
        public async void Test_AddNewComment(string commentText, int postId, int userId)
        {


            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,

                    });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();



            var mockUserService = new Mock<IUserService>();
            CommentService commentService = new CommentService(mockFactory.Object,mockUserService.Object);



            var result = await commentService.AddNewComment(commentText, postId, userId);
            Assert.True(result.Result);
        }



        [Theory]
        [InlineData(1,1,true)]
        [InlineData(2, 1, false)]
        [InlineData(5, 0, false)]

        public void EditLikeStatus(int commentId, int userId, bool like)
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,

                    });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();



            var mockUserService = new Mock<IUserService>();
            CommentService commentService = new CommentService(mockFactory.Object, mockUserService.Object);
            var result = commentService.EditLikeStatus(commentId, userId, like).Result.Result;
            Assert.True(result);
        }

        [Fact]
        public async void CommentGetByIdValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"id\":1,\"ownerMode\":true,\"content\":\"Oto mój pierwszy komentarz!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"authorID\":1,\"authorName\":\"Jan\",\"likesCount\":5,\"isLikedByUser\":false, \"postID\":1}")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockPersonService = new Mock<IUserService>();
            mockPersonService.Setup(x => x.getById(1)).Returns(Task.FromResult(new ServiceResult<UserViewModel>(new UserViewModel { UserID = 1 }, HttpStatusCode.OK)));
            CommentService commentService = new CommentService(mockFactory.Object, mockPersonService.Object);
            var result = await commentService.getById(1, 1);
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(HttpStatusCode.OK, result.Code);
        }

        [Fact]
        public async void CommentGetByPostIdValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"id\":1,\"ownerMode\":true,\"content\":\"Oto mój pierwszy komentarz!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"authorID\":1,\"authorName\":\"Jan\",\"likesCount\":5,\"isLikedByUser\":false, \"postID\":1},"+
                    "{\"id\":2,\"ownerMode\":true,\"content\":\"Oto mój drugi komentarz!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"authorID\":3,\"authorName\":\"Jan Rapowanie\",\"likesCount\":10,\"isLikedByUser\":false, \"postID\":1}]")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockPersonService = new Mock<IUserService>();
            mockPersonService.Setup(x => x.getAll()).Returns(Task.FromResult(new ServiceResult<List<UserViewModel>> (new List<UserViewModel>{ new UserViewModel { UserID = 1 }, new UserViewModel { UserID = 3 } }, HttpStatusCode.OK)));
            CommentService commentService = new CommentService(mockFactory.Object, mockPersonService.Object);
            var result = await commentService.getByPostId(1, 1);
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(HttpStatusCode.OK, result.Code);
        }
        [Fact]
        public async void CommentGetValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"id\":1,\"ownerMode\":true,\"content\":\"Oto mój pierwszy komentarz!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"authorID\":1,\"authorName\":\"Jan\",\"likesCount\":5,\"isLikedByUser\":false, \"postID\":1}," +
                    "{\"id\":2,\"ownerMode\":true,\"content\":\"Oto mój drugi komentarz!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"authorID\":1,\"authorName\":\"Jan Rapowanie\",\"likesCount\":10,\"isLikedByUser\":false, \"postID\":1}]")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockPersonService = new Mock<IUserService>();
            mockPersonService.Setup(x => x.getAll()).Returns(Task.FromResult(new ServiceResult<List<UserViewModel>>(new List<UserViewModel> { new UserViewModel { UserID = 1 }, new UserViewModel { UserID = 3 } }, HttpStatusCode.OK)));
            CommentService commentService = new CommentService(mockFactory.Object, mockPersonService.Object);
            var result = await commentService.getAll(1);
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(HttpStatusCode.OK, result.Code);
        }



    }
}
