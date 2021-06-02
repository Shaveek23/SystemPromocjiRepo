using IntegrationTest.APITest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.APITest
{
    public class APItester<GET, POST, PUT>
    {
        #region client
        //słownik HttpClient wszystkich grup
        static readonly Dictionary<string, HttpClient> clients = new Dictionary<string, HttpClient>()
        {
            ["D"] = new HttpClient { BaseAddress = new Uri("https://salesystemapi.azurewebsites.net/") },
            ["I"] = new HttpClient { BaseAddress = new Uri("https://systempromocji.azurewebsites.net/") },
            ["K"] = new HttpClient { BaseAddress = new Uri("https://serverappts.azurewebsites.net/") }
        };

        static readonly Dictionary<string, int> postIDs = new Dictionary<string, int>()
        {
            ["D"] = 1124,
            ["I"] = 111,
            ["K"] = 5
        };
        static readonly Dictionary<string, int> commentIDs = new Dictionary<string, int>()
        {
            ["D"] = 815,
            ["I"] = 178,
            ["K"] = 136
        };
        static readonly Dictionary<string, (int, int, int)> userIDs = new Dictionary<string, (int, int, int)>()
        {
            ["D"] = (1, 2, 4),
            ["I"] = (135, 136, 422),
            ["K"] = (31, 32, 249)
        };

        static readonly Dictionary<string,int> categoryIDs = new Dictionary<string, int>()
        {
            ["D"] = 1,
            ["I"] = 1,
            ["K"] = 7
        };
        public static readonly string group = "K";
        //wybrany client
        public readonly HttpClient client = clients[group];
        public readonly int existingPostID = postIDs[group];
        public readonly int existingCommentID = commentIDs[group];
        public readonly int existingUserID = userIDs[group].Item1;
        public readonly int NotOwnerUserID = userIDs[group].Item2;
        public readonly int AdminUserID = userIDs[group].Item3;
        public readonly int existingCategoryID = categoryIDs[group];
        #endregion

        //kreator requestów do API
        public HttpRequestMessage CreateRequest(HttpMethod method, string requestUri, object expected = null, int? userID = 0)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUri);
            userID = (userID == 0)?existingUserID : userID;
            if (userID.HasValue) requestMessage.Headers.Add("userId", $"{userID}");
            if (expected != null)
            {
                string jsonPost = JsonConvert.SerializeObject(expected);
                requestMessage.Content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            }
            return requestMessage;
        }

        #region API calls
        public async Task<(GET, HttpStatusCode)> Get(string requestUri, int? userID = 0)
        {
            var requestMessage = CreateRequest(HttpMethod.Get, requestUri, null, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            if (!responseMessage.IsSuccessStatusCode)
                return (default(GET), responseMessage.StatusCode);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            GET post = JsonConvert.DeserializeObject<GET>(jsonString);
            return (post, responseMessage.StatusCode);
        }
        public async Task<(List<GET>, HttpStatusCode)> GetAll(string requestUri, int? userID = 0)
        {
            var requestMessage = CreateRequest(HttpMethod.Get, requestUri, null, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            if (!responseMessage.IsSuccessStatusCode)
                return (default(List<GET>), responseMessage.StatusCode);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            List<GET> list = JsonConvert.DeserializeObject<List<GET>>(jsonString);
            return (list, responseMessage.StatusCode);
        }
        public async Task<(int, HttpStatusCode)> Post(string requestUri, POST obj, int? userID = 0)
        {
            var requestMessage = CreateRequest(HttpMethod.Post, requestUri, obj, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
                return (default(int), responseMessage.StatusCode);
            responseID response = JsonConvert.DeserializeObject<responseID>(jsonString);
            return (response.id, responseMessage.StatusCode);
        }
        public async Task<HttpStatusCode> Put(string requestUri, PUT obj, int? userID = 0)
        {
            var requestMessage = CreateRequest(HttpMethod.Put, requestUri, obj, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            return responseMessage.StatusCode;
        }
        public async Task<HttpStatusCode> Delete(string requestUri, int? userID = 0)
        {
            var requestMessage = CreateRequest(HttpMethod.Delete, requestUri, null, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            return responseMessage.StatusCode;
        }
        #endregion
    }

}
