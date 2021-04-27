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


            CommentService commentService = new CommentService(mockFactory.Object, mockUserService.Object);


            var result = await commentService.AddNewComment(commentText, postId, userId);
            Assert.True(result.Result);


        }

    }
}
