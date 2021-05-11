using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using WebApi.Models.DTO;
using Xunit;
using IntegrationTest.APITest.Models.Category;
namespace IntegrationTest.APITest
{

    public class CategoryAPITest : APItester<CategoryAPI_get, CategoryAPI_get> //TO DO: może coś mądrzejszego można, ale działa
    {

        #region GET
        [Fact]
        public async void Categories_ValidCall()
        {
            HttpStatusCode statusCode;
            CategoryAPI_get categories;

            //GET
            (categories, statusCode) = await Get("categories");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.NotNull(categories.categories);
            Assert.NotEmpty(categories.categories);
            foreach (var category in categories.categories)
            {
                Assert.NotNull(category.Name);
                Assert.NotEqual("", category.Name);
            }
        }
        #endregion

    }
}
