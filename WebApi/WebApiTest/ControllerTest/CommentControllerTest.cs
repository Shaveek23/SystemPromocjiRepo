
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

            var mockService = new Mock<ICommentService>();
            mockService.Setup(x => x.GetById(c_id, UserId)).Returns(new CommentDTOOutput

            {
                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            });

            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);

            var expected = new CommentDTOOutput

            {
                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            };


            var actual = (CommentDTOOutput)((OkObjectResult)controller.GetById(c_id, UserId).Result).Value;

            Assert.Equal(expected.Content, actual.Content);
            Assert.Equal(expected.DateTime, actual.DateTime);
            Assert.Equal(expected.PostID, actual.PostID);
            Assert.Equal(expected.UserID, actual.UserID);

            Assert.Equal(expected.CommentID, actual.CommentID);



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

                CommentID = c_id,
                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content
            });

            ;
            comments.Add(new CommentDTOOutput
            {

                CommentID = c_id + 1,

                UserID = u_id + 2,
                PostID = p_id + 3,
                DateTime = date,
                Content = content
            });
            var mockService = new Mock<ICommentService>();

            mockService.Setup(x => x.GetAll(UserId)).Returns(comments.AsQueryable());



            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = comments;

            var actual = ((OkObjectResult)controller.GetAll(UserId).Result).Value;
            var val = ((IQueryable<CommentDTOOutput>)actual).ToList();
            Assert.True(expected.All(shouldItem => val.Any(isItem => isItem == shouldItem)));

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
                return new CommentDTOOutput
                {
                    CommentID = c_id,
                    UserID = u_id,
                    PostID = p_id,
                    DateTime = date,
                    Content = content
                };
            }));



            var mockLogger = new Mock<ILogger<CommentController>>();
            var controller = new CommentController(mockLogger.Object, mockService.Object);
            var expected = c_id;
            ActionResult<int> actual = controller.AddComment(new CommentDTO
            {


                UserID = u_id,
                PostID = p_id,
                DateTime = date,
                Content = content

            }, UserId).Result;

            var res = ((OkObjectResult)actual.Result).Value;
            

            Assert.Equal(res, expected);
       


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
            mockService.Setup(x => x.DeleteComment(c_id, UserId)).Returns(true);
            var actual = controller.DeleteComment(c_id, UserId);
            Assert.Equal(typeof(OkResult), actual.GetType());

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
                  return new CommentDTOOutput
                  {
                      CommentID = c_id,
                      UserID = u_id,
                      PostID = p_id,
                      DateTime = date,
                      Content = content
                  };
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
            Assert.Equal(typeof(OkResult), actual.GetType());

        }
        

    }
}
