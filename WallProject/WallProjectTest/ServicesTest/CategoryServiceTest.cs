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
    public class CategoryServiceTest
    {
        [Fact]
        public async void CategoryGetAllValidCall()
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"categories\":[{\"id\":1,\"name\":\"Obuwdsavcie\"},{\"id\":2,\"name\":\"RTV/AGD\"}," +
                    "{\"id\":3,\"name\":\"ElektVZXCXZCXZronika\"},{\"id\":4,\"name\":\"Odziez\"}," +
                    "{\"id\":5,\"name\":\"Telefdsadsadsaony\"},{\"id\":6,\"name\":\"dasdsa\"}]}")
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            CategoryService categoryService = new CategoryService(mockFactory.Object);

            var result = await categoryService.getAll();
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Code);
        }
    }
}
