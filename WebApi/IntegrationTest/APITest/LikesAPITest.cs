using IntegrationTest.APITest.Models;
using IntegrationTest.APITest.Models.Comment;
using IntegrationTest.APITest.Models.Likes;
using IntegrationTest.APITest.Models.Post;
using IntegrationTest.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.APITest
{
    public class LikesAPITest : APItester<LikeAPI_get, LikeAPI_post, LikeAPI_put>
    {
        public LikeAPI_put getLikeToPut(bool like)
        {
            return new LikeAPI_put
            {
                like = like
            };
        }

        public PostAPI_post GetPostToPost(string content = "content")
        {
            return new PostAPI_post
            {
                title = "API title",
                content = content,
                categoryID = existingCategoryID
            };
        }
        public CommentAPI_post GetCommentToPost(string content = "content")
        {
            return new CommentAPI_post
            {
                postID = existingPostID,
                content = content
            };
        }

        public async Task<(int, HttpStatusCode)> PostPost(string requestUri, PostAPI_post obj, int? userID = 0)
        {
            var requestMessage = CreateRequest(HttpMethod.Post, requestUri, obj, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
                return (default(int), responseMessage.StatusCode);
            responseID response = JsonConvert.DeserializeObject<responseID>(jsonString);
            return (response.id, responseMessage.StatusCode);
        }

        public async Task<(int, HttpStatusCode)> PostComment(string requestUri, CommentAPI_post obj, int? userID = 0)
        {
            var requestMessage = CreateRequest(HttpMethod.Post, requestUri, obj, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
                return (default(int), responseMessage.StatusCode);
            responseID response = JsonConvert.DeserializeObject<responseID>(jsonString);
            return (response.id, responseMessage.StatusCode);
        }

        [Fact]

        public async void PostLikeAndDislike_ValidCall()
        {
            HttpStatusCode statusCode;
            int userID = existingUserID;
            List<LikeAPI_get> likes;
            int postID;

            PostAPI_post post = GetPostToPost();
            (postID, statusCode) = await PostPost("/post", post);
            Assert.True(statusCode.IsOK());

            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"post/{postID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());
            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);

            //DISLIKE:
            //PUT
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"post/{postID}/likedUsers", dislike, userID);
            Assert.True(statusCode.IsOK());

            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.DoesNotContain(likes, like => like.id == userID);

            statusCode = await Delete($"/post/{postID}");
            Assert.True(statusCode.IsOK());
        }
        [Fact]
        public async void PostLikeAndDislike_ValidCall_NoUserHeader()
        {
            HttpStatusCode statusCode;
            int postID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;

            PostAPI_post post = GetPostToPost();
            (postID, statusCode) = await PostPost("/post", post);
            Assert.True(statusCode.IsOK());

            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"post/{postID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());
            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers", null);
            Assert.False(statusCode.IsOK());
            Assert.Null(likes);

            //DISLIKE:
            //PUT
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"post/{postID}/likedUsers", dislike, userID);
            Assert.True(statusCode.IsOK());

            statusCode = await Delete($"/post/{postID}");
            Assert.True(statusCode.IsOK());
        }

        [Fact]
        public async void CommentLikeAndDislike_ValidCall()
        {
            HttpStatusCode statusCode;
            int commentID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;

            CommentAPI_post comment = GetCommentToPost();
            (commentID, statusCode) = await PostComment("/comment", comment);
            Assert.True(statusCode.IsOK());

            // LIKE:
            // PUT

            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"comment/{commentID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());
            // GET
            (likes, statusCode) = await GetAll($"comment/{commentID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);
            
            //DISLIKE:
            //PUT
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"comment/{commentID}/likedUsers", dislike, userID);
            Assert.True(statusCode.IsOK());

            // GET
            (likes, statusCode) = await GetAll($"comment/{commentID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.DoesNotContain(likes, like => like.id == userID);

            statusCode = await Delete($"/comment/{commentID}");
            Assert.True(statusCode.IsOK());
        }
        [Fact]
        public async void CommentLikeAndDislike_InvalidCall_NoUserHeader()
        {
            HttpStatusCode statusCode;
            int commentID;
            int userID = existingUserID;

            CommentAPI_post comment = GetCommentToPost();
            (commentID, statusCode) = await PostComment("/comment", comment);
            Assert.True(statusCode.IsOK());

            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"comment/{commentID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());

            //DISLIKE:
            //PUT Invalid
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"comment/{commentID}/likedUsers", dislike, null);
            Assert.False(statusCode.IsOK());

            //PUT VALID
            statusCode = await Put($"comment/{commentID}/likedUsers", dislike, userID);
            Assert.True(statusCode.IsOK());

            statusCode = await Delete($"/comment/{commentID}");
            Assert.True(statusCode.IsOK());
        }

    }
}
