using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models.DTO;
using WebApi.Services;
using WebApi.Services.Services_Interfaces;
using Xunit;

namespace WebApiTest.ControllerTest
{
    public class CategoryControllerTest
    {
        [Theory]
        [InlineData(0, "Kategoria")]
        [InlineData(5, "xd")]
        [InlineData(2, "lol")]
        public void GetById_Test(int id, string name)
        {


            var mockService = new Mock<ICategoryService>();
            mockService.Setup(x => x.GetById(id)).Returns(new ServiceResult<CategoryDTO>(new CategoryDTO

            {
                CategoryID = id,
                Name=name
            }));

            var mockLogger = new Mock<ILogger<CategoryController>>();
            var controller = new CategoryController(mockLogger.Object, mockService.Object);

            var expected = new CategoryDTO

            {
                CategoryID = id,
                Name = name
            };


            var actual = (CategoryDTO)((ObjectResult)controller.GetById(id).Result).Value;

            Assert.Equal(expected.CategoryID, actual.CategoryID);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Theory]
        [InlineData(0, "Kategoria")]
        [InlineData(5, "xd")]
        [InlineData(2, "lol")]
        public void GetAll_Test(int id, string name)
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            categories.Add(new CategoryDTO
            {
                CategoryID = id,
                Name = name
            });
            categories.Add(new CategoryDTO
            {
                CategoryID = id+1,
                Name = name+"cokolwiek"
            });
            var mockService = new Mock<ICategoryService>();
            mockService.Setup(x => x.GetAll()).Returns(new ServiceResult<IQueryable<CategoryDTO>>(categories.AsQueryable()
            ));

            var mockLogger = new Mock<ILogger<CategoryController>>();
            var controller = new CategoryController(mockLogger.Object, mockService.Object);

            var expected = categories;


            var actual = ((ServiceResult<IQueryable<CategoryDTO>>)((ObjectResult)controller.GetAll().Result).Value).Result.ToList();

            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem.CategoryID == shouldItem.CategoryID && isItem.Name==shouldItem.Name)));


        }

    }
}
