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
                    new Post { PostID = 12, UserID = 18, CategoryID = 1, Date = new DateTime(2011, 12, 12), IsPromoted = true},
                    new Post { PostID = 2, UserID = 5, CategoryID = 1, Title= "Title32321", Date = new DateTime(2015, 6, 5), IsPromoted = false }
        };

        List<User> users = new List<User>
        {
            new User{UserID=1},
            new User{UserID=8},
            new User{UserID=5}
        };

        List<CommentDTOOutput> comments = new List<CommentDTOOutput>
        {
           new CommentDTOOutput() { id=1},
           new CommentDTOOutput() { id = 2, authorID = 2, postId = 2, date = DateTime.Today, content = "test2" },
           new CommentDTOOutput() { id = 3, authorID = 3, postId = 3, date = DateTime.Today, content = "test3"}
        };


        List<CategoryDTO> categories = new List<CategoryDTO>
        {
           new CategoryDTO() { ID=1, Name="Kategoria1"},
           new CategoryDTO() { ID = 2, Name = "Buciki"},
           new CategoryDTO() { ID = 3, Name = "Jedzonko"}
        };



        [Fact]
        public void GetAll_ValidCall()
        {
            int userID = 1;
            var CategoryId = 1;
            var expectedReturnedCategory = "RTV";
            // arrange:
            var expected = posts;
            var mockIPostRepository = new Mock<IPostRepository>();
            mockIPostRepository.Setup(x => x.GetAll())
                .Returns(new ServiceResult<IQueryable<Post>>(posts.AsQueryable()));
            mockIPostRepository.Setup(x => x.GetLikes(It.IsAny<int>()))
                .Returns(new ServiceResult<IQueryable<PostLike>>(new List<PostLike>().AsQueryable()));

            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetAll())
                .Returns(new ServiceResult<IQueryable<User>>((users.AsQueryable())));

            var mockICommentService = new Mock<ICommentService>();
            mockICommentService.Setup(x => x.GetAll(userID)).Returns(new ServiceResult<IQueryable<CommentDTOOutput>>(comments.AsQueryable()));

            var mockICategoryService = new Mock<ICategoryService>();
            mockICategoryService.Setup(x => x.GetById(CategoryId)).Returns(new ServiceResult<CategoryDTO>(new CategoryDTO { ID = CategoryId, Name = expectedReturnedCategory }));


            var postService = new PostService(mockIPostRepository.Object, mockIUserRepository.Object, mockICommentService.Object, mockICategoryService.Object);

            // act:

            var actual = postService.GetAll(userID).Result.ToList();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetById_ValidCall()
        {
            var userID = 1;
            var expectedId = 1;
            var CategoryId = 1;
            var expectedReturnedCategory = "RTV";
            var expected = new ServiceResult<Post>(posts.Where(x => x.PostID == expectedId).FirstOrDefault());
            var mockIPostRepository = new Mock<IPostRepository>();
            mockIPostRepository.Setup(x => x.GetById(expectedId))
                .Returns(expected);
            mockIPostRepository.Setup(x => x.GetLikes(It.IsAny<int>()))
               .Returns(new ServiceResult<IQueryable<PostLike>>(new List<PostLike>().AsQueryable()));

            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new ServiceResult<User>(new User() { UserID = userID }));

            var mockICommentService = new Mock<ICommentService>();
            mockICommentService.Setup(x => x.GetAll(userID)).Returns(new ServiceResult<IQueryable<CommentDTOOutput>>(comments.AsQueryable()));

            var mockICategoryService = new Mock<ICategoryService>();
            mockICategoryService.Setup(x => x.GetById(CategoryId)).Returns(new ServiceResult<CategoryDTO>(new CategoryDTO { ID = CategoryId, Name = expectedReturnedCategory }));

            var postService = new PostService(mockIPostRepository.Object, mockIUserRepository.Object, mockICommentService.Object, mockICategoryService.Object);
            var actual = postService.GetById(expectedId, 1).Result;
            var expected2 = expected.Result;

            Assert.True(actual != null);
            Assert.Equal(expected2.PostID, actual.id);
            Assert.Equal(expected2.Title, actual.title);
            Assert.Equal(expected2.Content, actual.content);
            Assert.Equal(expected2.IsPromoted, actual.isPromoted);
            Assert.Equal(expected2.Date, actual.datetime);
            Assert.Equal(expectedReturnedCategory, actual.category);
        }

        [Fact]
        public void EditPost_ValidCall()
        {
            int postID = 1;
            DateTime currentTime = new DateTime(2000, 10, 10, 10, 10, 42);
            var newPostDTO = new PostPutDTO
            {
                title = "newtitle",
                content = "newcontent",
                categoryID = 5,
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
            mockIPostRepository.Setup(x => x.UpdateAsync(It.IsAny<Post>(), It.IsAny<int>())).Returns(Task.Run(() => new ServiceResult<Post>(expectedPost)));

            var mockIUserRespository = new Mock<IUserRepository>();
            var mockICommentService = new Mock<ICommentService>();
            var mockICategoryService = new Mock<ICategoryService>();

            var postService = new PostService(mockIPostRepository.Object, mockIUserRespository.Object, mockICommentService.Object, mockICategoryService.Object);
            var actual = postService.EditPostAsync(postID, newPostDTO, 1).Result.Result;
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
            var mockICategoryService = new Mock<ICategoryService>();


            var postService = new PostService(mockIPostRepository.Object, mockIUserRespository.Object, mockICommentService.Object, mockICategoryService.Object);

            var actual = postService.GetAllComments(postID, userId).Result.ToList();
            var expected = commentsList;
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

        }

    }

}