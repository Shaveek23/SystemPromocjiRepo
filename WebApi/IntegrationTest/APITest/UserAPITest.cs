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
    public class UserAPITest : APItester<UserAPI_get, UserAPI_post, UserAPI_put>
    {
        public UserAPI_post GetUserToPost(string userName = "userName")
        {
            return new UserAPI_post
            {
                userName = userName,
                userEmail = "damian@bis.pl",
                isAdmin = false,
                isEntrepreneur = false,
                isVerified = true,
                isActive = true
            };
        }
        public UserAPI_put GetUserToPut(string userName = "userName")
        {
            return new UserAPI_put
            {
                userName = userName,
                userEmail = "damian@bis.pl",
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
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");
            UserAPI_get userAfterPost, userAfterPut;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (userAfterPost, statusCode) = await Get($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            statusCode = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (userAfterPut, statusCode) = await Get($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"users/{userID}");
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            Assert.Equal(userID, userAfterPost.id);
            Assert.Equal(userToPost.userName, userAfterPost.userName);
            Assert.Equal(userToPut.userName, userAfterPut.userName);

        }

        [Fact]
        public async void AllUsers_ValidCall()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post postToPost = GetUserToPost("before edit");
            UserAPI_put postToPut = GetUserToPut("edited");
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
            statusCode = await Put($"users/{userID}", postToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (usersAfterPut, statusCode) = await GetAll("users");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (usersAfterDelete, statusCode) = await GetAll("users");
            Assert.Equal(HttpStatusCode.OK, statusCode);

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
            UserAPI_post post = GetUserToPost();

            //POST
            (userID, statusCode) = await Post("users", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"users/{userID}");
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
        [Fact]
        public async void GetUser_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post post = GetUserToPost();

            //POST
            (userID, statusCode) = await Post("users", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"users/{userID}", null);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void GetExistingUser_ValidCall()
        {
            HttpStatusCode statusCode;
            UserAPI_get user;
            //GET
            (user, statusCode) = await Get($"users/{existingUserID}", null);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.False(user.isAdmin);
        }
        [Fact]
        public async void GetNotOwner_ValidCall()
        {
            HttpStatusCode statusCode;
            UserAPI_get user;
            //GET
            (user, statusCode) = await Get($"users/{NotOwnerUserID}", null);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.False(user.isAdmin);
        }
        [Fact]
        public async void GetAdminUser_ValidCall()
        {
            HttpStatusCode statusCode;
            UserAPI_get user;
            //GET
            (user, statusCode) = await Get($"users/{AdminUserID}", null);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.True(user.isAdmin);
        }
        [Fact]
        public async void GetAllUser_ValidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            //GET
            (_, statusCode) = await GetAll($"users", null);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        #endregion

        #region POST
        [Fact]
        public async void PostUser_ValidCall()
        {
            UserAPI_post user = GetUserToPost();
            int userID;
            HttpStatusCode statusCode;

            //POST Invalid
            (userID, statusCode) = await Post("users", user);
            Assert.True(statusCode.IsOK());

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PostUser_InvalidCall_NoUserName()
        {
            //POST Invalid
            UserAPI_post user = GetUserToPost();
            int userID;
            HttpStatusCode statusCode;
            user.userName = null;
            (userID, statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"users/{userID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostUser_InvalidCall_NoUserEmail()
        {
            //POST Invalid
            UserAPI_post user = GetUserToPost();
            int userID;
            HttpStatusCode statusCode;
            user.userEmail = null;
            (userID, statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"users/{userID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostUser_InvalidCall_No_IsVerified_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUserToPost();
            int userID;
            HttpStatusCode statusCode;
            user.isVerified = null;
            (userID, statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"users/{userID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostUser_InvalidCall_No_IsActive_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUserToPost();
            int userID;
            HttpStatusCode statusCode;
            user.isActive = null;
            (userID, statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"users/{userID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostUser_InvalidCall_No_IsAdmin_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUserToPost();
            int userID;
            HttpStatusCode statusCode;
            user.isAdmin = null;
            (userID, statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"users/{userID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostUser_InvalidCall_No_IsEntrepreneur_Information()
        {
            //POST Invalid
            UserAPI_post user = GetUserToPost();
            int userID;
            HttpStatusCode statusCode;
            user.isEntrepreneur = null;
            (userID, statusCode) = await Post("user", user);
            Assert.False(statusCode.IsOK());
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"users/{userID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        #endregion

        #region DELETE
        [Fact]
        public async void DeleteUser_ValidCall()
        {
            UserAPI_post user = GetUserToPost();
            int userID;
            HttpStatusCode statusCode;

            //POST Invalid
            (userID, statusCode) = await Post("users", user);
            Assert.True(statusCode.IsOK());

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void DeleteUser_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post post = GetUserToPost();

            //POST
            (userID, statusCode) = await Post("users", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
        [Fact]
        public async void DeleteUser_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post post = GetUserToPost();

            //POST
            (userID, statusCode) = await Post("users", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}", null);
            Assert.False(statusCode.IsOK());

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void DeleteUser_ValidCall_Admin()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post post = GetUserToPost();

            //POST
            (userID, statusCode) = await Post("users", post, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}", AdminUserID);
            Assert.True(statusCode.IsOK());
            if(!statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"users/{userID}", existingUserID);
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void DeleteUser_ValidCall_NotAnOwner()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post post = GetUserToPost();

            //POST
            (userID, statusCode) = await Post("users", post, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}", NotOwnerUserID);
            Assert.False(statusCode.IsOK());

            //DELETE
            statusCode = await Delete($"users/{userID}", existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        #endregion

        #region PUT
        [Fact]
        public async void PutUser_InvalidCall_NoUserName()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");
            userToPut.userName = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutUser_InvalidCall_NoUserEmail()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");
            userToPut.userEmail = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutUser_InvalidCall_NoIsVerified()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");
            userToPut.isVerified = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutUser_InvalidCall_NoIsActive()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");
            userToPut.isActive = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutUser_InvalidCall_NoIsAdmin()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");
            userToPut.isAdmin = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutUser_InvalidCall_NoIsEntrepreneur()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");
            userToPut.isEntrepreneur = null;

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"users/{userID}", userToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutUser_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");

            //POST
            (userID, statusCode) = await Post("users", userToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"users/{userID}", userToPut, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutUser_InvalidCall_NotAnOwner()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");

            //POST
            (userID, statusCode) = await Post("users", userToPost, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"users/{userID}", userToPut, NotOwnerUserID);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}", existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutUser_InvalidCall_Admin()
        {
            HttpStatusCode statusCode;
            int userID;
            UserAPI_post userToPost = GetUserToPost("before edit");
            UserAPI_put userToPut = GetUserToPut("edited");

            //POST
            (userID, statusCode) = await Post("users", userToPost, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Valid
            statusCode = await Put($"users/{userID}", userToPut, AdminUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"users/{userID}", existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
#endregion

    }
}
