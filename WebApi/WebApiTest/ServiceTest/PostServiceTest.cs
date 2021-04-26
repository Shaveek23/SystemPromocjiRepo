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
using WebApi.Services;

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
            // arrange:
            var expected = posts;
            var mockIPostRepository = new Mock<IPostRepository>();
            mockIPostRepository.Setup(x => x.GetAll())
                .Returns(new ServiceResult<IQueryable<Post>>(posts.AsQueryable()));
            mockIPostRepository.Setup(x => x.GetLikes(It.IsAny<int>()))
                .Returns(new ServiceResult<IQueryable<PostLike>>(new List<PostLike>().AsQueryable()));

            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new ServiceResult<User>(new User()));

            var mockICommentService = new Mock<ICommentService>();

            var postService = new PostService(mockIPostRepository.Object, mockIUserRepository.Object, mockICommentService.Object);
            
            // act:
            var actual = postService.GetAll().Result.ToList();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetById_ValidCall()
        {
            var expectedId = 1;
            var expected = new ServiceResult<Post>(posts.Where(x => x.PostID == expectedId).FirstOrDefault());
            var mockIPostRepository = new Mock<IPostRepository>();
            mockIPostRepository.Setup(x => x.GetById(expectedId))
                .Returns(expected);
            mockIPostRepository.Setup(x => x.GetLikes(It.IsAny<int>()))
               .Returns(new ServiceResult<IQueryable<PostLike>>(new List<PostLike>().AsQueryable()));

            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new ServiceResult<User>(new User()));

            var mockICommentService = new Mock<ICommentService>();

            var postService = new PostService(mockIPostRepository.Object, mockIUserRepository.Object, mockICommentService.Object);
            var actual = postService.GetById(expectedId, 1).Result;
            var expected2 = expected.Result;

            Assert.True(actual != null);
            Assert.Equal(expected2.PostID, actual.id);
            //Assert.Equal(expected2.UserID, actual.authorID);
            Assert.Equal(expected2.Title, actual.title);
            Assert.Equal(expected2.Content, actual.content);
            Assert.Equal(expected2.IsPromoted, actual.isPromoted);
            Assert.Equal(expected2.Date, actual.datetime);
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
            mockIPostRepository.Setup(x => x.UpdateAsync(It.IsAny<Post>())).Returns(Task.Run(() => new ServiceResult<Post>(expectedPost)));

            var mockIUserRespository = new Mock<IUserRepository>();
            var mockICommentService = new Mock<ICommentService>();

            var postService = new PostService(mockIPostRepository.Object, mockIUserRespository.Object, mockICommentService.Object);
            var actual = postService.EditPostAsync(postID, newPostDTO).Result.Result;
            Assert.True(actual);

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
            mockIPostRepository.Setup(x => x.GetAllComments(postID)).Returns(new ServiceResult<IQueryable<Comment>>(commentsList.AsQueryable()));
            var mockIUserRespository = new Mock<IUserRepository>();
            var mockICommentService = new Mock<ICommentService>();
            mockICommentService.Setup(x => x.GetAll(It.IsAny<int>())).Returns(new ServiceResult<IQueryable<CommentDTOOutput>>(Mapper.MapOutput(commentsList.AsQueryable())));

            var postService = new PostService(mockIPostRepository.Object, mockIUserRespository.Object, mockICommentService.Object);
            var actual = postService.GetAllComments(postID, userId).Result.ToList();
            var expected = commentsList;
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

        }

    }
    
}
