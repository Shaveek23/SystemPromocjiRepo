using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace IntegrationTest.APITest
{
    public static class API
    {
        static Dictionary<string, HttpClient> clients = new Dictionary<string, HttpClient>()
        {
            ["I"] = new HttpClient { BaseAddress = new Uri("https://webapi20210317153051.azurewebsites.net/") },
            ["D"] = new HttpClient { BaseAddress = new Uri("https://salesystemapi.azurewebsites.net/") },
            ["K"] = new HttpClient { BaseAddress = new Uri("https://serverappats.azurewebsites.net/") }
        };

        public static HttpClient client = clients["I"];

        public static HttpRequestMessage CreateRequest(HttpMethod method, string requestUri, object expected = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUri);
            requestMessage.Headers.Add("userId", "1"); //można zparametryzować

            string jsonPost = JsonConvert.SerializeObject(expected);
            requestMessage.Content = new StringContent(jsonPost, Encoding.UTF8, "application/json");

            return requestMessage;
        }

        

    }

}
