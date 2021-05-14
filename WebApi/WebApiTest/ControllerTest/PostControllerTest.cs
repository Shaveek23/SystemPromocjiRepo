using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using WebApi.Controllers;
using WebApi.Models.POCO;
using Microsoft.Extensions.Logging;
using WebApi.Services;
using WebApi.Services.Serives_Implementations;
using WebApi.Services.Services_Interfaces;
using WebApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using WebApi.Models.DTO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using NuGet.Frameworks;
using Microsoft.VisualBasic;
using WebApi.Database.Mapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Services.Services_Implementations;

namespace WebApiTest.ControllerTest
{
    public class PostControllerTest
    {
        int userID = 1;
        DateTime datetime1 = new DateTime(2020, 1, 1);
        DateTime datetime2 = new DateTime(3213123);
        DateTime datetime3 = new DateTime(2000, 10, 10, 11, 4, 41);



        [Theory]
        [InlineData(0, "Konrad Gaweda", 1, 1, "MyTitle", "Content", 41, false, false)]
        [InlineData(1, "Jan Kowalski", 11, 2, "MyTitle1", "Conte nt1 ", 11, false, true)]
        [InlineData(4, "Jan Gawęda", 1, 4, "MyTitle2", " Co ntent2 ", 33, true, false)]
        [InlineData(7, "Konrad Kowalski", 32, 8,  "MyTitle3", "Conten t3", 441, true, true)]
        public void GetAll_Test(int in_id, string in_author, int in_authorID, int in_category, 
            string in_title, string in_content, int in_likesCount, bool in_isLiked, bool in_isPromoted)
        {
            List<PostDTOOutput> posts = new List<PostDTOOutput>();
            posts.Add(new PostDTOOutput
            {
                id = in_id,
                author = in_author,
                authorID = in_authorID,
                category = in_category,
                title = in_title,
                content = in_content,
                likesCount = in_likesCount,
                datetime = datetime1,
                isLikedByUser = in_isLiked,
                isPromoted = in_isPromoted
            });

            posts.Add(new PostDTOOutput
            {
                id = in_id + 1,
                author = in_author + " Second",
                authorID = in_authorID + 1,
                category = in_category + 32,
                title = in_title+ " One",
                content = in_content + " Three",
                likesCount = in_likesCount + 14,
                datetime = datetime2,
                isLikedByUser = in_isLiked = !in_isLiked,
                isPromoted = in_isPromoted = ! in_isPromoted
            });

            //Arrange
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetAll(userID)).Returns(new ServiceResult<IQueryable<PostDTOOutput>>(posts.AsQueryable()));
            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);

            var expected = posts;

