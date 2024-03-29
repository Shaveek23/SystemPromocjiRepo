﻿using Microsoft.AspNetCore.Mvc;
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
        [InlineData(0, "Kategoria1")]
        [InlineData(5, "Kategoria2")]
        [InlineData(2, "Kategoria4")]
        public void GetById_Test(int id, string name)
        {


            var mockService = new Mock<ICategoryService>();
            mockService.Setup(x => x.GetById(id)).Returns(new ServiceResult<CategoryDTO>(new CategoryDTO

            {
                ID = id,
                Name=name
            }));

            var mockLogger = new Mock<ILogger<CategoryController>>();
            var controller = new CategoryController(mockLogger.Object, mockService.Object);

            var expected = new CategoryDTO

            {
                ID = id,
                Name = name
            };


            var actual = (CategoryDTO)((ObjectResult)controller.GetById(id).Result).Value;

            Assert.Equal(expected.ID, actual.ID);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Theory]
        [InlineData(0, "Kategoria1")]
        [InlineData(5, "Kategoria2")]
        [InlineData(2, "Kategoria4")]
        public void GetAll_Test(int id, string name)
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();
            categories.Add(new CategoryDTO
            {
                ID = id,
                Name = name
            });
            categories.Add(new CategoryDTO
            {
                ID = id+1,
                Name = name+"cokolwiek"
            });
            var mockService = new Mock<ICategoryService>();
            mockService.Setup(x => x.GetAll()).Returns(new ServiceResult<IQueryable<CategoryDTO>>(categories.AsQueryable()));

            var mockLogger = new Mock<ILogger<CategoryController>>();
            var controller = new CategoryController(mockLogger.Object, mockService.Object);

            var expected = categories;

            var actual = ((IQueryable<CategoryDTO>)((ObjectResult)controller.GetAll().Result).Value).ToList();

            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem.ID == shouldItem.ID && isItem.Name==shouldItem.Name)));


        }

    }
}
