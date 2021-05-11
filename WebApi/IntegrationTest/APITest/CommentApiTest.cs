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
    public class CommentApiTest : APItester<CommentAPI_get, CommentAPI_post>
    {
        public CommentAPI_post GetComment(string content = "contnet")
        {
            return new CommentAPI_post
            {
                postID = 1,
                content = content
            };
        }
        public LikeApi getLike()
        {
            return new LikeApi
            {
               like=true
            };
        }

        #region ALL
        [Fact]
        public async void GetComment_ValidCall()
        {
            HttpStatusCode statusCode;
            int commentID;
            bool isPuted, isDeleted;
            CommentAPI_post commentToPost = GetComment("before edit");
            CommentAPI_post commentToPut = GetComment("edited");
            CommentAPI_get commentAfterPost, commentAfterPut;

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (commentAfterPost, statusCode) = await Get($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT
            (isPuted, statusCode) = await Put($"comment/{commentID}", commentToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (commentAfterPut, statusCode) = await Get($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            Assert.Equal(commentID, commentAfterPost.id);
            Assert.Equal(commentToPost.content, commentAfterPost.content);
            Assert.Equal(commentToPut.content, commentAfterPut.content);
            Assert.True(isPuted);
            Assert.True(isDeleted);

        }
        
        [Fact]
        public async void GetAllComments_ValidCall()
        {
            HttpStatusCode statusCode;
            int commentID;
            bool isPuted, isDeleted;
            CommentAPI_post commentToPost = GetComment("before edit");
            CommentAPI_post commentToPut = GetComment("edited");
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
            (isPuted, statusCode) = await Put($"comment/{commentID}", commentToPut);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (commentsAfterPut, statusCode) = await GetAll("comments");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET ALL
            (commentsAfterDelete, statusCode) = await GetAll("comments");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isPuted);
            Assert.True(isDeleted);

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
        public async void GetComment_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int commentID;
            bool isDeleted;
            CommentAPI_post comment = GetComment();

            //POST
            (commentID, statusCode) = await Post("comment", comment);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE
            (isDeleted, statusCode) = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //GET
            (_, statusCode) = await Get($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            Assert.True(isDeleted);
        }
        #endregion

        #region POST
        [Fact]
        public async void PostComment_InvalidCall_NoContent()
        {
            //POST
            CommentAPI_post comment = GetComment();
            comment.content = null;
            (_, HttpStatusCode statusCode) = await Post("comment", comment);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
        [Fact]
        public async void PostComment_InvalidCall_NoPostID()
        {
            //POST
            CommentAPI_post comment = GetComment();
            comment.postID = null;
            (_, HttpStatusCode statusCode) = await Post("comment", comment);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }

        [Fact]
        public async void PostCommet_InvalidCall_NoUserIdHeader()
        {
            //POST
            CommentAPI_post comment = GetComment();
            (_, HttpStatusCode statusCode) = await Post("comment", comment, null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);
        }
        #endregion

        #region DELETE
        [Fact]
        public async void DeleteComment_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int commentID;
            bool isDeleted;
            CommentAPI_post commentToPost = GetComment();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            (_, statusCode) = await Delete($"comment/{commentID}", null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void DeletePost_InvalidCall_NoIdFound()
        {
            HttpStatusCode statusCode;
            int commentID;
            bool isDeleted;
            CommentAPI_post commentToPost = GetComment();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //DELETE Invalid
            (_, statusCode) = await Delete($"comment/{commentID}", null);
            Assert.Equal(HttpStatusCode.BadRequest, statusCode);

            Assert.True(isDeleted);
        }
        #endregion

        [Fact]
        public async void PutComment_InvalidCall_NoContent()
        {
            HttpStatusCode statusCode;
            int commentID;
            bool isDeleted;
            CommentAPI_post commentToPost = GetComment();
            CommentAPI_post commentToPut = GetComment();
            commentToPut.content = null;

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"commnet/{commentID}", commentToPut);
            Assert.False(statusCode.IsOK());

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutComment_InvalidCall_NoPostID()
        {
            HttpStatusCode statusCode;
            int commentID;
            bool isDeleted;
            CommentAPI_post commentToPost = GetComment();
            CommentAPI_post commentToPut = GetComment();
            commentToPut.postID = null;

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"commnet/{commentID}", commentToPut);
            Assert.False(statusCode.IsOK());

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

        [Fact]
        public async void PutComment_InvalidCall_NoUserIdHeader()
        {
            HttpStatusCode statusCode;
            int commentID;
            bool isDeleted;
            CommentAPI_post commentToPost = GetComment();
            CommentAPI_post commentToPut = GetComment();

            //POST
            (commentID, statusCode) = await Post("comment", commentToPost);
            Assert.Equal(HttpStatusCode.OK, statusCode);

            //PUT Invalid
            (_, statusCode) = await Put($"commnet/{commentID}", commentToPut, null);
            Assert.Equal(HttpStatusCode.NotFound, statusCode);

            //DELETE Valid
            (isDeleted, statusCode) = await Delete($"comment/{commentID}");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.True(isDeleted);
        }

    }
}
