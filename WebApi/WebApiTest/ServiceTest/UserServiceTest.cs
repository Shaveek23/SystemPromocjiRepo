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
using WebApi.Services;
using WebApi.Models.DTO;
using System.Threading.Tasks;
using WebApi.Models.DTO.UserDTOs;

namespace WebApiTest
{
    public class UserServiceTest
    {
        List<User> users = new List<User>
        {
            new User {UserID=1, UserEmail="iojestsuper@mini.pw.edu.pl", UserName="student", Timestamp=new DateTime(2021,4,16,8,4,12),IsEnterprenuer=false,IsAdmin=false,IsVerified=false, Active=false },
            new User {UserID=1, UserEmail="cokolwiek@ck.pl", UserName="ktokolwiek", Timestamp=new DateTime(2021,2,6,4,2,12),IsEnterprenuer=false,IsAdmin=true,IsVerified=false, Active=true},
            new User {UserID=1, UserEmail="xd@xd.pl", UserName="jakikowliek", Timestamp=new DateTime(2020,1,11,5,4,12),IsEnterprenuer=true,IsAdmin=false,IsVerified=false , Active=true},
        };
        [Fact]
        public void GetAll_ValidCall()
        {
            var expected = users;
            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetAll())
                .Returns(new ServiceResult<IQueryable<User>>(users.AsQueryable()));

            var userService = new UserService(mockIUserRepository.Object);
            var actual = userService.GetAll().Result.ToList();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetById_ValidCall()
        {
            var expectedId = 1;
            var expected = users.Where(x => x.UserID == expectedId).FirstOrDefault();
            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetById(expectedId))
                .Returns(new ServiceResult<User>(expected));

            var userService = new UserService(mockIUserRepository.Object);
            var actual = UserMapper.Map(userService.GetById(expectedId).Result);

            Assert.True(actual != null);
            Assert.Equal(expected.UserID, actual.UserID);
            Assert.Equal(expected.UserName, actual.UserName);
            Assert.Equal(expected.UserEmail, actual.UserEmail);
            Assert.Equal(expected.Active, actual.Active);
            Assert.Equal(expected.IsEnterprenuer, actual.IsEnterprenuer);

        }

        //[Theory]
        //[InlineData(0)]
        public void GetById_InvalidCall_NoId(int expectedId)
        {
            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.GetById(expectedId))
                .Returns(new ServiceResult<User>(null, System.Net.HttpStatusCode.BadRequest, "Requested resource has not been found."));

            var userService = new UserService(mockIUserRepository.Object);
            var actual = userService.GetById(expectedId);

            Assert.Null(actual.Result);
            Assert.Equal(404, (int)actual.Code);
            Assert.Equal("Requested resource has not been found.", actual.Message);

        }

        [Fact]

        public void EditUser_ValidCall()
        {
            var newUserDTO = new UserPutDTO { userEmail = "iojestsuper@mini.pw.edu.pl", userName = "student", isEntrepreneur = false, isAdmin = false, isVerified = false, isActive = false };
            var newUser = new User { UserID = 1, UserEmail = "iojestsuper@mini.pw.edu.pl", UserName = "student", Timestamp = new DateTime(2021, 4, 16, 8, 4, 12), IsEnterprenuer = false, IsAdmin = false, IsVerified = false, Active = false };

            var mockIUserRepository = new Mock<IUserRepository>();
            mockIUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<int>())).Returns(Task.Run(() => new ServiceResult<User>(newUser)));



            var userService = new UserService(mockIUserRepository.Object);
            var actual = userService.EditUserAsync(newUser.UserID, newUserDTO, newUser.UserID).Result.Result;

            Assert.True(actual);

        }


    }
}