using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models.POCO;
using Xunit;
using Moq;
using Autofac.Extras.Moq;
using WebApi.Services.Services_Interfaces;
using WebApi.Database.Repositories.Interfaces;
using System.Linq;
using WebApi.Database;
using WebApi.Services.Serives_Implementations;
using WebApi.Database.Mapper;
using WebApi.Services;

namespace WebApiTest.ServiceTest
{
    public class CategoryServiceTest
    {
        public List<Category> categories = new List<Category>
        {
            new Category{CategoryID=0, Name="cokolwiekkkkkk"},
            new Category{CategoryID=1, Name="cokolwiek"},
            new Category{CategoryID=2, Name="xcvsdsada"},
        };

        [Fact]
        public void GetAll_ValidCall()
        {
            var expected = categories;
            var mockICategoryRepository = new Mock<ICategoryRepository>();
            mockICategoryRepository.Setup(x => x.GetAll())
                .Returns(new ServiceResult<IQueryable<Category>>(categories.AsQueryable()));

            var categoryService = new CategoryService(mockICategoryRepository.Object);
            var actual = categoryService.GetAll().Result.ToList();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem.id == shouldItem.CategoryID && isItem.name == shouldItem.Name)));

        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_ValidCall(int id)
        {
            var expected = new ServiceResult<Category>(categories.Where(x => x.CategoryID == id).FirstOrDefault());
            var mockICategoryRepository = new Mock<ICategoryRepository>();
            mockICategoryRepository.Setup(x => x.GetById(id))
                .Returns(expected);

            var categoryService = new CategoryService(mockICategoryRepository.Object);
            var actual = categoryService.GetById(id).Result;
            var expected2 = expected.Result;

            Assert.True(actual != null);
            Assert.Equal(expected2.CategoryID, actual.id);
            Assert.Equal(expected2.Name, actual.name);

        }

    }
}
