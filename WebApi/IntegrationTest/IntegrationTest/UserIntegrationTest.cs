using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Controllers;
using WebApi.Database;
using WebApi.Database.Repositories.Implementations;
using WebApi.Models.DTO;
using WebApi.Models.POCO;
using WebApi.Services.Serives_Implementations;
using WebApi.Services.Services_Implementations;
using Xunit;
using WebApi.Services.Hosted_Service;
using WebApi.Models.DTO.UserDTOs;

namespace IntegrationTest.IntegrationTest
{
    public class UserIntegrationTest
    {
        public UserController GetUserController(DbContextOptions<DatabaseContext> options)
        {
            var logger = new Mock<ILogger<UserController>>();
            var databaseContext = new DatabaseContext(options);
            var UserRepository = new UserRepository(databaseContext);
            var UserService = new UserService(UserRepository);
            var newsletterService = new Mock<INewsletterService>(); 
            var UserController = new UserController(logger.Object, UserService,newsletterService.Object);
            return UserController;
        }



        [Fact]
        public void GetUser_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetUser_ValidCall").Options;
            var expectedUser = new User() { UserID = 1, UserName = "userName", UserEmail = "test@eamil.com", Active = true, IsAdmin = false, IsEnterprenuer = false, IsVerified = false, Timestamp = DateTime.Now };

            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(expectedUser);
                dbContext.SaveChanges();
            }

            var userController = GetUserController(options);

            var actual = (UserGetDTO)((ObjectResult)userController.Get(expectedUser.UserID).Result).Value;

            Assert.NotNull(actual);
            Assert.Equal(expectedUser.UserID, actual.id);
            Assert.Equal(expectedUser.UserName, actual.userName);
            Assert.Equal(expectedUser.UserEmail, actual.userEmail);
            Assert.Equal(expectedUser.IsAdmin, actual.isAdmin);
            Assert.Equal(expectedUser.Active, actual.isActive);
            Assert.Equal(expectedUser.IsEnterprenuer, actual.isEntrepreneur);
            Assert.Equal(expectedUser.IsVerified, actual.isVerified);
        }

        [Fact]
        public void GetUsers_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetUsers_ValidCall").Options;
            var expectedUsers = new List<User>{
                new User { UserID = 1, UserEmail = "email132@mini.pw.edu.pl", UserName = "student", Timestamp = new DateTime(2021, 4, 16, 8, 4, 12), IsEnterprenuer = false, IsAdmin = false, IsVerified = false },
                new User { UserID = 2, UserEmail = "321321312@ck.pl", UserName = "student2", Timestamp = new DateTime(2021, 2, 6, 4, 2, 12), IsEnterprenuer = false, IsAdmin = true, IsVerified = false },
                new User { UserID = 3, UserEmail = "dsadas@adsa.pl", UserName = "student2223", Timestamp = new DateTime(2020, 1, 11, 5, 4, 12), IsEnterprenuer = true, IsAdmin = false, IsVerified = false }
                };

            using (var dbContext = new DatabaseContext(options))
            {
                foreach (var user in expectedUsers)
                    dbContext.Add(user);
                dbContext.SaveChanges();
            }

            var userController = GetUserController(options);

            var actual = (IEnumerable<UserGetDTO>)((ObjectResult)userController.GetAll().Result).Value;
            var count = actual.Count();
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(expectedUsers.Count, count);
        }



        public UserPutDTO GetUserEditDTO()
        {
            return new UserPutDTO() { userEmail = "email132@mini.pw.edu.pl", userName = "student",isEntrepreneur = false, isAdmin = false, isVerified = false, isActive = true };
        }
        public UserPostDTO GetUserAddDTO()
        {
            return new UserPostDTO() { userEmail = "email132@mini.pw.edu.pl", userName = "student", isEntrepreneur = false, isAdmin = false, isVerified = false, isActive = true };
        }

        [Fact]
        public void AddUser_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "AddUser_ValidCall").Options;

            var userController = GetUserController(options);

            var userDTO = GetUserAddDTO();
            int userCreatorID = 1;
            var result = userController.AddUser(userCreatorID, userDTO).Result.Value;

            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Users.FirstOrDefault();

            Assert.NotNull(actual);
            Assert.Equal(userDTO.userName, actual.UserName);
            Assert.Equal(userDTO.userEmail, actual.UserEmail);
            Assert.Equal(userDTO.isAdmin, actual.IsAdmin);
            Assert.Equal(userDTO.isActive, actual.Active);
            Assert.Equal(userDTO.isEntrepreneur, actual.IsEnterprenuer);
            Assert.Equal(userDTO.isVerified, actual.IsVerified);
        }

        [Fact]
        public void EditUser_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "EditUser_ValidCall").Options;

            var users = new List<User>{
                new User { UserID = 1, UserEmail = "email132@mini.pw.edu.pl", UserName = "student", Timestamp = new DateTime(2021, 4, 16, 8, 4, 12), Active=true, IsEnterprenuer = false, IsAdmin = false, IsVerified = false },
                new User { UserID = 2, UserEmail = "321321312@ck.pl", UserName = "student2", Timestamp = new DateTime(2021, 2, 6, 4, 2, 12), Active=true, IsEnterprenuer = false, IsAdmin = true, IsVerified = false },
                new User { UserID = 3, UserEmail = "dsadas@adsa.pl", UserName = "student2223", Timestamp = new DateTime(2020, 1, 11, 5, 4, 12), Active=true, IsEnterprenuer = true, IsAdmin = false, IsVerified = false }
                };
            using (var dbContext = new DatabaseContext(options))
            {
                foreach (var newUser in users)
                    dbContext.Add(newUser);
                dbContext.SaveChanges();
            }

            var userController = GetUserController(options);
            int userEditorID = 1;
            int editedUserID = 2;
            var userDTO = GetUserEditDTO();

            var result = userController.EditUser(userEditorID, userDTO, editedUserID).Result;

            var databaseContext = new DatabaseContext(options);
            var user = databaseContext.Users.Where(x => x.UserID == editedUserID).FirstOrDefault();

        
            Assert.Equal(userDTO.userName, user.UserName);
            Assert.Equal(userDTO.userEmail, user.UserEmail);
            Assert.Equal(userDTO.isAdmin, user.IsAdmin);
            Assert.Equal(userDTO.isActive, user.Active);
            Assert.Equal(userDTO.isEntrepreneur, user.IsEnterprenuer);
            Assert.Equal(userDTO.isVerified, user.IsVerified);
        }

        [Fact]
        public void DeleteUser_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "DeleteUser_ValidCall").Options;

            var user = new User() { UserID = 1, UserEmail = "dsadasdsadasdas@mini.pw.edu.pl", UserName = "sadsa", Timestamp = new DateTime(2011, 4, 16, 8, 4, 12), IsEnterprenuer = false, IsAdmin = false, IsVerified = false, Active = true };

            using (var dbContext = new DatabaseContext(options))
            {
                dbContext.Add(user);
                dbContext.SaveChanges();
            }

            var userController = GetUserController(options);

            var result = userController.DeleteUser(1,1).Result.Value;

            var databaseContext = new DatabaseContext(options);
            var actual = databaseContext.Posts.FirstOrDefault();

            Assert.Null(actual);
        }

    }
}
