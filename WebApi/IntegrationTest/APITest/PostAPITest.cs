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
    public class PostAPITest
    { 
        public PostAPI getPost(string content)
        {
            return new PostAPI {
                Content = content,
                Title = "interation_title",
                UserID = 1,
                Datetime = new DateTime(2008, 3, 1, 7, 0, 0),
                Category = 1,
                IsPromoted = false
            };
        }


        [Fact]
        public async void Post_ValidCall()
        {
            //POST
            var expectedPost = getPost("before edit");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //GET
            var beforePutGetRequestMessage = API.CreateRequest(HttpMethod.Get, $"post/{postID}");
            var beforePutGetResult = await API.client.SendAsync(beforePutGetRequestMessage);
            var beforePutJsonString = await beforePutGetResult.Content.ReadAsStringAsync();
            var beforePutPost = JsonConvert.DeserializeObject<PostDTO>(beforePutJsonString);

            //PUT
            var editedPost = getPost("edited");
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
            var PutResult = await API.client.SendAsync(PutRequestMessage);
            var PutJsonString = await PutResult.Content.ReadAsStringAsync();
            var isEdited = JsonConvert.DeserializeObject<bool>(PutJsonString);

            //GET
            var afterPutGetRequestMessage = API.CreateRequest(HttpMethod.Get, $"post/{postID}");
            var afterPutGetResult = await API.client.SendAsync(afterPutGetRequestMessage);
            var afterPutJsonString = await afterPutGetResult.Content.ReadAsStringAsync();
            var afterPutPost = JsonConvert.DeserializeObject<PostDTO>(afterPutJsonString);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            //GET
            var afterDeleteGetRequestMessage = API.CreateRequest(HttpMethod.Get, $"post/{postID}");
            var afterDeleteGetResult = await API.client.SendAsync(afterDeleteGetRequestMessage);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, beforePutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, afterDeleteGetResult.StatusCode);

            Assert.Equal(postID, beforePutPost.id);
            Assert.Equal(beforePutPost.content, expectedPost.Content);
            Assert.Equal(afterPutPost.content, editedPost.Content);
            Assert.True(isEdited);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void AllPosts_ValidCall()
        {
            //GET all
            var beforePostGetRequestMessage = API.CreateRequest(HttpMethod.Get, "posts");
            var beforePostGetResult = await API.client.SendAsync(beforePostGetRequestMessage);
            var beforePostJsonString = await beforePostGetResult.Content.ReadAsStringAsync();
            List<PostAPI> beforePostPosts = JsonConvert.DeserializeObject<List<PostAPI>>(beforePostJsonString);

            //POST
            var expectedPost = getPost("before edit");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //GET all
            var afterPostGetRequestMessage = API.CreateRequest(HttpMethod.Get, "posts");
            var afterPostGetResult = await API.client.SendAsync(afterPostGetRequestMessage);
            var afterPostJsonString = await afterPostGetResult.Content.ReadAsStringAsync();
            List<PostAPI> afterPostPosts = JsonConvert.DeserializeObject<List<PostAPI>>(afterPostJsonString);

            //PUT
            var editedPost = getPost("edited");
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
            var PutResult = await API.client.SendAsync(PutRequestMessage);
            var PutJsonString = await PutResult.Content.ReadAsStringAsync();
            var isEdited = JsonConvert.DeserializeObject<bool>(PutJsonString);

            //GET all
            var afterPutGetRequestMessage = API.CreateRequest(HttpMethod.Get, "posts");
            var afterPutGetResult = await API.client.SendAsync(afterPutGetRequestMessage);
            var afterPutJsonString = await afterPutGetResult.Content.ReadAsStringAsync();
            List<PostAPI> afterPutPosts = JsonConvert.DeserializeObject<List<PostAPI>>(afterPutJsonString);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            //GET all
            var afterDeleteGetRequestMessage = API.CreateRequest(HttpMethod.Get, "posts");
            var afterDeleteGetResult = await API.client.SendAsync(afterDeleteGetRequestMessage);
            var afterDeleteJsonString = await afterDeleteGetResult.Content.ReadAsStringAsync();
            List<PostAPI> afterDeletePosts = JsonConvert.DeserializeObject<List<PostAPI>>(afterDeleteJsonString);

            
            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterDeleteGetResult.StatusCode);

            Assert.NotEmpty(beforePostPosts);
            Assert.NotEmpty(afterPostPosts);
            Assert.NotEmpty(afterPutPosts);
            Assert.NotEmpty(afterDeletePosts);

            Assert.Equal(beforePostPosts.Count, afterDeletePosts.Count);
            Assert.Equal(afterPostPosts.Count, afterPutPosts.Count);
            Assert.Equal(beforePostPosts.Count + 1, afterPostPosts.Count);

        }

        [Fact]
        public async void PostPost_InvalidCall_NoContent()
        {
            //POST
            var expectedPost = getPost("content");
                expectedPost.Content = null;
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, PostResult.StatusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoTitle()
        {
            //POST
            var expectedPost = getPost("content");
                expectedPost.Title = null;
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, PostResult.StatusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoCatgory()
        {
            //POST
            var expectedPost = getPost("content");
            expectedPost.Category = null;
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, PostResult.StatusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoIsPromoted()
        {
            //POST
            var expectedPost = getPost("content");
            expectedPost.IsPromoted = null;
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, PostResult.StatusCode);
        }
        [Fact]
        public async void PostPost_InvalidCall_NoDatetime()
        {
            //POST
            var expectedPost = getPost("content");
            expectedPost.Datetime = null;
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, PostResult.StatusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoUserIdHeader()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            PostRequestMessage.Headers.Clear();
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var jsonString = await PostResult.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, PostResult.StatusCode);
        }

        [Fact]
        public async void DeletePost_InvalidCall_NoUserIdHeader()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //DELETE Invalid
            var InvalidDeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
                InvalidDeleteRequestMessage.Headers.Clear();
            var InvalidDeleteResult = await API.client.SendAsync(InvalidDeleteRequestMessage);

            //DELETE Valid
            var ValidDeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var ValidDeleteResult = await API.client.SendAsync(ValidDeleteRequestMessage);
            var ValidDeleteJsonString = await ValidDeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(ValidDeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, InvalidDeleteResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, ValidDeleteResult.StatusCode);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void DeletePost_InvalidCall_NoIdFound()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //DELETE Valid
            var ValidDeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var ValidDeleteResult = await API.client.SendAsync(ValidDeleteRequestMessage);
            var ValidDeleteJsonString = await ValidDeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(ValidDeleteJsonString);

            //DELETE Invalid
            var InvalidDeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var InvalidDeleteResult = await API.client.SendAsync(InvalidDeleteRequestMessage);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, ValidDeleteResult.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, InvalidDeleteResult.StatusCode);
            Assert.True(isDeleted);

        }

        [Fact]
        public async void PutPost_InvalidCall_NoTitle()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //PUT
            var editedPost = getPost("edited");
                editedPost.Title = null;
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
            var PutResult = await API.client.SendAsync(PutRequestMessage);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoContent()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //PUT
            var editedPost = getPost("edited");
            editedPost.Content = null;
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
            var PutResult = await API.client.SendAsync(PutRequestMessage);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoDatetime()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //PUT
            var editedPost = getPost("edited");
            editedPost.Datetime = null;
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
            var PutResult = await API.client.SendAsync(PutRequestMessage);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoCategory()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //PUT
            var editedPost = getPost("edited");
            editedPost.Category = null;
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
            var PutResult = await API.client.SendAsync(PutRequestMessage);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoIsPromoted()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //PUT
            var editedPost = getPost("edited");
            editedPost.IsPromoted = null;
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
            var PutResult = await API.client.SendAsync(PutRequestMessage);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoUserIdHeader()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //PUT
            var editedPost = getPost("edited");
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
                PutRequestMessage.Headers.Clear();
            var PutResult = await API.client.SendAsync(PutRequestMessage);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoIdFound()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            //PUT
            var editedPost = getPost("edited");
            var PutRequestMessage = API.CreateRequest(HttpMethod.Put, $"post/{postID}", editedPost);
            var PutResult = await API.client.SendAsync(PutRequestMessage);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);
            Assert.Equal(HttpStatusCode.InternalServerError, PutResult.StatusCode); //Czy tak powinno być?
            Assert.True(isDeleted);
        }

        [Fact]
        public async void GetPost_InvalidCall_NoIdFound()
        {
            //POST
            var expectedPost = getPost("content");
            var PostRequestMessage = API.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await API.client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);

            //DELETE
            var DeleteRequestMessage = API.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await API.client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            //GET
            var PutRequestMessage = API.CreateRequest(HttpMethod.Get, $"post/{postID}");
            var PutResult = await API.client.SendAsync(PutRequestMessage);

            Assert.Equal(HttpStatusCode.OK, PostResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, PutResult.StatusCode);
            Assert.True(isDeleted);
        }


    }
}