            //Act
            var actual = ((IQueryable<PostDTOOutput>)((ObjectResult)controller.GetAll(userID).Result).Value);
            //Asset
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }



        [Theory]
        [InlineData(0, "Konrad Gaweda", 1, 1, "MyTitle", "Content", 41, false, false)]
        [InlineData(1, "Jan Kowalski", 11, 2, "MyTitle1", "Conte nt1 ", 11, false, true)]
        [InlineData(4, "Jan Gawęda", 1, 4, "MyTitle2", " Co ntent2 ", 33, true, false)]
        [InlineData(7, "Konrad Kowalski", 32, 8, "MyTitle3", "Conten t3", 441, true, true)]
        public void GetAllOfUser_Test(int in_id, string in_author, int in_authorID, int in_category,
            string in_title, string in_content, int in_likesCount, bool in_isLiked, bool in_isPromoted)
        {
            List<PostDTOOutput> posts = new List<PostDTOOutput>();
            posts.Add(new PostDTOOutput
            {
                id = in_id,
                author = in_author,
                authorID = in_authorID,
                category = in_category,
                title = in_title,
                content = in_content,
                likesCount = in_likesCount,
                datetime = datetime1,
                isLikedByUser = in_isLiked,
                isPromoted = in_isPromoted
            });

            posts.Add(new PostDTOOutput
            {
                id = in_id + 1,
                author = in_author + " Second",
                authorID = in_authorID + 1,
                category = in_category + 31,
                title = in_title + " One",
                content = in_content + " Three",
                likesCount = in_likesCount + 14,
                datetime = datetime2,
                isLikedByUser = !in_isLiked,
                isPromoted = !in_isPromoted
            });

            posts.Add(new PostDTOOutput
            {
                id = in_id,
                author = in_author + " Second2",
                authorID = in_authorID,
                category = in_category + 3,
                title = in_title + " Onedsa",
                content = in_content + " Three",
                likesCount = in_likesCount + 4,
                datetime = datetime3,
                isLikedByUser = in_isLiked,
                isPromoted = !in_isPromoted
            });

            //Arrange
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetAllOfUser(in_authorID)).Returns(new ServiceResult<IQueryable<PostDTOOutput>>(posts.Where(p=>p.authorID == in_authorID).AsQueryable()));
            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);

            var expected = new List<PostDTOOutput> { posts[0], posts[2] };
            //Act
            var actual=((IQueryable<PostDTOOutput>)((ObjectResult)controller.GetUserPosts(in_authorID).Result).Value).ToList();
           

            //Asset
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }


        [Theory]
        [InlineData(0, "Konrad Gaweda", 1, 1, "MyTitle", "Content", 41, false, false)]
        [InlineData(1, "Jan Kowalski", 11, 2, "MyTitle1", "Conte nt1 ", 11, false, true)]
        [InlineData(4, "Jan Gawęda", 1, 4, "MyTitle2", " Co ntent2 ", 33, true, false)]
        [InlineData(7, "Konrad Kowalski", 32, 8, "MyTitle3", "Conten t3", 441, true, true)]

        public void GetById_Test(int in_id, string in_author, int in_authorID, int in_category,
            string in_title, string in_content, int in_likesCount, bool in_isLiked, bool in_isPromoted)
        {
            //Arrange
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetById(in_id, userID)).Returns(new ServiceResult<PostDTOOutput>(new PostDTOOutput
            {
                id = in_id,
                author = in_author,
                authorID = in_authorID,
                category = in_category,
                title = in_title,
                content = in_content,
                likesCount = in_likesCount,
                datetime = datetime1,
                isLikedByUser = in_isLiked,
                isPromoted = in_isPromoted
            }));

            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);

            var expected = new PostDTOOutput
            {
                id = in_id,
                author = in_author,
                authorID = in_authorID,
                category = in_category,
                title = in_title,
                content = in_content,
                likesCount = in_likesCount,
                datetime = datetime1,
                isLikedByUser = in_isLiked,
                isPromoted = in_isPromoted
            };

            //Act
            var actual = (PostDTOOutput)(((ObjectResult)controller.Get(userID, in_id).Result).Value);

            //Assert
            Assert.Equal(expected.id, actual.id);
            Assert.Equal(expected.author, actual.author);
            Assert.Equal(expected.authorID, actual.authorID);
            Assert.Equal(expected.category, actual.category);
            Assert.Equal(expected.title, actual.title);
            Assert.Equal(expected.content, actual.content);
            Assert.Equal(expected.likesCount, actual.likesCount);
            Assert.Equal(expected.datetime, actual.datetime);
            Assert.Equal(expected.isLikedByUser, actual.isLikedByUser);
            Assert.Equal(expected.isPromoted, actual.isPromoted);

        }
        [Theory]
        [InlineData(1)]
        public void GetPostCommentsTest(int postID)
        {

            int UserId = 1;
            var commentsList = new List<CommentDTOOutput>
            { new CommentDTOOutput{
                id=1,
                postId=postID,
                authorID=1,
                content="porzadny kontent",
                date=datetime1
            } ,
            new CommentDTOOutput{
                id=2,
                postId=postID,
                authorID=2,
                content="mniej porzadny kontent",
                date=datetime2
            } ,
            new CommentDTOOutput{
                id=3,
                postId=postID,
                authorID=1,
                content="slaby kontent",
                date=datetime3
            } ,
            };

            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetAllComments(postID, UserId)).Returns(new ServiceResult<IQueryable<CommentDTOOutput>>(commentsList.AsQueryable()));

            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);

            var expected = commentsList.AsQueryable();

            //Act
            var actual = ((IQueryable<CommentDTOOutput>)((ObjectResult)controller.GetPostComments(userID, postID).Result).Value).ToList();
            //Asset
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));


        }
        [Theory]
        [InlineData(1,1)]
        [InlineData(3,2)]
        [InlineData(5,0)]
        [InlineData(int.MaxValue,1)]
        public void DeletePost_Test(int userId, int postId)
        {
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.DeletePostAsync(postId)).Returns(Task.FromResult(new ServiceResult<bool>(true)));

            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);
            var result = (bool)((ObjectResult)controller.Delete(userId, postId).Result).Value;
            Assert.True(result);

        }
        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(2)]
        public void GetPostLikes_Test(int id)
        {
            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var idList = new List<int>();
            idList.Add(0);
            idList.Add(1);
            idList.Add(2);

            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetLikes(id)).Returns(new ServiceResult<IQueryable<LikerDTO>>(Mapper.Map(idList.AsQueryable())));
            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);

            var result = ((IQueryable<LikerDTO>)((ObjectResult)controller.GetPostLikes(id).Result).Value).ToList<LikerDTO>(); ;
            var expected = idList;
            Assert.True(expected.All(shouldItem => result.Any(isItem => isItem.id == shouldItem)));

        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(1, 4, false)]
        [InlineData(2, 3, true)]
        public void EditLikeStatus_Test(int userId, int id, bool lik)
        {
            LikeDTO like = new LikeDTO { like = lik };
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.EditLikeStatusAsync(userId, id, like)).Returns(Task.FromResult(new ServiceResult<bool>(true)));
            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);
            var actual = controller.EditLikeStatus(userId, id, like);
            var val = (bool)((ObjectResult)actual.Result).Value;
            Assert.True(val);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 2)]
        [InlineData(5, 0)]
        [InlineData(int.MaxValue, 1)]
        public void EditPost_Test(int userId, int postId)
        {
            PostDTOEdit body = new PostDTOEdit { content = "cokolwiek", category = 1, dateTime = new DateTime(1999, 12, 12, 12, 12, 12), isPromoted = true, title = "tytul" };
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.EditPostAsync(postId,body)).Returns(Task.FromResult(new ServiceResult<bool>(true)));

            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);
            var result = (bool)((ObjectResult)controller.Edit(userId, postId,body).Result).Value;
            Assert.True(result);

        }
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(int.MaxValue)]
        public void CreatePost_Test(int userId)
        {
            PostDTOEdit body = new PostDTOEdit { content = "cokolwiek", category = 1, dateTime = new DateTime(1999, 12, 12, 12, 12, 12), isPromoted = true, title = "tytul" };
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.AddPostAsync(body,userId)).Returns(Task.FromResult(new ServiceResult<int?>(0)));

            var mockLogger = new Mock<ILogger<PostController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            mockNewsletterService.Setup(x => x.SendNewsletterNotifications(It.IsAny<bool>(), It.IsAny<string>(), It.IsAny<int>()));
            var controller = new PostController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);
            var result = (int?)((ObjectResult)controller.Create( userId,body).Result).Value;
            Assert.True(result is int?);

        }
    }
}
