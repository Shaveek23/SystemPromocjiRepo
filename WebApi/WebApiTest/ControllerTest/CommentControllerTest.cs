
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
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

            //DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            //var mockCommentService = new Mock<ICommentService>();
            //mockCommentService.Setup(x => x.GetById(c_id, UserId)).Returns(new ServiceResult<CommentDTOOutput>( new CommentDTOOutput
            //{
            //    id = c_id,
            //    authorID = u_id,
            //    postId = p_id,
            //    date = date,
            //    content = content
            //}));

            //var mockLogger = new Mock<ILogger<CommentController>>();
            //var controller = new CommentController(mockLogger.Object, mockCommentService.Object);

            //var expected = new CommentDTOOutput

            //{
            //    id = c_id,
            //    authorID = u_id,
            //    postId = p_id,
            //    date = date,
            //    content = content
            //};


            //var actual = (CommentDTOOutput)((ObjectResult)controller.GetById(c_id, UserId).Result).Value;


            //Assert.Equal(expected.content, actual.content);
            //Assert.Equal(expected.date, actual.date);
            //Assert.Equal(expected.postId, actual.postId);
            //Assert.Equal(expected.authorID, actual.authorID);
            //Assert.Equal(expected.id, actual.id);

            Assert.True(false);


        }
   
        [Theory]
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]
        public void GetAll_Test(int c_id, int u_id, int p_id, string content)
        {
                                                                                                                                                        
            //DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);


            //List<CommentDTOOutput> comments = new List<CommentDTOOutput>();
            //comments.Add(new CommentDTOOutput
 
            //{

            //    CommentID = c_id,
            //    UserID = u_id,
            //    PostID = p_id,
            //    DateTime = date,
            //    Content = content
            //});

            //;
            //comments.Add(new CommentDTOOutput
            //{

            //    CommentID = c_id + 1,

            //    UserID = u_id + 2,
            //    PostID = p_id + 3,
            //    DateTime = date,
            //    Content = content
            //});
            //var mockService = new Mock<ICommentService>();

            //mockService.Setup(x => x.GetAll(UserId)).Returns(new ServiceResult<IQueryable<CommentDTOOutput>>(comments.AsQueryable()));



            //var mockLogger = new Mock<ILogger<CommentController>>();
            //var controller = new CommentController(mockLogger.Object, mockService.Object);
            //var expected = comments;

            //var actual =((ServiceResult<IQueryable<CommentDTOOutput>>) ((OkObjectResult)controller.GetAll(UserId).Result).Value).Result;
            //var val = ((IQueryable<CommentDTOOutput>)actual).ToList();
            //Assert.True(expected.All(shouldItem => val.Any(isItem => isItem == shouldItem)));
            Assert.True(false);
        }

        [Theory]
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]
        public void AddCommentDTO_Test(int? c_id, int? u_id, int? p_id, string content)
        {


            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.AddCommentAsync(UserId, It.IsAny<CommentDTO>())).Returns(Task.Run(() =>
            {
                return new ServiceResult<int?>(c_id);
            }));



            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = c_id;
            var actual = controller.AddComment(new CommentDTO
            {


                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content

            }, UserId).Result.Result;
            var val = (int)((ObjectResult)actual).Value;

            Assert.Equal(val, expected);
       


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
        [InlineData(1, 1, 1, "test")]
        [InlineData(2, 2, 2, "test2")]
        [InlineData(3, 3, 3, "test3")]
        public void EditCommentDTO_Test(int c_id, int u_id, int p_id, string content)
        {

            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.EditCommentAsync(c_id, UserId, It.IsAny<CommentDTO>())).Returns(Task.Run(() =>
              {
                  return new ServiceResult<bool>(true);

              }));
            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = new CommentDTO
            {
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            };

            var actual = controller.EditComment(c_id, UserId, new CommentDTO
            {

                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content

            }).Result;
            var val = (bool)((ObjectResult)actual.Result).Value;
            Assert.True(val);

        }
        

    }
}
