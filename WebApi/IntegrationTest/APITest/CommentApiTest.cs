using IntegrationTest.APITest.Models;
using IntegrationTest.APITest.Models.Comment;
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
    public class CommentApiTest : APItester<CommentAPI_get, CommentAPI_post, CommentAPI_put>
    {
        public CommentAPI_post GetCommentToPost(string content = "content")
        {
            return new CommentAPI_post
            {
                postID = 1,
                content = content
            };
        }
        public CommentAPI_put GetCommentToPut(string content = "content")
        {
            return new CommentAPI_put
            {
                content = content
            };
        }

        #region ALL
        [Fact]
        public async void Single_ValidCall()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost("before edit");
            CommentAPI_put commentToPut = GetCommentToPut("edited");
            CommentAPI_get commentAfterPost, commentAfterPut;

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (commentAfterPost, statusCode) = await Get($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            statusCode = await Put($"comment/{commentID}", commentToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (commentAfterPut, statusCode) = await Get($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            Assert.Equal(commentID, commentAfterPost.id);
            Assert.Equal(commentToPost.content, commentAfterPost.content);
            Assert.Equal(commentToPut.content, commentAfterPut.content);

        }

        [Fact]
        public async void All_ValidCall()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost("before edit");
            CommentAPI_put commentToPut = GetCommentToPut("edited");
            List<CommentAPI_get> comments, commentsAfterPost, commentsAfterPut, commentsAfterDelete;


            //GET ALL
            (comments, statusCode) = await GetAll("comments");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (commentsAfterPost, statusCode) = await GetAll("comments");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            statusCode = await Put($"comment/{commentID}", commentToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (commentsAfterPut, statusCode) = await GetAll("comments");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (commentsAfterDelete, statusCode) = await GetAll("comments");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.NotEmpty(comments);
            Assert.NotEmpty(commentsAfterPost);
            Assert.NotEmpty(commentsAfterPut);
            Assert.NotEmpty(commentsAfterDelete);

            Assert.Equal(comments.Count, commentsAfterDelete.Count);
            Assert.Equal(commentsAfterPost.Count, commentsAfterPut.Count);
            Assert.Equal(comments.Count + 1, commentsAfterPost.Count);

        }
        #endregion

        /*
        [Fact]
        public async  void GetCommentLikes_ValidCall()
        {
            //POST
            var expectedComment = getComment("before edit");
            var CommentRequestMessage = CreateRequest(HttpMethod.Post, "comment", expectedComment);
            var CommentResult = await client.SendAsync(CommentRequestMessage);
            var CommentJsonString = await CommentResult.Content.ReadAsStringAsync();
            var commentId = JsonConvert.DeserializeObject<int>(CommentJsonString);

           

            //GET LIKES
            var beforePutLikesGetRequestMessage = CreateRequest(HttpMethod.Get, $"comment/{commentId}/likedUsers");
            var beforePutLikesGetResult = await client.SendAsync(beforePutLikesGetRequestMessage);
            var beforePutLikesJsonString = await beforePutLikesGetResult.Content.ReadAsStringAsync();
            List<LikeApi> beforePutLikes= JsonConvert.DeserializeObject<List<LikeApi>>(beforePutLikesJsonString);

            //PUT LIKE
           
            var PutRequestMessage = CreateRequest(HttpMethod.Put, $"comment/{commentId}/likedUsers", getLike());
            var PutResult = await client.SendAsync(PutRequestMessage);
            var PutJsonString = await PutResult.Content.ReadAsStringAsync();
            var isEdited = JsonConvert.DeserializeObject<bool>(PutJsonString);

            //GET LIKES
            var afterPutLikesGetRequestMessage = CreateRequest(HttpMethod.Get, $"comment/{commentId}/likedUsers");
            var afterPutLikesGetResult = await client.SendAsync(afterPutLikesGetRequestMessage);
            var afterPutLikesJsonString = await afterPutLikesGetResult.Content.ReadAsStringAsync();
            List<LikeApi> afterPutLikes = JsonConvert.DeserializeObject<List<LikeApi>>(afterPutLikesJsonString);

            //PUT LIKE
            
            var PutRequestMessageRe = CreateRequest(HttpMethod.Put, $"comment/{commentId}/likedUsers", getLike());
            var PutResultRe = await client.SendAsync(PutRequestMessageRe);
            var PutJsonStringReAdd = await PutResultRe.Content.ReadAsStringAsync();
            var isEditedReAdd = JsonConvert.DeserializeObject<bool>(PutJsonStringReAdd);
            //GET LIKES
            var afterRePutLikesGetRequestMessage = CreateRequest(HttpMethod.Get, $"comment/{commentId}/likedUsers");
            var afterRePutLikesGetResult = await client.SendAsync(afterRePutLikesGetRequestMessage);
            var afterRePutLikesJsonString = await afterRePutLikesGetResult.Content.ReadAsStringAsync();
            List<LikeApi> afterRePutLikes = JsonConvert.DeserializeObject<List<LikeApi>>(afterRePutLikesJsonString);


            //DELETE
            var DeleteRequestMessage = CreateRequest(HttpMethod.Delete, $"comment/{commentId}");
            var DeleteResult = await client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, CommentResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, beforePutLikesGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutLikesGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, PutResultRe.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterRePutLikesGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, DeleteResult.StatusCode);

            Assert.NotEmpty(afterPutLikes);
           

            Assert.Equal(beforePutLikes.Count, afterRePutLikes.Count);
            Assert.Equal(1,Math.Abs( beforePutLikes.Count- afterPutLikes.Count));
            Assert.Equal( 1, Math.Abs(afterPutLikes.Count- afterRePutLikes.Count));
        }
        */

        #region GET
        [Fact]
        public async void GetComment_ValidCall()
        {
            HttpStatusCode statusCode;
            CommentAPI_get comment;
            //GET
            (comment, statusCode) = await Get($"comment/{existingCommentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(existingCommentID, comment.id);
        }
        [Fact]
        public async void GetComment_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post comment = GetCommentToPost();

            //POST
            (commentID, statusCode) = await Post("comment", comment);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

        }
        #endregion

        #region POST
        [Fact]
        public async void PostComment_ValidCall()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PostComment_InvalidCall_NoContent()
        {
            HttpStatusCode statusCode;
            int commentID;
            //POST
            CommentAPI_post comment = GetCommentToPost();
            comment.content = null;
            (commentID, statusCode) = await Post("comment", comment);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            if(statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"comment/{commentID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostComment_InvalidCall_NoPostID()
        {
            HttpStatusCode statusCode;
            int commentID;
            //POST
            CommentAPI_post comment = GetCommentToPost();
            comment.postID = null;
            (commentID, statusCode) = await Post("comment", comment);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"comment/{commentID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        [Fact]
        public async void PostComment_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int commentID;
            //POST
            CommentAPI_post comment = GetCommentToPost();
            (commentID, statusCode) = await Post("comment", comment, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
            if (statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"comment/{commentID}");
                Assert.Equal(HttpStatusCode.OK, statusCode);
            }
        }
        #endregion

        #region DELETE
        [Fact]
        public async void DeleteComment_ValidCall()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void DeleteComment_InvalidCall_NotAnOwner()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            statusCode = await Delete($"comment/{commentID}", NotOwnerUserID);
            Assert.Equal(HttpStatusCode.Forbidden, statusCode);

            //DELETE Valid
            statusCode = await Delete($"comment/{commentID}", existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void DeleteComment_ValidCall_Admin()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost, existingUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            statusCode = await Delete($"comment/{commentID}", AdminUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);
            if (!statusCode.IsOK())
            {
                //DELETE
                statusCode = await Delete($"comment/{commentID}", existingUserID);
                Assert.Equal(HttpStatusCode.Forbidden, statusCode);
            }
        }
        [Fact]
        public async void DeleteComment_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            statusCode = await Delete($"comment/{commentID}", null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void DeleteComment_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Valid
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            statusCode = await Delete($"comment/{commentID}", null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
        #endregion

        #region PUT
        [Fact]
        public async void PutComment_InvalidCall_NoContent()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();
            CommentAPI_put commentToPut = GetCommentToPut();
            commentToPut.content = null;

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"comment/{commentID}", commentToPut);
            Assert.False(statusCode.IsOK());

            //DELETE Valid
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutComment_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();
            CommentAPI_put commentToPut = GetCommentToPut();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"comment/{commentID}", commentToPut, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutComment_InvalidCall_NotAnOwner()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();
            CommentAPI_put commentToPut = GetCommentToPut();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            statusCode = await Put($"comment/{commentID}", commentToPut, NotOwnerUserID);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        [Fact]
        public async void PutComment_ValidCall_Admin()
        {
            HttpStatusCode statusCode;
            int commentID;
            CommentAPI_post commentToPost = GetCommentToPost();
            CommentAPI_put commentToPut = GetCommentToPut();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Valid
            statusCode = await Put($"comment/{commentID}", commentToPut, AdminUserID);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Valid
            statusCode = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
        #endregion
    }
}
