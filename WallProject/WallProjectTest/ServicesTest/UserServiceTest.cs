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
    public class UserServiceTest
    {
        [Fact]
        public async void UserGetByIdValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"userId\":1,\"userName\":\"gawezi\",\"userEmail\":\"cokolwiek@cokolwiek.pl\",\"isAdmin\":true,\"isEnterprenuer\":true,\"isVerified\":true,\"isActive\":true,}")
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            UserService userService = new UserService(mockFactory.Object);

            var result = await userService.getById(1);
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Code);
        }

        [Fact]
        public async void UserGetAllValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"userId\":1,\"userName\":\"gawezi\",\"userEmail\":\"cokolwiek@cokolwiek.pl\",\"isAdmin\":true,\"isEnterprenuer\":true,\"isVerified\":true,\"isActive\":true},"+
                    "{\"userId\":2,\"userName\":\"golik\",\"userEmail\":\"cokolwiek@golik.pl\",\"isAdmin\":true,\"isEnterprenuer\":true,\"isVerified\":true,\"isActive\":true}]")
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            UserService userService = new UserService(mockFactory.Object);

            var result = await userService.getAll();
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Code);
        }
    }
}
