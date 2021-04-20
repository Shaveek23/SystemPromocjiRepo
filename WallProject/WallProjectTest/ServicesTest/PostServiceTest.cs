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
    public class PosttServiceTest
    {
        [Theory]
        [InlineData("text", 1)]
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



            CommentService commentService = new CommentService(mockFactory.Object);
            PersonService personService = new PersonService(mockFactory.Object);
            PostService postService = new PostService(mockFactory.Object, commentService, personService);


            var result = await postService.AddNewPost(postText, userId);
            Assert.True(result.Result);


        }

    }
}
