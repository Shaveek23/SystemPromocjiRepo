using IntegrationTest.APITest.Models.User;
using IntegrationTest.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using WebApi.Models.DTO;
using Xunit;

namespace IntegrationTest.APITest
{
    public class UserAPITest : APItester<UserAPI_get, UserAPI_post>
    {
        public UserAPI_post GetUser(string userName = "userName")
        {
            return new UserAPI_post
            {
                userName = userName,
                userEmail = "damian@bis.pl",
                timestamp = DateTime.Now,
                isAdmin = false,
                isEntrepreneur = false,
                isVerified = true,
                isActive = true
            };
        }
        #region ALL
        [Fact]
        public async void User_ValidCall()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isPuted, isDeleted;
            UserAPI_post userToPost = GetUser("before edit");
            UserAPI_post userToPut = GetUser("edited");
            UserAPI_get userAfterPost, userAfterPut;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (userAfterPost, statusCode) = await Get($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            (isPuted, statusCode) = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (userAfterPut, statusCode) = await Get($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"users/{userID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            Assert.Equal(userID, userAfterPost.id);
            Assert.Equal(userToPost.userName, userAfterPost.userName);
            Assert.Equal(userToPut.userName, userAfterPut.userName);
            Assert.True(isPuted);
            Assert.True(isDeleted);

        }
        
        [Fact]
        public async void AllUsers_ValidCall()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isPuted, isDeleted;
            UserAPI_post postToPost = GetUser("before edit");
            UserAPI_post postToPut = GetUser("edited");
            List<UserAPI_get> users, usersAfterPost, usersAfterPut, usersAfterDelete;


            //GET ALL
            (users, statusCode) = await GetAll("users");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //POST
            (userID, statusCode) = await Post("users", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (usersAfterPost, statusCode) = await GetAll("users");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            (isPuted, statusCode) = await Put($"users/{userID}", postToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (usersAfterPut, statusCode) = await GetAll("users");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (usersAfterDelete, statusCode) = await GetAll("users");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isPuted);
            Assert.True(isDeleted);

            Assert.NotEmpty(users);
            Assert.NotEmpty(usersAfterPost);
            Assert.NotEmpty(usersAfterPut);
            Assert.NotEmpty(usersAfterDelete);

            Assert.Equal(users.Count, usersAfterDelete.Count);
            Assert.Equal(usersAfterPost.Count, usersAfterPut.Count);
            Assert.Equal(users.Count + 1, usersAfterPost.Count);
        }
        #endregion

        #region GET
        [Fact]
        public async void GetUser_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post post = GetUser();

            //POST
            (userID, statusCode) = await Post("users", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"users/{userID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void GetPost_ValidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post post = GetUser();

            //POST
            (userID, statusCode) = await Post("users", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"users/{userID}", null);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);


            Assert.True(isDeleted);
        }
        #endregion

        #region POST
        [Fact]
        public async void PostUser_InvalidCall_NoUserName()
        {
            //POST Invalid
            UserAPI_post user = GetUser();
            user.userName = null;
            (_, HttpStatusCode statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
        }

        [Fact]
        public async void PostUser_InvalidCall_NoUserEmail()
        {
            //POST Invalid
            UserAPI_post user = GetUser();
            user.userEmail = null;
            (_, HttpStatusCode statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
        }

        [Fact]
        public async void PostUser_InvalidCall_No_IsVerified_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUser();
            user.isVerified = null;
            (_, HttpStatusCode statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
        }

        [Fact]
        public async void PostUser_InvalidCall_No_IsActive_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUser();
            user.isActive = null;
            (_, HttpStatusCode statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
        }

        [Fact]
        public async void PostUser_InvalidCall_No_IsAdmin_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUser();
            user.isAdmin = null;
            (_, HttpStatusCode statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
        }

        [Fact]
        public async void PostUser_InvalidCall_No_IsEntrepreneur_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUser();
            user.isEntrepreneur = null;
            (_, HttpStatusCode statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
        }

        [Fact]
        public async void PostUser_InvalidCall_No_TimeStamp_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUser();
            user.timestamp = null;
            (_, HttpStatusCode statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
        }


        [Fact]
        public async void PostUser_InvalidCall_NoUserIdHeader()
        {
            //POST Invalid
            UserAPI_post user = GetUser();
            (_, HttpStatusCode statusCode) = await Post("user", user, null);
            Assert.False(statusCode.IsOK());
        }
        #endregion

        #region DELETE
        [Fact]
        public async void DeleteUser_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post post = GetUser();

            //POST
            (userID, statusCode) = await Post("users", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void DeleteUser_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post post = GetUser();

            //POST
            (userID, statusCode) = await Post("users", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Delete($"users/{userID}", null);
            Assert.False(statusCode.IsOK());

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }
        #endregion

        #region PUT
        [Fact]
        public async void PutUser_InvalidCall_NoUserName()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post userToPost = GetUser("before edit");
            UserAPI_post userToPut = GetUser("edited");
            userToPut.userName = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutUser_InvalidCall_NoUserEmail()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post userToPost = GetUser("before edit");
            UserAPI_post userToPut = GetUser("edited");
            userToPut.userEmail = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutUser_InvalidCall_NoIsVerified()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post userToPost = GetUser("before edit");
            UserAPI_post userToPut = GetUser("edited");
            userToPut.isVerified = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutUser_InvalidCall_NoIsActive()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post userToPost = GetUser("before edit");
            UserAPI_post userToPut = GetUser("edited");
            userToPut.isActive = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutUser_InvalidCall_NoIsAdmin()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post userToPost = GetUser("before edit");
            UserAPI_post userToPut = GetUser("edited");
            userToPut.isAdmin = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutUser_InvalidCall_NoIsEntrepreneur()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post userToPost = GetUser("before edit");
            UserAPI_post userToPut = GetUser("edited");
            userToPut.isEntrepreneur = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutUser_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int userID;
            bool isDeleted;
            UserAPI_post userToPost = GetUser("before edit");
            UserAPI_post userToPut = GetUser("edited");

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"users/{userID}", userToPut, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }
        #endregion
    }
}
