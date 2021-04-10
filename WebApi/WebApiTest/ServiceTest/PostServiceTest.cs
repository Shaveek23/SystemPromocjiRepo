using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models.POCO;
using Xunit;
using Moq;
using Autofac.Extras.Moq;
using WebApi.Services.Services_Interfaces;
using WebApi.Database.Repositories.Interfaces;
using System.Linq;
using WebApi.Database;
using WebApi.Services.Serives_Implementations;
using WebApi.Database.Mapper;
using WebApi.Models.DTO.PostDTOs;
using System.Threading.Tasks;
using WebApi.Models.DTO;

namespace WebApiTest.ServiceTest
{
    public class PostServiceTest
    {
        List<Post> posts = new List<Post> {
                    new Post { PostID = 1, UserID = 1, CategoryID = 1, Title= "Title1", Content = "Content1 ", Date = new DateTime(2020, 7, 1), IsPromoted = false},
                    new Post { PostID = 31, UserID = 1, CategoryID = 1, Title= "Title2221", Content = "Content32 ", Date = new DateTime(2000, 5, 2), IsPromoted = false },
                    new Post { PostID = 4, UserID = 1, CategoryID = 1, Title= "Title1321321312312", Content = "Content656565 "},
                    new Post { PostID = 12, UserID = 8, CategoryID = 3, Date = new DateTime(2011, 12, 12), IsPromoted = true},
                    new Post { PostID = 2, UserID = 5, CategoryID = 5, Title= "Title32321", Date = new DateTime(2015, 6, 5), IsPromoted = false },
                    new Post()
        };

        [Fact]
        public void GetAll_ValidCall()
        {
            var expected = posts;
            var mockIPostRepository = new Mock<IPostRepository>();
            mockIPostRepository.Setup(x => x.GetAll())
                .Returns(posts.AsQueryable());

            var postService = new PostService(mockIPostRepository.Object);
            var actual = postService.GetAll().ToList();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetById_ValidCall()
        {
            var expectedId = 1;
            var expected = posts.Where(x => x.PostID == expectedId).FirstOrDefault();
            var mockIPostRepository = new Mock<IPostRepository>();
            mockIPostRepository.Setup(x => x.GetById(expectedId))
                .Returns(expected);

            var postService = new PostService(mockIPostRepository.Object);
            var actual = postService.GetById(expectedId);

            Assert.True(actual != null);
            Assert.Equal(expected.PostID, actual.id);
            Assert.Equal(expected.UserID, actual.authorID);
            Assert.Equal(expected.Title, actual.title);
            Assert.Equal(expected.Content, actual.content);
            Assert.Equal(expected.IsPromoted, actual.isPromoted);
            Assert.Equal(expected.Date, actual.datetime);
        }

        [Fact]
        public void EditPost_ValidCall()
        {
            int postID = 1;
            DateTime currentTime = new DateTime(2000, 10, 10, 10, 10, 42);
            var newPostDTO = new PostEditDTO
            {
                title = "newtitle",
                content = "newcontent",
                category = 5,
                dateTime = currentTime,
                isPromoted = true
            };
            var expectedPost = new Post
            {
                PostID = postID,
                UserID = 1,
                CategoryID = 5,
                Title = "newtitle",
                Content = "newcontent",
                Date = currentTime,
                IsPromoted = true,

            };

            var mockIPostRepository = new Mock<IPostRepository>();
            mockIPostRepository.Setup(x => x.UpdateAsync(It.IsAny<Post>())).Returns(Task.Run(() => expectedPost));


            var postService = new PostService(mockIPostRepository.Object);
            var actual = postService.EditPostAsync(postID, newPostDTO).Result;

            Assert.True(actual != null);
            Assert.Equal(actual.PostID, expectedPost.PostID);
            Assert.Equal(actual.UserID, expectedPost.UserID);
            Assert.Equal(actual.Title, expectedPost.Title);
            Assert.Equal(actual.Content, expectedPost.Content);
            Assert.Equal(actual.IsPromoted, expectedPost.IsPromoted);
            Assert.Equal(actual.Date, expectedPost.Date);
        }
        [Fact]
        public void GetAllComments_ValisCall()
        {
            int postID = 1;
            int userId = 1;
            var mockIPostRepository = new Mock<IPostRepository>();
            DateTime currentTime = new DateTime(2000, 10, 10, 10, 10, 42);
            var commentsList = new List<Comment>
            { new Comment{
                CommentID=1,
                PostID=postID,
                UserID=1,
                Content="porzadny kontent",
                DateTime=currentTime
            } ,
            new Comment{
                CommentID=2,
                PostID=postID,
                UserID=2,
                Content="mniej porzadny kontent",
                DateTime=currentTime
            } ,
            new Comment{
                CommentID=3,
                PostID=postID,
                UserID=1,
                Content="slaby kontent",
                DateTime=currentTime
            } ,
            };
            mockIPostRepository.Setup(x => x.GetAllComments(postID)).Returns(commentsList.AsQueryable());
            var postService = new PostService(mockIPostRepository.Object);
            var actual = postService.GetAllComments(postID, userId).ToList();
            var expected = commentsList;
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }

    }
    
}
