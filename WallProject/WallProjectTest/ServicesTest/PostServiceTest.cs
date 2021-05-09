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

namespace WallProjectTest.ServicesTest
{
    public class PostServiceTest
    {

        [Fact]
        public async void PostGetByIdValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"id\":1,\"title\":\"tytuł 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"category\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false,\"comments\":[]}")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockResult = new ServiceResult<List<CommentViewModel>>(new List<CommentViewModel>());
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(_ => _.getByPostId(1, 1))
                .Returns(Task.FromResult(mockResult));
            var mockPersonService = new Mock<IUserService>();

            PostService postService = new PostService(mockFactory.Object, mockCommentService.Object, mockPersonService.Object);
            var result = await postService.getById(1, 1);
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.Empty(result.Result.Comments);
        }

        [Fact]
        public async void PostGetAllValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"id\":1,\"title\":\"tytuł 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"category\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false,\"comments\":[]}," +
                    "{\"id\":2,\"title\":\"tytuł 2\",\"content\":\"Oto mój drugi post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"category\":2,\"isPromoted\":false,\"author\":\"Jan2\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false,\"comments\":[]}]")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockResult = new ServiceResult<List<CommentViewModel>>(new List<CommentViewModel>());
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(_ => _.getByPostId(1, 1))
                .Returns(Task.FromResult(mockResult)); mockCommentService.Setup(_ => _.getByPostId(2, 1))
     .Returns(Task.FromResult(mockResult));
            var mockPersonService = new Mock<IUserService>();
            mockPersonService.Setup(x => x.getAll()).Returns(Task.FromResult(new ServiceResult<List<UserViewModel>>(new List<UserViewModel>(), HttpStatusCode.OK)));

            PostService postService = new PostService(mockFactory.Object, mockCommentService.Object, mockPersonService.Object);
            var result = await postService.getAll(1);
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.Empty(result.Result[0].Comments);
            Assert.Empty(result.Result[1].Comments);

        }

        [Theory]
        [InlineData("text", 1)]
        [InlineData("lol", 3)]
        [InlineData("cokolwiek do testu", 420)]
        [InlineData("jakis test", 6)]
        public async void Test_AddNewPost(string postText, int userId)
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
            PersonService personService = new PersonService(mockFactory.Object);
            PostService postService = new PostService(mockFactory.Object, commentService, mockUserService.Object);


            var result = await postService.AddNewPost(postText, userId);
            Assert.True(result.Result);


        }

        [Theory]
        [InlineData(1, 1, true)]
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
            var mockCommentService = new Mock<ICommentService>();
            PostService postService = new PostService(mockFactory.Object, mockCommentService.Object,mockUserService.Object);
            var result = postService.EditLikeStatus(commentId, userId, like).Result.Result;
            Assert.True(result);
        }

    }
}
