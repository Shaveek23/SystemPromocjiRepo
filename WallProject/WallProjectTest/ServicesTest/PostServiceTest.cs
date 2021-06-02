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
                    Content = new StringContent("{\"id\":1,\"title\":\"tytuł 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"CategoryID\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockResult = new ServiceResult<List<CommentViewModel>>(new List<CommentViewModel>());
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(_ => _.getByPostId(1, 1))
                .Returns(Task.FromResult(mockResult));
            var mockPersonService = new Mock<IUserService>();

            var mockUserResult = new ServiceResult<List<UserViewModel>>(new List<UserViewModel>() { new UserViewModel()
            {
                UserID = 1, IsActive = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = true, Timestamp = DateTime.MinValue, UserEmail="DSADASDAS", UserName="name"
            }
            });
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(_ => _.getAll())
            .Returns(Task.FromResult(mockUserResult));

            PostService postService = new PostService(mockFactory.Object, mockCommentService.Object, mockUserService.Object);
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
                    Content = new StringContent("[{\"id\":1,\"title\":\"tytuł 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"CategoryID\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}," +
                    "{\"id\":2,\"title\":\"tytuł 2\",\"content\":\"Oto mój drugi post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"CategoryID\":2,\"isPromoted\":false,\"author\":\"Jan2\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}]")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockResult = new ServiceResult<List<CommentViewModel>>(new List<CommentViewModel>());
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(_ => _.getAll(1)).Returns(Task.FromResult(mockResult));
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
        [InlineData("text", 1,1, "text")]
        [InlineData("lol", 3, 1, "text")]
        [InlineData("cokolwiek do testu", 420, 2, "text")]
        [InlineData("jakis test", 6, 1, "text")]
        public async void Test_AddNewPost(string postText, int userId,int categoryId,string title)

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


            var result = await postService.AddNewPost(postText, userId,categoryId,title);
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
        [Theory]
        [InlineData(1 )]
        [InlineData(4)]
        [InlineData(3)]
        async public void getUserPostsTest(int userID)
        {
            var fixure = new Fixture();
            var mockFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[{\"id\":1,\"title\":\"tytuł 1\",\"content\":\"Oto mój pierwszy post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"CategoryID\":1,\"isPromoted\":false,\"author\":\"Jan\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}," +
                    "{\"id\":2,\"title\":\"tytuł 2\",\"content\":\"Oto mój drugi post!\",\"datetime\":\"2021-03-30T22:21:46.5885085\",\"CategoryID\":2,\"isPromoted\":false,\"author\":\"Jan2\",\"authorID\":1,\"likesCount\":5,\"isLikedByUser\":false}]")
                });
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixure.Create<Uri>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client).Verifiable();
            var mockResult = new ServiceResult<List<CommentViewModel>>(new List<CommentViewModel>());
            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(_ => _.getAll(userID)).Returns(Task.FromResult(mockResult));
            var mockPersonService = new Mock<IUserService>();
            mockPersonService.Setup(x => x.getAll()).Returns(Task.FromResult(new ServiceResult<List<UserViewModel>>(new List<UserViewModel>(), HttpStatusCode.OK)));
            PostService postService = new PostService(mockFactory.Object, mockCommentService.Object, mockPersonService.Object);
            var result = await postService.getUserPosts(userID);
            Assert.NotNull(result);
            Assert.Null(result.Message);
            Assert.Equal(HttpStatusCode.OK, result.Code);
            Assert.Empty(result.Result[0].Comments);
            Assert.Empty(result.Result[1].Comments);
        }

        [Theory]
        [InlineData(1, 1, "text", "text",1,true)]
        [InlineData(2, 3, "text", "text",2,false)]
        [InlineData(3, 420,  "cokolwiek do testu","text",3,true)]
        [InlineData(4, 6,  "jakis test", "text",1,false)]
        async public void EditPostTest(int userID, int postID, string title, string content, int categoryID, bool isPromoted)
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
            PersonService personService = new PersonService(mockFactory.Object);
            PostService postService = new PostService(mockFactory.Object, commentService, mockUserService.Object);
            var result = await postService.EditPost(userID, postID, title, content, categoryID, isPromoted);
            Assert.True(result.Result);
        }
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 3)]
        [InlineData(3, 420)]
        async public void DeletePost(int userID, int postID)
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
            PersonService personService = new PersonService(mockFactory.Object);
            PostService postService = new PostService(mockFactory.Object, commentService, mockUserService.Object);
            var result = await postService.DeletePost(userID, postID);
            Assert.True(result.Result);
        }

    }
}
