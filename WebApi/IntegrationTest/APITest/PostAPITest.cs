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
    public class PostAPITest : APItester<PostAPI_get, PostAPI_post, PostAPI_put>
    {
        public PostAPI_post GetPostToPost(string content = "content")
        {
            return new PostAPI_post
            {
                title = "API title",
                content = content,
                category = 1
            };
        }
        public PostAPI_put GetPostToPut(string content = "content")
        {
            return new PostAPI_put
            {
                title = "API title",
                content = content,
                category = 1,
                isPromoted = false
            };
        }

        #region ALL
        [Fact]
        public async void SinglePost_ValidCall()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost("before edit");
            PostAPI_put postToPut = GetPostToPut("edited");
            PostAPI_get postAfterPost, postAfterPut;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (postAfterPost, statusCode) = await Get($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            statusCode = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (postAfterPut, statusCode) = await Get($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"post/{postID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            Assert.Equal(postID, postAfterPost.id);
            Assert.Equal(postToPost.content, postAfterPost.content);
            Assert.Equal(postToPut.content, postAfterPut.content);
        }

        [Fact]
        public async void AllPosts_ValidCall()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost("before edit");
            PostAPI_put postToPut = GetPostToPut("edited");
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
            statusCode = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (postsAfterPut, statusCode) = await GetAll("posts");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (postsAfterDelete, statusCode) = await GetAll("posts");
            Assert.Equal(HttpStatusCode.OK, statusCode);

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
        public async void GetPost_ValidCall()
        {
            HttpStatusCode statusCode;
            //GET
            (_, statusCode) = await Get($"post/{existingPostID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void GetPost_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post post = GetPostToPost();

            //POST
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"post/{postID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public async void GetPost_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post post = GetPostToPost();

            //POST
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"post/{postID}", null);
            Assert.False(statusCode.IsOK());

            //DELETE
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        #endregion

        #region POST
        [Fact]
        public async void PostPost_ValidCall()
        {
            int postID;
            HttpStatusCode statusCode;
            PostAPI_post post = GetPostToPost();
            //POST Invalid
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            //DELETE
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PostPost_InvalidCall_NoContent()
        {
            int postID;
            HttpStatusCode statusCode;
            PostAPI_post post = GetPostToPost();
            post.content = null;
            //POST Invalid
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"post/{postID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostPost_InvalidCall_NoTitle()
        {
            int postID;
            HttpStatusCode statusCode;
            PostAPI_post post = GetPostToPost();
            post.title = null;
            //POST Invalid
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"post/{postID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostPost_InvalidCall_NoCatgory()
        {
            int postID;
            HttpStatusCode statusCode;
            PostAPI_post post = GetPostToPost();
            post.category = null;
            //POST Invalid
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"post/{postID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            };
        }
        [Fact]
        public async void PostPost_InvalidCall_NoUserIdHeader()
        {
            int postID;
            HttpStatusCode statusCode;
            PostAPI_post post = GetPostToPost();
            //POST Invalid
            (postID, statusCode) = await Post("post", post, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"post/{postID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        #endregion

        #region DELETE
        [Fact]
        public async void DeletePost_ValidCall()
        {
            int postID;
            HttpStatusCode statusCode;
            PostAPI_post post = GetPostToPost();
            //POST Invalid
            (postID, statusCode) = await Post("post", post);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            //DELETE
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void DeletePost_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            statusCode = await Delete($"post/{postID}", null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void DeletePost_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }
        [Fact]
        public async void DeletePost_InvalidCall_NotAnOwner()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();

            //POST
            (postID, statusCode) = await Post("post", postToPost, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            statusCode = await Delete($"post/{postID}", NotOwnerUserID);
            Assert.Equal(HttpStatusCode.Forbidden, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}", existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void DeletePost_ValidCall_Admin()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();

            //POST
            (postID, statusCode) = await Post("post", postToPost, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}", AdminUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            if (!statusCode.IsOK())
            {
                //DELETE Valid
                statusCode = await Delete($"post/{postID}", existingUserID);
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }

        }
        #endregion

        #region PUT
        [Fact]
        public async void PutPost_ValidCall()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Valid
            statusCode = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutPost_InvalidCall_NoTitle()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();
            postToPut.title = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoContent()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();
            postToPut.content = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutPost_InvalidCall_NoCategory()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();
            postToPut.category = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutPost_InvalidCall_NoIsPromoted()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();
            postToPut.isPromoted = null;

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public async void PutPost_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"post/{postID}", postToPut, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutPost_InvalidCall_NotAnOwner()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();

            //POST
            (postID, statusCode) = await Post("post", postToPost, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"post/{postID}", postToPut, NotOwnerUserID);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}", existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutPost_InvalidCall_Admin()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();

            //POST
            (postID, statusCode) = await Post("post", postToPost, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Valid
            statusCode = await Put($"post/{postID}", postToPut, AdminUserID);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            if (!statusCode.IsOK())
            {
                //DELETE Valid
                statusCode = await Delete($"post/{postID}", existingUserID);
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }

        [Fact]
        public async void PutPost_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int postID;
            PostAPI_post postToPost = GetPostToPost();
            PostAPI_put postToPut = GetPostToPut();

            //POST
            (postID, statusCode) = await Post("post", postToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Valid
            statusCode = await Delete($"post/{postID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"post/{postID}", postToPut);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

        }
        #endregion
    }
}
