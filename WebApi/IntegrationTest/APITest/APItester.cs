using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.APITest
{
    public class APItester<T1, T2>
    {
        //słownik HttpClient wszystkich grup
        static Dictionary<string, HttpClient> clients = new Dictionary<string, HttpClient>()
        {
            ["I"] = new HttpClient { BaseAddress = new Uri("https://webapi20210317153051.azurewebsites.net/") },
            ["D"] = new HttpClient { BaseAddress = new Uri("https://salesystemapi.azurewebsites.net/") },
            ["K"] = new HttpClient { BaseAddress = new Uri("https://serverappats.azurewebsites.net/") }
        };

        //wybrany client
        public readonly HttpClient client = clients["I"];

        //kreator requestów do API
        public HttpRequestMessage CreateRequest(HttpMethod method, string requestUri, object expected = null, int? userID = 1)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUri);
            if (userID.HasValue) requestMessage.Headers.Add("userId", $"{userID}");
            if (expected != null)
            {
                string jsonPost = JsonConvert.SerializeObject(expected);
                requestMessage.Content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            }
            return requestMessage;
        }

        public async Task<(bool, HttpStatusCode)> Delete(string requestUri, int? userID = 1)
        {
            var requestMessage = CreateRequest(HttpMethod.Delete, requestUri, null, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            if (!responseMessage.IsSuccessStatusCode)
                return (false, responseMessage.StatusCode);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            bool isDeleted = JsonConvert.DeserializeObject<bool>(jsonString);
            return (isDeleted, responseMessage.StatusCode);
        }

        public async Task<(T1, HttpStatusCode)> Get(string requestUri, int? userID = 1)
        {
            var requestMessage = CreateRequest(HttpMethod.Get, requestUri, null, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            if (!responseMessage.IsSuccessStatusCode)
                return (default(T1), responseMessage.StatusCode);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            T1 post = JsonConvert.DeserializeObject<T1>(jsonString);
            return (post, responseMessage.StatusCode);
        }

        public async Task<(bool, HttpStatusCode)> Put(string requestUri, T2 obj, int? userID = 1)
        {
            var requestMessage = CreateRequest(HttpMethod.Put, requestUri, obj, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            if (!responseMessage.IsSuccessStatusCode)
                return (false, responseMessage.StatusCode);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            bool isPuted = JsonConvert.DeserializeObject<bool>(jsonString);
            return (isPuted, responseMessage.StatusCode);
        }

        public async Task<(int, HttpStatusCode)> Post(string requestUri, T2 obj, int? userID = 1)
        {
            var requestMessage = CreateRequest(HttpMethod.Post, requestUri, obj, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode)
                return (default(int), responseMessage.StatusCode);
            int postID = JsonConvert.DeserializeObject<int>(jsonString);
            return (postID, responseMessage.StatusCode);
        }

        public async Task<(List<T1>, HttpStatusCode)> GetAll(string requestUri, int? userID = 1)
        {
            var requestMessage = CreateRequest(HttpMethod.Get, requestUri, null, userID);
            var responseMessage = await client.SendAsync(requestMessage);
            if (!responseMessage.IsSuccessStatusCode)
                return (default(List<T1>), responseMessage.StatusCode);
            var jsonString = await responseMessage.Content.ReadAsStringAsync();
            List<T1> list = JsonConvert.DeserializeObject<List<T1>>(jsonString);
            return (list, responseMessage.StatusCode);
        }
    }

}
