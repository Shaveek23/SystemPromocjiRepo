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

namespace WebApiTest.ControllerTest
{
    public class PostControllerTest
    {
        int userID = 1;
        DateTime datetime1 = new DateTime(2020, 1, 1);
        DateTime datetime2 = new DateTime(3213123);
        DateTime datetime3 = new DateTime(2000, 10, 10, 11, 4, 41);



        [Theory]
        [InlineData(0, "Konrad Gaweda", 1, "RTV", "MyTitle", "Content", 41, false, false)]
        [InlineData(1, "Jan Kowalski", 11, "RTV", "MyTitle1", "Conte nt1 ", 11, false, true)]
        [InlineData(4, "Jan Gawęda", 1, "RTV", "MyTitle2", " Co ntent2 ", 33, true, false)]
        [InlineData(7, "Konrad Kowalski", 32, "RTV",  "MyTitle3", "Conten t3", 441, true, true)]
        public void GetAll_Test(int in_id, string in_author, int in_authorID, string in_category, 
            string in_title, string in_content, int in_likesCount, bool in_isLiked, bool in_isPromoted)
        {
            List<PostDTO> posts = new List<PostDTO>();
            posts.Add(new PostDTO
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

            posts.Add(new PostDTO
            {
                id = in_id + 1,
                author = in_author + " Second",
                authorID = in_authorID + 1,
                category = in_category + "321",
                title = in_title+ " One",
                content = in_content + " Three",
                likesCount = in_likesCount + 14,
                datetime = datetime2,
                isLikedByUser = in_isLiked = !in_isLiked,
                isPromoted = in_isPromoted = ! in_isPromoted
            });

            //Arrange
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetAll()).Returns(posts.AsQueryable());
            var mockLogger = new Mock<ILogger<PostController>>();
            var controller = new PostController(mockLogger.Object, mockService.Object);

            var expected = posts;

            //Act
            var actual = ((IQueryable<PostDTO>)((OkObjectResult)controller.GetAll(userID).Result).Value).ToList();
            //Asset
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }


        [Theory]
        [InlineData(0, "Konrad Gaweda", 1, "RTV", "MyTitle", "Content", 41, false, false)]
        [InlineData(1, "Jan Kowalski", 11, "RTV", "MyTitle1", "Conte nt1 ", 11, false, true)]
        [InlineData(4, "Jan Gawęda", 1, "RTV", "MyTitle2", " Co ntent2 ", 33, true, false)]
        [InlineData(7, "Konrad Kowalski", 32, "RTV", "MyTitle3", "Conten t3", 441, true, true)]
        public void GetAllOfUser_Test(int in_id, string in_author, int in_authorID, string in_category,
            string in_title, string in_content, int in_likesCount, bool in_isLiked, bool in_isPromoted)
        {
            List<PostDTO> posts = new List<PostDTO>();
            posts.Add(new PostDTO
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

            posts.Add(new PostDTO
            {
                id = in_id + 1,
                author = in_author + " Second",
                authorID = in_authorID + 1,
                category = in_category + "321",
                title = in_title + " One",
                content = in_content + " Three",
                likesCount = in_likesCount + 14,
                datetime = datetime2,
                isLikedByUser = !in_isLiked,
                isPromoted = !in_isPromoted
            });

            posts.Add(new PostDTO
            {
                id = in_id,
                author = in_author + " Second2",
                authorID = in_authorID,
                category = in_category + "3",
                title = in_title + " Onedsa",
                content = in_content + " Three",
                likesCount = in_likesCount + 4,
                datetime = datetime3,
                isLikedByUser = in_isLiked,
                isPromoted = !in_isPromoted
            });

            //Arrange
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetAllOfUser(in_authorID)).Returns(posts.Where(p=>p.authorID == in_authorID).AsQueryable());
            var mockLogger = new Mock<ILogger<PostController>>();
            var controller = new PostController(mockLogger.Object, mockService.Object);

            var expected = new List<PostDTO> { posts[0], posts[2] };
            //Act
            var actual = ((IQueryable<PostDTO>)((OkObjectResult)controller.GetUserPosts(in_authorID).Result).Value).ToList();

            //Asset
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }


        [Theory]
        [InlineData(0, "Konrad Gaweda", 1, "RTV", "MyTitle", "Content", 41, false, false)]
        [InlineData(1, "Jan Kowalski", 11, "RTV", "MyTitle1", "Conte nt1 ", 11, false, true)]
        [InlineData(4, "Jan Gawęda", 1, "RTV", "MyTitle2", " Co ntent2 ", 33, true, false)]
        [InlineData(7, "Konrad Kowalski", 32, "RTV", "MyTitle3", "Conten t3", 441, true, true)]

        public void GetById_Test(int in_id, string in_author, int in_authorID, string in_category,
            string in_title, string in_content, int in_likesCount, bool in_isLiked, bool in_isPromoted)
        {
            //Arrange
            var mockService = new Mock<IPostService>();
            mockService.Setup(x => x.GetById(in_id)).Returns(new PostDTO
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

            var mockLogger = new Mock<ILogger<PostController>>();
            var controller = new PostController(mockLogger.Object, mockService.Object);

            var expected = new PostDTO
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
            var actual = (PostDTO)(((OkObjectResult)controller.Get(userID, in_id).Result).Value);
            //var actual = ((IQueryable<PostDTO>)((OkObjectResult)controller.Get(userID, in_id).Result).Value).ToList();

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

        //TODO:
        //public void DeletePost_Test
        //public PostLikesDTO GetPostLikes
        //public void EditLikeStatus
    }
}
