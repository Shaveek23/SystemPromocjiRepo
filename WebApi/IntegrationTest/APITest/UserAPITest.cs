using IntegrationTest.APITest.Models;
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
    public class UserAPITest
    {

        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://webapi20210317153051.azurewebsites.net/")
        };

        public UserApi getUser(string userName)
        {
            return new UserApi
            {
                UserName = userName,
                UserEmail = "damian@bis.pl",
                Timestamp = new DateTime(2008, 3, 1, 7, 0, 0),
                IsAdmin = false,
                IsEntrepreneur = false,
                IsVerified = true,
                IsActive = true
            };
        }
        public LikeApi getLike()
        {
            return new LikeApi
            {
                like = true
            };
        }

        [Fact]
        public async void User_ValidCall()
        {
            //POST
            var expectedUser = getUser("created user");
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);
            var UserJsonString = await UserResult.Content.ReadAsStringAsync();
            var UserId = JsonConvert.DeserializeObject<int>(UserJsonString);

            //GET
            var beforePutGetRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, $"users/{UserId}");
            var beforePutGetResult = await client.SendAsync(beforePutGetRequestMessage);
            var beforePutJsonString = await beforePutGetResult.Content.ReadAsStringAsync();
            var beforePutUser = JsonConvert.DeserializeObject<UserDTO>(beforePutJsonString);

            //PUT
            var editedUser = getUser("edited username");
            var PutRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Put, $"users/{UserId}", editedUser);
            var PutResult = await client.SendAsync(PutRequestMessage);
            var PutJsonString = await PutResult.Content.ReadAsStringAsync();
            var isEdited = JsonConvert.DeserializeObject<bool>(PutJsonString);

            //GET
            var afterPutGetRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, $"users/{UserId}");
            var afterPutGetResult = await client.SendAsync(afterPutGetRequestMessage);
            var afterPutJsonString = await afterPutGetResult.Content.ReadAsStringAsync();
            var afterPutUser = JsonConvert.DeserializeObject<UserDTO>(afterPutJsonString);

            //DELETE
            var DeleteRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Delete, $"users/{UserId}");
            var DeleteResult = await client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);
            //GET
            var afterDeleteGetRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, $"users/{UserId}");
            var afterDeleteGetResult = await client.SendAsync(afterDeleteGetRequestMessage);

            Assert.Equal(HttpStatusCode.OK, UserResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, beforePutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, afterDeleteGetResult.StatusCode);

            Assert.Equal(UserId, beforePutUser.ID);
            Assert.Equal(beforePutUser.UserName, expectedUser.UserName);
            Assert.Equal(afterPutUser.UserName, editedUser.UserName);
            Assert.True(isEdited);
            Assert.True(isDeleted);

        }
        [Fact]
        public async void AllUsers_ValidCall()
        {
            //Getall
            var beforePostGetRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, "users");
            var beforePostGetResult = await client.SendAsync(beforePostGetRequestMessage);
            var beforePostJsonString = await beforePostGetResult.Content.ReadAsStringAsync();
            List<UserDTO> beforePostUsers = JsonConvert.DeserializeObject<List<UserDTO>>(beforePostJsonString);

            //POST
            var expectedUser = getUser("New user to list");
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);
            var UserJsonString = await UserResult.Content.ReadAsStringAsync();
            var UserId = JsonConvert.DeserializeObject<int>(UserJsonString);

            //Getall

            var afterPostGetRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, "users");
            var afterPostGetResult = await client.SendAsync(afterPostGetRequestMessage);
            var afterPostJsonString = await afterPostGetResult.Content.ReadAsStringAsync();
            List<UserDTO> afterPostUsers = JsonConvert.DeserializeObject<List<UserDTO>>(afterPostJsonString);

            //PUT
            var editedUser = getUser("edited");
            var PutRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Put, $"users/{UserId}", editedUser);
            var PutResult = await client.SendAsync(PutRequestMessage);
            var PutJsonString = await PutResult.Content.ReadAsStringAsync();
            var isEdited = JsonConvert.DeserializeObject<bool>(PutJsonString);

            //Getall

            var afterPutGetRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, "users");
            var afterPutGetResult = await client.SendAsync(afterPutGetRequestMessage);
            var afterPutJsonString = await afterPutGetResult.Content.ReadAsStringAsync();
            List<UserDTO> afterPutUsers = JsonConvert.DeserializeObject<List<UserDTO>>(afterPutJsonString);

            //DELETE
            var DeleteRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Delete, $"users/{UserId}");
            var DeleteResult = await client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            //Getall

            var afterDeleteGetRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, "users");
            var afterDeleteGetResult = await client.SendAsync(afterDeleteGetRequestMessage);
            var afterDeleteJsonString = await afterDeleteGetResult.Content.ReadAsStringAsync();
            List<UserDTO> afterDeleteUsers = JsonConvert.DeserializeObject<List<UserDTO>>(afterDeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, UserResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterDeleteGetResult.StatusCode);

            Assert.NotEmpty(beforePostUsers);
            Assert.NotEmpty(afterPostUsers);
            Assert.NotEmpty(afterPutUsers);
            Assert.NotEmpty(afterDeleteUsers);

            Assert.Equal(beforePostUsers.Count, afterDeleteUsers.Count);
            Assert.Equal(afterPostUsers.Count, afterPutUsers.Count);
            Assert.Equal(beforePostUsers.Count + 1, afterPostUsers.Count);
        }


        [Fact]
        public async void PostUser_InvalidCall_NoUserName()
        {
            //POST
            var expectedUser = getUser("SomeUsername");
            expectedUser.UserName = null;
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, UserResult.StatusCode);
        }

        [Fact]
        public async void PostUser_InvalidCall_NoUserEmail()
        {
            //POST
            var expectedUser = getUser("SomeUsername");
            expectedUser.UserEmail = null;
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, UserResult.StatusCode);
        }

        [Fact]
        public async void PostUser_InvalidCall_No_IsVerified_Information()
        {
            //POST
            var expectedUser = getUser("SomeUsername");
            expectedUser.IsVerified = null;
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, UserResult.StatusCode);
        }

        [Fact]
        public async void PostUser_InvalidCall_No_IsActive_Information()
        {
            //POST
            var expectedUser = getUser("SomeUsername");
            expectedUser.IsActive = null;
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, UserResult.StatusCode);
        }

        [Fact]
        public async void PostUser_InvalidCall_No_IsAdmin_Information()
        {
            //POST
            var expectedUser = getUser("SomeUsername");
            expectedUser.IsAdmin = null;
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, UserResult.StatusCode);
        }

        [Fact]
        public async void PostUser_InvalidCall_No_IsEntrepreneur_Information()
        {
            //POST
            var expectedUser = getUser("SomeUsername");
            expectedUser.IsEntrepreneur = null;
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, UserResult.StatusCode);
        }

        [Fact]
        public async void PostUser_InvalidCall_No_TimeStamp_Information()
        {
            //POST
            var expectedUser = getUser("SomeUsername");
            expectedUser.Timestamp = null;
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            var UserResult = await client.SendAsync(UserRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, UserResult.StatusCode);
        }


        [Fact]
        public async void PostUser_InvalidCall_NoUserIdHeader()
        {
            //POST
            var expectedUser = getUser("SomeUsername");
            var UserRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "users", expectedUser);
            UserRequestMessage.Headers.Clear();
            var UserResult = await client.SendAsync(UserRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, UserResult.StatusCode);
        }
    }
}
