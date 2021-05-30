using AutoFixture;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WallProject.Models;
using WallProject.Models.MainView;
using WallProject.Services;
using WallProject.Services.Services_Interfaces;
using WallProject.Services.Serives_Implementations;
using Xunit;

namespace WallProjectTest.ServicesTest
{
    public class WallServiceTest
    {
       private  ServiceResult<UserViewModel> mockUserResult = new ServiceResult<UserViewModel>(new UserViewModel()
        {
            UserID = 1,
            IsActive = true,
            IsAdmin = false,
            IsEnterprenuer = false,
            IsVerified = true,
            Timestamp = DateTime.MinValue,
            UserEmail = "DSADASDAS",
            UserName = "name"
        });


       private ServiceResult<List<CategoryViewModel>> mockCategoryResult = new ServiceResult<List<CategoryViewModel>>(new List<CategoryViewModel>() { new CategoryViewModel()
       {
                CategoryID=1,CategoryName="nazwa"
       } });


        private ServiceResult<List<PostViewModel>>  mockPostListResult = new ServiceResult<List<PostViewModel>>(new List<PostViewModel>() { new PostViewModel()
        {
            Id=1,IsLikedByUser=true,
            IsPromoted=true,
            Category= "category", 
            Comments=new List<CommentViewModel>(),
            Content= "contetnt",
            CurrentUser=new UserViewModel(),
            Datetime=DateTime.MinValue,
            Likes=3,
            Owner=new UserViewModel(),
            OwnerName= "OwnerName",
            Title= "title"
        }});
        private ServiceResult<PostViewModel> mockPostResult = new ServiceResult<PostViewModel>( new PostViewModel()
        {
            Id=1,IsLikedByUser=true,
            IsPromoted=true,
            Category= "category",
            Comments=new List<CommentViewModel>(),
            Content= "contetnt",
            CurrentUser=new UserViewModel(),
            Datetime=DateTime.MinValue,
            Likes=3,
            Owner=new UserViewModel(),
            OwnerName= "OwnerName",
            Title= "title"
        });


        [Theory]
        [InlineData(1)]

        public void getWallTest(int userID)
        {

            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockUserService = new Mock<IUserService>();

           

            mockUserService.Setup(x => x.getById(userID)).Returns(Task.FromResult(mockUserResult));
            mockPostService.Setup(x => x.getAll(userID)).Returns(Task.Run(() => new ServiceResult<List<PostViewModel>>(new List<PostViewModel>(), HttpStatusCode.OK)));
            mockCategoryService.Setup(x => x.getAll()).Returns(Task.FromResult(mockCategoryResult));
            var service = new WallService(mockPostService.Object, mockUserService.Object, mockCategoryService.Object, mockCommentService.Object);
            var result = ((ServiceResult<WallViewModel>)(service.getWall(userID).Result)).Result;
            Assert.NotNull(result);


        }


        [Fact]
        public void ChangeCategoryFilterStatusTest()
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockUserService = new Mock<IUserService>();
            var service = new WallService(mockPostService.Object, mockUserService.Object, mockCategoryService.Object, mockCommentService.Object);

            mockCategoryService.Setup(x => x.getAll()).Returns(Task.FromResult(mockCategoryResult));
        }

        [Theory]
        [InlineData(1)]
        public void getUserPostsTest(int userID)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockUserService = new Mock<IUserService>();
            var service = new WallService(mockPostService.Object, mockUserService.Object, mockCategoryService.Object, mockCommentService.Object);
           
          
            mockUserService.Setup(x => x.getById(userID)).Returns(Task.FromResult(mockUserResult));
            mockPostService.Setup(x => x.getUserPosts(userID)).Returns(Task.FromResult(mockPostListResult));
            mockCategoryService.Setup(x => x.getAll()).Returns(Task.FromResult(mockCategoryResult));
            var result = ((ServiceResult<WallViewModel>)(service.getUserPosts(userID).Result)).Result;
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1)]
        public void getUserCommentsTest(int userID)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockUserService = new Mock<IUserService>();
            var service = new WallService(mockPostService.Object, mockUserService.Object, mockCategoryService.Object, mockCommentService.Object);

           
            mockUserService.Setup(x => x.getById(userID)).Returns(Task.FromResult(mockUserResult));
            mockCommentService.Setup(x => x.getByUserId(userID)).Returns(Task.Run(() => new ServiceResult<List<CommentViewModel>>(new List<CommentViewModel>(), HttpStatusCode.OK)));
            var result = ((ServiceResult<CommentListViewModel>)(service.getUserComments(userID).Result)).Result;
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1,2)]
        async public void getPostToEditTest(int userID, int postID)
        {
            var mockCommentService = new Mock<ICommentService>();
            var mockPostService = new Mock<IPostService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockUserService = new Mock<IUserService>();
            var service = new WallService(mockPostService.Object, mockUserService.Object, mockCategoryService.Object, mockCommentService.Object);
            mockUserService.Setup(x => x.getById(userID)).Returns(Task.FromResult(mockUserResult));
            mockCategoryService.Setup(x => x.getAll()).Returns(Task.FromResult(mockCategoryResult));
            mockPostService.Setup(x => x.getById(postID,userID)).Returns(Task.FromResult(mockPostResult));


        }
    }
}


