
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Controllers;
using WebApi.Database;
using WebApi.Database.Repositories.Implementations;
using WebApi.Models.DTO;
using WebApi.Models.POCO;
using WebApi.Services.Serives_Implementations;
using Xunit;

namespace IntegrationTest.IntegrationTest
{
    public class CategoryIntegrationTest
    {
        public CategoryController GetCategoryController(DbContextOptions<DatabaseContext> options)
        {
            var logger = new Mock<ILogger<CategoryController>>();
            var databaseContext = new DatabaseContext(options);
            var categoryRepository = new CategoryRepository(databaseContext);
            var categoryService = new CategoryService(categoryRepository);
            var categoryController = new CategoryController(logger.Object, categoryService);

            return categoryController;
        }

        [Fact]
        public void GetCategories_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetCategories_ValidCall").Options;
            var categories = new List<Category>{
                new Category { CategoryID = 1, Name = "test1"},
                new Category { CategoryID = 2, Name = "test2"},
                new Category { CategoryID = 3, Name = "test3"},
                new Category { CategoryID = 4, Name = "test4"}
            };

            using (var dbContext = new DatabaseContext(options))
            {
                foreach (var category in categories)
                    dbContext.Add(category);
                dbContext.SaveChanges();
            }

            var categoryController = GetCategoryController(options);
            
            var actual = (IQueryable<CategoryDTO>)((ObjectResult)categoryController.GetAll().Result).Value;
            var count = actual.Count();
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(4, count);
            foreach(var category in actual)
            {
                Assert.NotNull(category.Name);
                Assert.NotEqual("", category.Name);
            }    
        }

        [Fact]
        public void GetCategories_ValidCall_Empty()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetCategories_ValidCall_Empty").Options;

            var categoryController = GetCategoryController(options);

            var actual = (IQueryable<CategoryDTO>)((ObjectResult)categoryController.GetAll().Result).Value;

            Assert.NotNull(actual);
            Assert.Empty(actual);
        }

    }
}
