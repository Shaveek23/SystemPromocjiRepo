using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.APITest
{
    public class LikesAPITest
    {
        HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://webapi20210317153051.azurewebsites.net/")
        };


        public class LikeAPI
        {
            public bool like;
        }

        public class LikeResultAPI
        {
            public int id;
        }

        public LikeAPI getLike(bool like)
        {
            return new LikeAPI
            {
                like = like
            };
        }

        public class CommentAPI
        {
            public string content;
            public int postId;
        }

        /*
        private async Task<int> PreparePost()
        {
            var post = new PostAPI
            {
                Content = "Post testowy",
                Title = "interation_title",
                UserID = 1,
                Datetime = new DateTime(2008, 3, 1, 7, 0, 0),
                Category = 1,
                IsPromoted = false
            };
            var expectedPost = post;
            var PostRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "post", expectedPost);
            var PostResult = await client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var postID = JsonConvert.DeserializeObject<int>(PostJsonString);
            return postID;
        }


        private async Task<int> PrepareComment(int postID)
        {
            var comment = new CommentAPI
            {
                content = "Post testowy",
                postId = postID
            };
            
            var PostRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Post, "comment", comment);
            var PostResult = await client.SendAsync(PostRequestMessage);
            var PostJsonString = await PostResult.Content.ReadAsStringAsync();
            var commentID = JsonConvert.DeserializeObject<int>(PostJsonString);

            return commentID;

        }



        private async void DeletePost(int postID)
        {
            var DeleteRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Delete, $"post/{postID}");
            var DeleteResult = await client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);
        }

        private async void DeleteComment(int commentID)
        {
            var DeleteRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Delete, $"comment/{commentID}");
            var DeleteResult = await client.SendAsync(DeleteRequestMessage);
            var DeleteJsonString = await DeleteResult.Content.ReadAsStringAsync();
            var isDeleted = JsonConvert.DeserializeObject<bool>(DeleteJsonString);
        }

        [Fact]

        public async void PostLikeAndDislike_ValidCall()
        {

            int postID = await PreparePost();
            int likerID = 99;


            // LIKE:
            // PUT

            LikeAPI like = getLike(true);
            var putRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Put, $"post/{postID}/likedUsers", like, likerID);
            var putResult = await client.SendAsync(putRequestMessage);
            var putJsonString = await putResult.Content.ReadAsStringAsync();
            var putActual = JsonConvert.DeserializeObject<bool>(putJsonString);


            // GET
            var getRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, $"post/{postID}/likedUsers");
            var getResult = await client.SendAsync(getRequestMessage);

            var getJsonString = await getResult.Content.ReadAsStringAsync();
            var likes = JsonConvert.DeserializeObject<List<LikeResultAPI>>(getJsonString);

            // DISLIKE:
            LikeAPI dislike = getLike(false);
            var putRequestMessage2 = ApiTestManager.CreateRequest(HttpMethod.Put, $"post/{postID}/likedUsers", dislike, likerID);
            var putResult2 = await client.SendAsync(putRequestMessage2);
            var putJsonString2 = await putResult2.Content.ReadAsStringAsync();
            var putActual2 = JsonConvert.DeserializeObject<bool>(putJsonString2);


            // GET
            var getRequestMessage2 = ApiTestManager.CreateRequest(HttpMethod.Get, $"post/{postID}/likedUsers");
            var getResult2 = await client.SendAsync(getRequestMessage2);
            var getJsonString2 = await getResult2.Content.ReadAsStringAsync();
            var likes2 = JsonConvert.DeserializeObject<List<LikeResultAPI>>(getJsonString2);

            // delete post created for this test:
            DeletePost(postID);

            Assert.True(putActual);
            Assert.Contains(likes, like => like.id == likerID);

            Assert.True(putActual2);
            Assert.DoesNotContain(likes2, like => like.id == likerID);

        }

        [Fact]

        public async void CommentLikeAndDislike_ValidCall()
        {

            int postID = await PreparePost();
            int commentID = await PrepareComment(postID);
            int likerID = 99;


            // LIKE:
            // PUT

            LikeAPI like = getLike(true);
            var putRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Put, $"post/{commentID}/likedUsers", like, likerID);
            var putResult = await client.SendAsync(putRequestMessage);
            var putJsonString = await putResult.Content.ReadAsStringAsync();
            var putActual = JsonConvert.DeserializeObject<bool>(putJsonString);


            // GET
            var getRequestMessage = ApiTestManager.CreateRequest(HttpMethod.Get, $"post/{commentID}/likedUsers");
            var getResult = await client.SendAsync(getRequestMessage);

            var getJsonString = await getResult.Content.ReadAsStringAsync();
            var likes = JsonConvert.DeserializeObject<List<LikeResultAPI>>(getJsonString);

            // DISLIKE:
            LikeAPI dislike = getLike(false);
            var putRequestMessage2 = ApiTestManager.CreateRequest(HttpMethod.Put, $"post/{commentID}/likedUsers", dislike, likerID);
            var putResult2 = await client.SendAsync(putRequestMessage2);
            var putJsonString2 = await putResult2.Content.ReadAsStringAsync();
            var putActual2 = JsonConvert.DeserializeObject<bool>(putJsonString2);


            // GET
            var getRequestMessage2 = ApiTestManager.CreateRequest(HttpMethod.Get, $"post/{commentID}/likedUsers");
            var getResult2 = await client.SendAsync(getRequestMessage2);
            var getJsonString2 = await getResult2.Content.ReadAsStringAsync();
            var likes2 = JsonConvert.DeserializeObject<List<LikeResultAPI>>(getJsonString2);

            // delete post created for this test:
            DeletePost(postID);
            DeleteComment(commentID);

            Assert.True(putActual);
            Assert.Contains(likes, like => like.id == likerID);

            Assert.True(putActual2);
            Assert.DoesNotContain(likes2, like => like.id == likerID);

        }



        */
    }
}
