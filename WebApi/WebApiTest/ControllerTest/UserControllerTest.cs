﻿using System;
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
using System.ComponentModel.DataAnnotations;
using WebApi.Database.Repositories.Implementations;
using WebApi.Services.Services_Implementations;
using WebApi.Models.DTO.UserDTOs;

namespace WebApiTest.ControllerTest
{
    public class UserControllerTest
    {
        public static readonly object[][] TestData =
        {
            new object[]{1, "iojestsuper@mini.pw.edu.pl", "student",new DateTime(2021,4,16,8,4,12),false,false,false },
            new object[]{1, "cokolwiek@ck.pl", "ktokolwiek", new DateTime(2021,2,6,4,2,12),false,true,false },
            new object[]{1, "xd@xd.pl", "jakikowliek", new DateTime(2020,1,11,5,4,12),true,false,false }
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Get_Test(int id, string email, string name, DateTime date, bool isVerified, bool isEnterprenuer, bool active)
        {
            //Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetById(0)).Returns(new ServiceResult<UserGetDTO>(new UserGetDTO
            {
                id = id,
                userEmail = email,
                userName = name,
                isVerified = isVerified,
                isActive = active,
                isEntrepreneur = isEnterprenuer
            }));
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            var controller = new UserController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);

            var expected = new UserGetDTO
            {
                id = id,
                userEmail = email,
                userName = name,
                isVerified = isVerified,
                isActive = active,
                isEntrepreneur = isEnterprenuer
            };

            //Act
            var actual = (ObjectResult)controller.Get(0).Result;
            int idActual = (int)((UserGetDTO)(actual.Value)).id;

            //Assert
            Assert.Equal(expected.id, idActual);

        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void GetAll_Test(int id, string email, string name, DateTime date, bool isVerified, bool isEnterprenuer, bool active)
        {
            List<UserGetDTO> people = new List<UserGetDTO>();
            people.Add(new UserGetDTO
            {
                id = id,
                userEmail = email,
                userName = name,
                isVerified = isVerified,
                isActive = active,
                isEntrepreneur = isEnterprenuer
            });

            people.Add(new UserGetDTO
            {
                id = id + 1,
                userEmail = "cko" + email,
                userName = name + "XD",
                isVerified = !isVerified,
                isActive = active,
                isEntrepreneur = !isEnterprenuer
            });

            //Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetAll()).Returns(new ServiceResult<IQueryable<UserGetDTO>>(people.AsQueryable()));
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            var controller = new UserController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);

            var expected = people;
            //Act
            var actual = ((IQueryable<UserGetDTO>)((ObjectResult)controller.GetAll().Result).Value);
            var val = actual.ToList();

            //Asset
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void AddUserDTO_Test(int id, string email, string name, DateTime date, bool isVerified, bool isEnterprenuer, bool active)
        {
            //Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.AddUserAsync(id, It.IsAny<UserPostDTO>())).Returns(Task.Run(() =>
            {
                return new ServiceResult<idDTO>( new idDTO { id = 1 });
            }));

            var mockLogger = new Mock<ILogger<UserController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            var controller = new UserController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);

            var expected = new UserPostDTO
            {
              
                userEmail = email,
                userName = name,
                isVerified = isVerified,
                isActive = active,
                isEntrepreneur = isEnterprenuer
            };

            //Act
            var actual = controller.AddUser(1, new UserPostDTO
            {
               
                userEmail = email,
                userName = name,
                isVerified = isVerified,
                isActive = active,
                isEntrepreneur = isEnterprenuer
            }).Result.Result;

            var val = (idDTO)((ObjectResult)actual).Value;
            Assert.True(val.id > 0);
           


        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]

        public void DeleteUserTest(int a_id, int u_id)
        {

            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var mockService = new Mock<IUserService>();


            var mockLogger = new Mock<ILogger<UserController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            var controller = new UserController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);
            mockService.Setup(x => x.DeleteUserAsync(a_id, u_id)).Returns(Task.FromResult(new ServiceResult<bool>(true)));
            var actual = controller.DeleteUser(a_id, u_id).Result.Result;
            var val = (bool)((ObjectResult)actual).Value;

            Assert.True(val);

        }


        [Theory]
        [InlineData(1, 1, 1, "test", "")]
        [InlineData(2, 2, 2, "test2", "konrad@gaw.pl")]
        [InlineData(3, 3, 3, "test3", "ckool@fsf.pl")]
        public void EditUser_Test(int a_id, int u_id, int p_id, string name, string email)
        {

            DateTime date = new DateTime(2008, 3, 1, 7, 0, 0);

            var mockService = new Mock<IUserService>();
            var expected = new UserPutDTO {   userEmail = email, userName = name ,isActive=true,isEntrepreneur=false,isAdmin=false,isVerified=true};
            mockService.Setup(x => x.EditUserAsync(a_id, expected, u_id)).Returns(Task.Run(() =>
             {
                 return new ServiceResult<bool>(true);

             }));
            var mockLogger = new Mock<ILogger<UserController>>();
            var mockNewsletterService = new Mock<INewsletterService>();
            var controller = new UserController(mockLogger.Object, mockService.Object, mockNewsletterService.Object);


            var actual = controller.EditUser(a_id, expected, u_id).Result;
            var val = (bool)((ObjectResult)actual.Result).Value;
            Assert.True(val);

        }


    }
}
