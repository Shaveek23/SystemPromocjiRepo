using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;
using IntegrationTest.APITest.Models.Category;
namespace IntegrationTest.APITest
{

    public class CategoryAPITest : APItester<CategoryAPI_get, object, object> //TO DO: może coś mądrzejszego można, ale działa
    {
        public List<CategoryAPI_get> expected = new List<CategoryAPI_get>
        {
            new CategoryAPI_get{id = 1, name = "Buty"},
            new CategoryAPI_get{id = 2, name = "RTV/AGD"},
            new CategoryAPI_get{id = 3, name = "Elektronika"},
            new CategoryAPI_get{id = 4, name = "Ubrania"},
            new CategoryAPI_get{id = 5, name = "Telefony"},
            new CategoryAPI_get{id = 6, name = "Jedzenie"},
        };

        #region GET
        [Fact]
        public async void GetAllCategory_ValidCall()
        {
            HttpStatusCode statusCode;
            List<CategoryAPI_get> categories;

            //GET
            (categories, statusCode) = await GetAll("categories");
            Assert.Equal(HttpStatusCode.OK, statusCode);

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.NotNull(categories);
            Assert.NotEmpty(categories);
            //Assert.Equal(expected.Count, categories.Count);
            foreach (var category in categories)
            {
                Assert.NotNull(category.name);
                Assert.NotEqual("", category.name);
                //Assert.Equal(expected[category.id - 1].name, category.name); //sprawdzamy czy mamy dokładnie te kategorie jak w ustaleniach
            }
        }
        #endregion

    }
}
