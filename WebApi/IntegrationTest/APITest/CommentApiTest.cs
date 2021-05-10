using IntegrationTest.APITest;
using IntegrationTest.APITest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using WebApi.Models.DTO;
using Xunit;


namespace IntegrationTest
{
    public class CommentApiTest
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://webapi20210317153051.azurewebsites.net/")
        };
        public CommentApi getComment(string content)
        {
            return new CommentApi
            {
                Content = content,
                UserID = 1,
                PostID = 1,
                DateTime= new DateTime(2008, 3, 1, 7, 0, 0)

            };
        }
        public LikeApi getLike()
        {
            return new LikeApi
            {
               like=true
            };
        }
     
        [Fact]
        public async void Comment_ValidCall()
        {
            //POST
            var expectedComment = getComment("before edit");
            var CommentRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Post, "comment", expectedComment);
            var CommentResult = await client.SendAsync(CommentRequestMessage);
            var CommentJsonString = await CommentResult.Content.ReadAsStringAsync();
            var commentId = JsonConvert.DeserializeObject<int>(CommentJsonString);

            //GET
            var beforePutGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, $"comment/{commentId}");
            var beforePutGetResult = await client.SendAsync(beforePutGetRequestMessage);
            var beforePutJsonString = await beforePutGetResult.Content.ReadAsStringAsync();
            var beforePutComment = JsonConvert.DeserializeObject<CommentDTOOutput>(beforePutJsonString);

            //PUT
            var editedComment= getComment("edited");
            var PutRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Put, $"comment/{commentId}", editedComment);
            var PutResult = await client.SendAsync(PutRequestMessage);
            var PutJsonString = await PutResult.Content.ReadAsStringAsync();
            var isEdited = JsonConvert.DeserializeObject<bool>(PutJsonString);

            //GET
            var afterPutGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, $"comment/{commentId}");
            var afterPutGetResult = await client.SendAsync(afterPutGetRequestMessage);
            var afterPutJsonString = await afterPutGetResult.Content.ReadAsStringAsync();
            var afterPutComment = JsonConvert.DeserializeObject<PostDTO>(afterPutJsonString);

            //DELETE
            var DeleteRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Delete, $"comment/{commentId}");
            var DeleteResult = await client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);
            //GET
            var afterDeleteGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, $"comment/{commentId}");
            var afterDeleteGetResult = await client.SendAsync(afterDeleteGetRequestMessage);

            Assert.Equal(HttpStatusCode.OK, CommentResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, beforePutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, afterDeleteGetResult.StatusCode);

            Assert.Equal(commentId, beforePutComment.id);
            Assert.Equal(beforePutComment.content, expectedComment.Content);
            Assert.Equal(afterPutComment.content, editedComment.Content);
            Assert.True(isEdited);
            Assert.True(isDeleted);

        }
        [Fact]
        public async void AllComments_ValidCall()
        {
            //Getall
           
            var beforePostGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, "comments");
            var beforePostGetResult = await client.SendAsync(beforePostGetRequestMessage);
            var beforePostJsonString = await beforePostGetResult.Content.ReadAsStringAsync();
            List<CommentApi> beforePostComments = JsonConvert.DeserializeObject<List<CommentApi>>(beforePostJsonString);
            //POST
            var expectedComment = getComment("before edit");
            var CommentRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Post, "comment", expectedComment);
            var CommentResult = await client.SendAsync(CommentRequestMessage);
            var CommentJsonString = await CommentResult.Content.ReadAsStringAsync();
            var commentId = JsonConvert.DeserializeObject<int>(CommentJsonString);

            //Getall

            var afterPostGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, "comments");
            var afterPostGetResult = await client.SendAsync(afterPostGetRequestMessage);
            var afterPostJsonString = await afterPostGetResult.Content.ReadAsStringAsync();
            List<CommentApi> afterPostComments = JsonConvert.DeserializeObject<List<CommentApi>>(afterPostJsonString);

            //PUT
            var editedComment = getComment("edited");
            var PutRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Put, $"comment/{commentId}", editedComment);
            var PutResult = await client.SendAsync(PutRequestMessage);
            var PutJsonString = await PutResult.Content.ReadAsStringAsync();
            var isEdited = JsonConvert.DeserializeObject<bool>(PutJsonString);

            //Getall

            var afterPutGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, "comments");
            var afterPutGetResult = await client.SendAsync(afterPutGetRequestMessage);
            var afterPutJsonString = await afterPutGetResult.Content.ReadAsStringAsync();
            List<CommentApi> afterPutComments = JsonConvert.DeserializeObject<List<CommentApi>>(afterPutJsonString);

            //DELETE
            var DeleteRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Delete, $"comment/{commentId}");
            var DeleteResult = await client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);

            //Getall

            var afterDeleteGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, "comments");
            var afterDeleteGetResult = await client.SendAsync(afterDeleteGetRequestMessage);
            var afterDeleteJsonString = await afterDeleteGetResult.Content.ReadAsStringAsync();
            List<CommentApi> afterDeleteComments = JsonConvert.DeserializeObject<List<CommentApi>>(afterDeleteJsonString);

            Assert.Equal(HttpStatusCode.OK, CommentResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, PutResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterPutGetResult.StatusCode);
            Assert.Equal(HttpStatusCode.OK, afterDeleteGetResult.StatusCode);

            Assert.NotEmpty(beforePostComments);
            Assert.NotEmpty(afterPostComments);
            Assert.NotEmpty(afterPutComments);
            Assert.NotEmpty(afterDeleteComments);

            Assert.Equal(beforePostComments.Count, afterDeleteComments.Count);
            Assert.Equal(afterPostComments.Count, afterPutComments.Count);
            Assert.Equal(beforePostComments.Count + 1, afterPostComments.Count);

        }
        [Fact]

        public async  void GetCommentLikes_ValidCall()
        {
            //POST
            var expectedComment = getComment("before edit");
            var CommentRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Post, "comment", expectedComment);
            var CommentResult = await client.SendAsync(CommentRequestMessage);
            var CommentJsonString = await CommentResult.Content.ReadAsStringAsync();
            var commentId = JsonConvert.DeserializeObject<int>(CommentJsonString);

           

            //GET LIKES
            var beforePutLikesGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, $"comment/{commentId}/likedUsers");
            var beforePutLikesGetResult = await client.SendAsync(beforePutLikesGetRequestMessage);
            var beforePutLikesJsonString = await beforePutLikesGetResult.Content.ReadAsStringAsync();
            List<LikeApi> beforePutLikes= JsonConvert.DeserializeObject<List<LikeApi>>(beforePutLikesJsonString);

            //PUT LIKE
           
            var PutRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Put, $"comment/{commentId}/likedUsers", getLike());
            var PutResult = await client.SendAsync(PutRequestMessage);
            var PutJsonString = await PutResult.Content.ReadAsStringAsync();
            var isEdited = JsonConvert.DeserializeObject<bool>(PutJsonString);

            //GET LIKES
            var afterPutLikesGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, $"comment/{commentId}/likedUsers");
            var afterPutLikesGetResult = await client.SendAsync(afterPutLikesGetRequestMessage);
            var afterPutLikesJsonString = await afterPutLikesGetResult.Content.ReadAsStringAsync();
            List<LikeApi> afterPutLikes = JsonConvert.DeserializeObject<List<LikeApi>>(afterPutLikesJsonString);

            //PUT LIKE
            
            var PutRequestMessageRe =  ApiTestManager.CreateRequest(HttpMethod.Put, $"comment/{commentId}/likedUsers", getLike());
            var PutResultRe = await client.SendAsync(PutRequestMessageRe);
            var PutJsonStringReAdd = await PutResultRe.Content.ReadAsStringAsync();
            var isEditedReAdd = JsonConvert.DeserializeObject<bool>(PutJsonStringReAdd);
            //GET LIKES
            var afterRePutLikesGetRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Get, $"comment/{commentId}/likedUsers");
            var afterRePutLikesGetResult = await client.SendAsync(afterRePutLikesGetRequestMessage);
            var afterRePutLikesJsonString = await afterRePutLikesGetResult.Content.ReadAsStringAsync();
            List<LikeApi> afterRePutLikes = JsonConvert.DeserializeObject<List<LikeApi>>(afterRePutLikesJsonString);


            //DELETE
            var DeleteRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Delete, $"comment/{commentId}");
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
        [Fact]
        public async void PostComment_InvalidCall_NoContent()
        {
            //POST
            var expectedComment = getComment("content");
            expectedComment.Content = null;
            var CommentRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Post, "comment", expectedComment);
            var CommentResult = await client.SendAsync(CommentRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, CommentResult.StatusCode);
        }
        [Fact]
        public async void PostComment_InvalidCall_NoUserIdHeader()
        {
            //POST
            var expectedComment = getComment("content");
            expectedComment.Content = null;
            var CommentRequestMessage =  ApiTestManager.CreateRequest(HttpMethod.Post, "comment", expectedComment);
            CommentRequestMessage.Headers.Clear();
            var CommentResult = await client.SendAsync(CommentRequestMessage);

            Assert.Equal(HttpStatusCode.BadRequest, CommentResult.StatusCode);
        }


    }
}
