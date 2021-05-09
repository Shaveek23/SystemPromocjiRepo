using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IntegrationTest.APITest
{
    static class ApiTestManager
    {
        public static HttpRequestMessage CreateRequest(HttpMethod method, string requestUri, object expected = null, int userId = 1)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUri);
            requestMessage.Headers.Add("userId", $"{userId}"); //można zparametryzować

            if (expected != null)
            {
                string jsonPost = JsonConvert.SerializeObject(expected);
                requestMessage.Content = new StringContent(jsonPost, Encoding.UTF8, "application/json");
            }
          

            return requestMessage;
        }


    }
}
