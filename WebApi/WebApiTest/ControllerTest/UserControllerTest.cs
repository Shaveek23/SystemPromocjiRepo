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

namespace WebApiTest.ControllerTest
{
    public class UserControllerTest
    {
        public static readonly object[][] TestData =
        {
            new object[]{1, "iojestsuper@mini.pw.edu.pl", "student",new DateTime(2021,4,16,8,4,12),false,false,false },
            new object[]{1, "cokolwiek@c321.pl", "ktokolwiek", new DateTime(2021,2,6,4,2,12),false,true,false },
            new object[]{1, "321321@x434344.pl", "jakikowliek", new DateTime(2020,1,11,5,4,12),true,false,false }
        };

        [Theory]
        [MemberData(nameof(TestData))]
        public void Get_Test(int id, string email, string name, DateTime date, bool isVerified, bool isEnterprenuer, bool active)
        {
            //Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetById(0)).Returns(new ServiceResult<UserDTO>(new UserDTO
            {
                id = id,
                userEmail = email,
                userName = name,
                timestamp = date,
                isVerified = isVerified,
                isActive = active,
                isEnterprenuer = isEnterprenuer
            }));
            var mockLogger = new Mock<ILogger<UserController>>();
            var controller = new UserController(mockLogger.Object, mockService.Object);

            var expected = new UserDTO
            {
                id = id,
                userEmail = email,
                userName = name,
                timestamp = date,
                isVerified = isVerified,
                isActive = active,
                isEnterprenuer = isEnterprenuer
            };

            //Act
            var actual = (ObjectResult)controller.Get(0).Result;
            int idActual = (int)((UserDTO)(actual.Value)).id;

            //Assert
            Assert.Equal(expected.id, idActual);

        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void GetAll_Test(int id, string email, string name, DateTime date, bool isVerified, bool isEnterprenuer, bool active)
        {
            List<UserDTO> people = new List<UserDTO>();
            people.Add(new UserDTO
            {
                id = id,
                userEmail = email,
                userName = name,
                timestamp = date,
                isVerified = isVerified,
                isActive = active,
                isEnterprenuer = isEnterprenuer
            });

            people.Add(new UserDTO
            {
                id = id + 1,
                userEmail = "cko" + email,
                userName = name + "1234",
                timestamp = date,
                isVerified = !isVerified,
                isActive = active,
                isEnterprenuer = !isEnterprenuer
            });

            //Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetAll()).Returns(new ServiceResult<IQueryable<UserDTO>>(people.AsQueryable()));
            var mockLogger = new Mock<ILogger<UserController>>();
            var controller = new UserController(mockLogger.Object, mockService.Object);

            var expected = people;
            //Act
            var actual = ((IQueryable<UserDTO>)((ObjectResult)controller.GetAll().Result).Value);
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
            mockService.Setup(x => x.AddUserAsync(id, It.IsAny<UserDTO>())).Returns(Task.Run(() =>
            {
                return new ServiceResult<int?>(id);
            }));

            var mockLogger = new Mock<ILogger<UserController>>();
            var controller = new UserController(mockLogger.Object, mockService.Object);

            var expected = new UserDTO
            {
                id = id,
                userEmail = email,
                userName = name,
                timestamp = date,
                isVerified = isVerified,
                isActive = active,
                isEnterprenuer = isEnterprenuer
            };

            //Act
            var actual = controller.AddUser(1, new UserDTO
            {
                id = id,
                userEmail = email,
                userName = name,
                timestamp = date,
                isVerified = isVerified,
                isActive = active,
                isEnterprenuer = isEnterprenuer
            }).Result.Result;

            var val = (int)((ObjectResult)actual).Value;
            //Assert
            Assert.Equal(expected.id, val);


        }


    }
}
