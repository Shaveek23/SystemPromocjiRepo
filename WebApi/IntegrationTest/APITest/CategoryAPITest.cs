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

    public class CategoryAPITest
    {
        [Fact]
        public async void Categories_ValidCall()
        {
            var requestMessage = API.CreateRequest(HttpMethod.Get, "categories");
            var result = await API.client.SendAsync(requestMessage);
            var jsonString = await result.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<Categories>(jsonString);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(actual);
            Assert.NotEmpty(actual.categories);
            foreach (var category in actual.categories)
            {
                Assert.NotNull(category.Name);
                Assert.NotEqual("", category.Name);
            }
        }
    }
}
