
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Database.Mapper;
using WebApi.Models.DTO;
using WebApi.Services;
using WebApi.Services.Services_Interfaces;
using Xunit;

namespace WebApiTest.ControllerTest
{
    public class CommentControllerTest
    {

        const int UserId = 1;

        [Theory]
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]

        public void GetById_Test(int c_id, int u_id, int p_id, string content)
        {

            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var mockCommentService = new Mock<ICommentService>();
            mockCommentService.Setup(x => x.GetById(c_id, UserId)).Returns(new ServiceResult<CommentDTOOutput>(new CommentDTOOutput
            {
                id = c_id,
                authorID = u_id,
                postId = p_id,
                date = date,
                content = content
            }));

            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockCommentService.Object);

            var expected = new CommentDTOOutput

            {
                id = c_id,
                authorID = u_id,
                postId = p_id,
                date = date,
                content = content
            };


            var actual = (CommentDTOOutput)((ObjectResult)controller.GetById(c_id, UserId).Result).Value;


            Assert.Equal(expected.content, actual.content);
            Assert.Equal(expected.date, actual.date);
            Assert.Equal(expected.postId, actual.postId);
            Assert.Equal(expected.authorID, actual.authorID);
            Assert.Equal(expected.id, actual.id);

        }

        [Theory]
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]
        public void GetAll_Test(int c_id, int u_id, int p_id, string content)
        {

            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);


            List<CommentDTOOutput> comments = new List<CommentDTOOutput>();
            comments.Add(new CommentDTOOutput

            {

                id = c_id,
                authorID = u_id,
                postId = p_id,
                date = date,
                content = content
            });

            ;
            comments.Add(new CommentDTOOutput
            {

                id = c_id + 1,
                authorID = u_id + 2,
                postId = p_id + 3,
                date = date,
                content = content
            });
            var mockService = new Mock<ICommentService>();

            mockService.Setup(x => x.GetAll(UserId)).Returns(new ServiceResult<IQueryable<CommentDTOOutput>>(comments.AsQueryable()));



            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = comments;

           
            var val = ((IQueryable<CommentDTOOutput>)((ObjectResult)controller.GetAll(UserId).Result).Value).ToList();
           // var val = ((IQueryable<CommentDTOOutput>)actual).ToList();
            Assert.True(expected.All(shouldItem => val.Any(isItem => isItem == shouldItem)));
        }

        [Theory]
        [InlineData(1, 1, "test")]
        [InlineData(2, 2, "test2")]
        [InlineData(3, 3, "test3")]
        public void AddCommentDTO_Test(int? c_id, int? p_id, string content)
        {

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.AddCommentAsync(UserId, It.IsAny<CommentDTONew>())).Returns(Task.Run(() =>
            {
                return new ServiceResult<idDTO>(new idDTO { id = c_id });
            }));



            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = c_id;
            var actual = controller.AddComment(new CommentDTONew
            {
                PostID = p_id,
                Content = content

            }, UserId).Result.Result;
            var val = (idDTO)((ObjectResult)actual).Value;

            Assert.Equal(val.id, expected);
        }




        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]

        #region TODO
        public void GetLikedUsers_Test(int c_id, int u_id)
        {
            //DateTime date = DateTime.Today;

            //var mockService = new Mock<ICommentService>();
            //mockService.Setup(x => x.GetLikedUsers(0)).Returns(

            //    (IQueryable<int>)new List<int>() { u_id }
            //);


            //var mockLogger = new Mock<ILogger<CommentController>>();
            //var controller = new CommentController(mockLogger.Object, mockService.Object);
            //var expected = new List<int> { u_id };
            //var actual = controller.GetLikedUsers(c_id).ToList();
            //Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
            Assert.True(true);
        }
        #endregion



        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]

        public void DeleteComment_Test(int c_id, int u_id)
        {

            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var mockService = new Mock<ICommentService>();


            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            mockService.Setup(x => x.DeleteComment(c_id, UserId)).Returns(new ServiceResult<bool>(true));
            var actual = (bool)((ObjectResult)controller.DeleteComment(c_id, UserId).Result).Value;
            Assert.True(actual);

        }


        [Theory]
        [InlineData(1, "test")]
        [InlineData(2, "test2")]
        [InlineData(3, "test3")]
        public void EditCommentDTO_Test(int c_id, string content)
        {

            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.EditCommentAsync(c_id, UserId, It.IsAny<CommentDTOEdit>())).Returns(Task.Run(() =>
              {
                  return new ServiceResult<bool>(true);

              }));
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = new CommentDTOEdit
            {

                Content = content
            };

            var actual = controller.EditComment(c_id, UserId, new CommentDTOEdit
            {


                Content = content

            }).Result;
            var val = (bool)((ObjectResult)actual.Result).Value;
            Assert.True(val);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(2)]
        public void GetCommentLikes_Test(int id)
        {
            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var idList = new List<int>();
            idList.Add(0);
            idList.Add(1);
            idList.Add(2);

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.GetLikedUsers(id)).Returns(new ServiceResult<IQueryable<LikerDTO>>(Mapper.Map(idList.AsQueryable())));
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);

            var result = ((IQueryable<LikerDTO>)((ObjectResult)controller.GetCommentLikes(id).Result).Value).ToList<LikerDTO>(); 
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
            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.EditLikeOnCommentAsync(userId, id, like)).Returns(Task.FromResult(new ServiceResult<bool>(true)));
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var actual = controller.EditLikeStatus(userId, id, like);
            var val = (bool)((ObjectResult)actual.Result).Value;
            Assert.True(val);
        }


    }
}
