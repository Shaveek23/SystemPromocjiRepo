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

namespace WallProjectTest
{
    public class UnitTest1
    {
        [Fact]
        public async void PersonGetByIdValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"personID\":1,\"firstName\":\"Adam\",\"lastName\":\"Nowak\",\"address\":\"ul. Koszykowa 57A/7\",\"city\":\"Warszawa\"}]")
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            PersonService personService = new PersonService(mockFactory.Object);

            var result = await personService.getAll();
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Code);
        }

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
                    Content = new StringContent("{\"id\":1,\"title\":\"tytu³ 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"category\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}")
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
    }
}
