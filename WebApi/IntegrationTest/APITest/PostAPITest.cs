using IntegrationTest.APITest.Models;
using IntegrationTest.APITest.Models.Post;
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
    public class PostAPITest : APItester<PostAPI_get, PostAPI_post>
    {
        public PostAPI_post GetPost(string content = "content")
        {
            return new PostAPI_post
            {
                title = "API title",
                content = content,
                datetime = DateTime.Now,
                category = 1,
                isPromoted = true
            };
        }

        #region ALL
        [Fact]
        public async void GetPost_ValidCall()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isPuted, isDeleted;
            PostAPI_post postToPost = GetPost("before edit");
            PostAPI_post postToPut = GetPost("edited");
            PostAPI_get postAfterPost, postAfterPut;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (postAfterPost, statusCode) = await Get($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            (isPuted, statusCode) = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (postAfterPut, statusCode) = await Get($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"post/{postID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            Assert.Equal(postID, postAfterPost.id);
            Assert.Equal(postToPost.content, postAfterPost.content);
            Assert.Equal(postToPut.content, postAfterPut.content);
            Assert.True(isPuted);
            Assert.True(isDeleted);
        }

        [Fact]
        public async void GetAllPosts_ValidCall()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isPuted, isDeleted;
            PostAPI_post postToPost = GetPost("before edit");
            PostAPI_post postToPut = GetPost("edited");
            List<PostAPI_get> posts, postsAfterPost, postsAfterPut, postsAfterDelete;


            //GET ALL
            (posts, statusCode) = await GetAll("posts");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (postsAfterPost, statusCode) = await GetAll("posts");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            (isPuted, statusCode) = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (postsAfterPut, statusCode) = await GetAll("posts");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (postsAfterDelete, statusCode) = await GetAll("posts");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isPuted);
            Assert.True(isDeleted);

            Assert.NotEmpty(posts);
            Assert.NotEmpty(postsAfterPost);
            Assert.NotEmpty(postsAfterPut);
            Assert.NotEmpty(postsAfterDelete);

            Assert.Equal(posts.Count, postsAfterDelete.Count);
            Assert.Equal(postsAfterPost.Count, postsAfterPut.Count);
            Assert.Equal(posts.Count + 1, postsAfterPost.Count);
        }
        #endregion

        #region GET
        [Fact]
        public async void GetPost_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post post = GetPost();

            //POST
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"post/{postID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void GetPost_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post post = GetPost();

            //POST
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"post/{postID}", null);
            Assert.False(statusCode.IsOK());

            //DELETE
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }
        #endregion

        #region POST
        [Fact]
        public async void PostPost_InvalidCall_NoContent()
        {
            //POST Invalid
            PostAPI_post post = GetPost();
            post.content = null;
            (_, HttpStatusCode statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoTitle()
        {
            //POST Invalid
            PostAPI_post post = GetPost();
            post.title = null;
            (_, HttpStatusCode statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoCatgory()
        {
            //POST Invalid
            PostAPI_post post = GetPost();
            post.category = null;
            (_, HttpStatusCode statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoIsPromoted()
        {
            //POST Invalid
            PostAPI_post post = GetPost();
            post.isPromoted = null;
            (_, HttpStatusCode statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoDatetime()
        {
            //POST Invalid
            PostAPI_post post = GetPost();
            post.datetime = null;
            (_, HttpStatusCode statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async void PostPost_InvalidCall_NoUserIdHeader()
        {
            //POST Invalid
            PostAPI_post post = GetPost();
            (_, HttpStatusCode statusCode) = await Post("post", post, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
        #endregion

        #region DELETE
        [Fact]
        public async void DeletePost_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            (_, statusCode) = await Delete($"post/{postID}", null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void DeletePost_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            (_, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
            
            Assert.True(isDeleted);
        }
        #endregion

        #region PUT
        [Fact]
        public async void PutPost_InvalidCall_NoTitle()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();
            PostAPI_post postToPut = GetPost();
            postToPut.title = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoContent()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();
            PostAPI_post postToPut = GetPost();
            postToPut.content = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoDatetime()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();
            PostAPI_post postToPut = GetPost();
            postToPut.datetime = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoCategory()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();
            PostAPI_post postToPut = GetPost();
            postToPut.category = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoIsPromoted()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();
            PostAPI_post postToPut = GetPost();
            postToPut.isPromoted = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();
            PostAPI_post postToPut = GetPost();
            postToPut.title = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"post/{postID}", postToPut, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int postID;
            bool isDeleted;
            PostAPI_post postToPost = GetPost();
            PostAPI_post postToPut = GetPost();
            postToPut.title = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (isDeleted, statusCode) = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            Assert.True(isDeleted);
        }
        #endregion
    }
}
