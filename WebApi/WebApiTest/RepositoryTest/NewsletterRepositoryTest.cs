using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Database;
using WebApi.Database.Repositories.Implementations;
using WebApi.Models.POCO;
using Xunit;

namespace WebApiTest
{
   public class NewsletterRepositoryTest
    {
        private int UserId = 1;
        void SeedNewsletters(DatabaseContext dbContext)
        {
            dbContext.Add(new Newsletter { CategoryID = 1, NewsletterID = 1, UserID = 1 });
            dbContext.Add(new Newsletter { CategoryID = 2, NewsletterID = 2, UserID = 2 });
            dbContext.Add(new Newsletter { CategoryID = 3, NewsletterID = 3, UserID = 1 });
            dbContext.SaveChanges();
        }
        [Fact]
        public void GetAllSubscribedCategoriesTest()
        {

            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "GetAllSubscribedCategoriesTest_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedNewsletters(dbContext);

            }
            using (var dbContext = new DatabaseContext(options))
            {
                var expected = dbContext.Newsletters.Where(x=>x.UserID== UserId).ToList();
                var repositiry = new NewsletterRepository(dbContext);
                var actual = repositiry.GetAllSubscribedCategories(UserId).Result;
                Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
            }
        }
    }
}
