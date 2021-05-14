using IntegrationTest.APITest.Models.Likes;
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

        [Fact]

        public async void PostLikeAndDislike_ValidCall()
        {
            HttpStatusCode statusCode;
            int postID = existingPostID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;
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
        }
        [Fact]
        public async void PostLikeAndDislike_InvalidCall_NotAnOwner()
        {
            HttpStatusCode statusCode;
            int postID = existingPostID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;
            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"post/{postID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());
            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);

            //DISLIKE Invalid:
            //PUT
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"post/{postID}/likedUsers", dislike, NotOwnerUserID);
            Assert.False(statusCode.IsOK());

            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);

            //DISLIKE Valid:
            //PUT
            statusCode = await Put($"post/{postID}/likedUsers", dislike, userID);
            Assert.False(statusCode.IsOK());

            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.DoesNotContain(likes, like => like.id == userID);
        }
        [Fact]
        public async void PostLikeAndDislike_InvalidCall_Admin()
        {
            HttpStatusCode statusCode;
            int postID = existingPostID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;
            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"post/{postID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());
            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);

            //DISLIKE Invalid:
            //PUT
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"post/{postID}/likedUsers", dislike, AdminUserID);
            Assert.False(statusCode.IsOK());
            if (!statusCode.IsOK())
            {
                // GET
                (likes, statusCode) = await GetAll($"post/{postID}/likedUsers");
                Assert.True(statusCode.IsOK());
                Assert.Contains(likes, like => like.id == userID);
                //DISLIKE Valid:
                //PUT
                statusCode = await Put($"post/{postID}/likedUsers", dislike, userID);
                Assert.False(statusCode.IsOK());
            }
            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.DoesNotContain(likes, like => like.id == userID);

        }
        [Fact]
        public async void PostLikeAndDislike_ValidCall_NoUserHeader()
        {
            HttpStatusCode statusCode;
            int postID = existingPostID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;
            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"post/{postID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());
            // GET
            (likes, statusCode) = await GetAll($"post/{postID}/likedUsers", null);
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);

            //DISLIKE:
            //PUT
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"post/{postID}/likedUsers", dislike, userID);
            Assert.True(statusCode.IsOK());
        }

        [Fact]
        public async void CommentLikeAndDislike_ValidCall()
        {
            HttpStatusCode statusCode;
            int commentID = existingCommentID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;
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
        }
        [Fact]
        public async void CommentLikeAndDislike_InvalidCall_NotAnOwner()
        {
            HttpStatusCode statusCode;
            int commentID = existingCommentID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;
            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"comment/{commentID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());
            // GET
            (likes, statusCode) = await GetAll($"comment/{commentID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);

            //DISLIKE Invalid:
            //PUT
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"comment/{commentID}/likedUsers", dislike, NotOwnerUserID);
            Assert.False(statusCode.IsOK());

            // GET
            (likes, statusCode) = await GetAll($"comment/{commentID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);

            //DISLIKE Valid:
            //PUT
            statusCode = await Put($"comment/{commentID}/likedUsers", dislike, userID);
            Assert.True(statusCode.IsOK());

            // GET
            (likes, statusCode) = await GetAll($"comment/{commentID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.DoesNotContain(likes, like => like.id == userID);
        }
        [Fact]
        public async void CommentLikeAndDislike_InvalidCall_Admin()
        {
            HttpStatusCode statusCode;
            int commentID = existingCommentID;
            int userID = existingUserID;
            List<LikeAPI_get> likes;
            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"comment/{commentID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());
            // GET
            (likes, statusCode) = await GetAll($"comment/{commentID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.Contains(likes, like => like.id == userID);

            //DISLIKE Valid:
            //PUT
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"comment/{commentID}/likedUsers", dislike, AdminUserID);
            Assert.True(statusCode.IsOK());
            if(!statusCode.IsOK())
            {
                // GET
                (likes, statusCode) = await GetAll($"comment/{commentID}/likedUsers");
                Assert.True(statusCode.IsOK());
                Assert.Contains(likes, like => like.id == userID);
                //DISLIKE Valid:
                //PUT
                statusCode = await Put($"comment/{commentID}/likedUsers", dislike, userID);
                Assert.True(statusCode.IsOK());
            }

            // GET
            (likes, statusCode) = await GetAll($"comment/{commentID}/likedUsers");
            Assert.True(statusCode.IsOK());
            Assert.DoesNotContain(likes, like => like.id == userID);
        }
        [Fact]
        public async void CommentLikeAndDislike_InvalidCall_NoUserHeader()
        {
            HttpStatusCode statusCode;
            int commentID = existingCommentID;
            int userID = existingUserID;
            // LIKE:
            // PUT
            LikeAPI_put like = getLikeToPut(true);
            statusCode = await Put($"comment/{commentID}/likedusers", like, userID);
            Assert.True(statusCode.IsOK());

            //DISLIKE:
            //PUT Invalid
            LikeAPI_put dislike = getLikeToPut(false);
            statusCode = await Put($"comment/{commentID}/likedUsers", dislike, null);
            Assert.True(statusCode.IsOK());

            //PUT VALID
            statusCode = await Put($"comment/{commentID}/likedUsers", dislike, userID);
            Assert.True(statusCode.IsOK());
        }

    }
}
