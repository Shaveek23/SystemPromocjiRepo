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
using System.Text;

namespace WallProjectTest.ServiceTests
{
    public class PostServiceTest
    {
        public static readonly object[][] GetPostByIdTestData =
        {
            new object[]{0,"Cokolwiek", "Jakikolwiek kontent", new DateTime(2010,4,10,8,5,54),1,true,"Jaroslaw Kaczynski",69,420,false},
            new object[]{6969,"Smolensk", "Rekonstrukcja katastrofy smolenskiej w wykonaniu Antoniego Macierewicza", new DateTime(2010,4,10,8,5,54),2,true,"Antoni Macierewicz",69,420,true},
            new object[]{2137,"JP2", "Karol Wojtyla zostal papiezem", new DateTime(2010,4,10,8,5,54),3,false,"Antoni Macierewicz",69,420,true},

        };
        public static readonly object[][] GetAllTestData =
{
            new object[]{0,"Cokolwiek", "Jakikolwiek kontent", new DateTime(2010,4,10,8,5,54),1,true,"Jaroslaw Kaczynski",69,420,false},
            new object[]{6969,"Smolensk", "Rekonstrukcja katastrofy smolenskiej w wykonaniu Antoniego Macierewicza", new DateTime(2010,4,10,8,5,54),2,true,"Antoni Macierewicz",69,420,true},
            new object[]{2137,"JP2", "Karol Wojtyla zostal papiezem", new DateTime(2010,4,10,8,5,54),3,false,"Antoni Macierewicz",69,420,true},

        };

        public StringContent CreateContent(int id, string title, string content, DateTime dateTime, int category, bool isPromoted, string author, int authorId, int likesCount, bool isLikedByUser)
        {
            StringBuilder contentBuilder = new StringBuilder();
            //Content = new StringContent("{\"id\":1,\"title\":\"tytuł 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\"," +
            //    "\"category\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}");
            contentBuilder.Append("{\"id\":");
            contentBuilder.Append(id);
            contentBuilder.Append(",\"title\":\"");
            contentBuilder.Append(title);
            contentBuilder.Append("\",\"content\":\"");
            contentBuilder.Append(content);
            contentBuilder.Append("\",\"datetime\":\"");
            contentBuilder.Append(dateTime.ToString());
            contentBuilder.Append("\",\"category\":");
            contentBuilder.Append(category);
            contentBuilder.Append(",\"isPromoted\":");
            contentBuilder.Append(isPromoted.ToString().ToLower());
            contentBuilder.Append(",\"author\":\"");
            contentBuilder.Append(author);
            contentBuilder.Append("\",\"authorID\":");
            contentBuilder.Append(authorId);
            contentBuilder.Append(",\"likesCount\":");
            contentBuilder.Append(likesCount);
            contentBuilder.Append(",\"isLikedByUser\":");
            contentBuilder.Append(isLikedByUser.ToString().ToLower());
            contentBuilder.Append("}");

            return new StringContent(contentBuilder.ToString());
        }

        //public async Task<ServiceResult<List<PostViewModel>>> getAll(int userID)

        [Theory, MemberData(nameof(GetAllTestData))]
        public async void GetAllValidCall(int id, string title, string content, DateTime dateTime, int category, bool isPromoted, string author, int authorId, int likesCount, bool isLikedByUser)
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = CreateContent(id, title, content, dateTime, category, isPromoted, author, authorId, likesCount, isLikedByUser)
                    //Content = new StringContent("{\"id\":1,\"title\":\"tytuł 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"category\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockResult = new ServiceResult<List<CommentViewModel>>(new List<CommentViewModel>());
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(_ => _.getByPostId(id, authorId))
                .Returns(Task.FromResult(mockResult));
            var mockPersonService = new Mock<IPersonService>();

            PostService postService = new PostService(mockFactory.Object, mockCommentService.Object, mockPersonService.Object);
            var result = await postService.getById(id, authorId);
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.Empty(result.Result.Comments);
        }

        [Theory,MemberData(nameof(GetPostByIdTestData))]
        public async void PostGetByIdValidCall(int id, string title, string content, DateTime dateTime, int category, bool isPromoted, string author, int authorId, int likesCount, bool isLikedByUser)
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = CreateContent(id, title, content, dateTime, category, isPromoted, author, authorId, likesCount, isLikedByUser)
                    //Content = new StringContent("{\"id\":1,\"title\":\"tytuł 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"category\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockResult = new ServiceResult<List<CommentViewModel>>(new List<CommentViewModel>());
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(_ => _.getByPostId(id, authorId))
                .Returns(Task.FromResult(mockResult));
            var mockPersonService = new Mock<IPersonService>();

            PostService postService = new PostService(mockFactory.Object, mockCommentService.Object, mockPersonService.Object);
            var result = await postService.getById(id, authorId);
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.Empty(result.Result.Comments);
        }
    }
}
