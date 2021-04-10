using System;
using WebApi.Models.POCO;
using Xunit;
using WebApi.Database;
using Microsoft.EntityFrameworkCore;
using WebApi.Database.Repositories.Implementations;
using Moq;
using Autofac.Extras.Moq;
using WebApi.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebApi.Models.DTO.PostDTOs;

namespace WebApiTest
{
    public class PostRepositoryTest
    {
        DateTime datetime1 = new DateTime(2020, 1, 1);
        DateTime datetime2 = new DateTime(3213123);
        DateTime datetime3 = new DateTime(2000, 10, 10, 11, 4, 41);
        void SeedPost(DatabaseContext dbContext)
        {
            dbContext.Add(new Post { PostID = 1, UserID = 1, CategoryID = 1, Title = "Title1", Content = "Content1 ", Date = new DateTime(2020, 1, 1), IsPromoted = false});
            dbContext.Add(new Post { PostID = 12, UserID = 8, CategoryID = 3, Date = new DateTime(3213123), IsPromoted = true });
            dbContext.Add(new Post { PostID = 2, UserID = 5, CategoryID = 5, Title = "Title32321", Date = new DateTime(2000, 10, 10, 11, 4, 41), IsPromoted = false });
            dbContext.SaveChanges();
        }
        void SeedComment(DatabaseContext dbContext)
        {
            dbContext.Add(new Comment() { CommentID = 1, UserID = 1, PostID = 2, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test" });
            dbContext.Add(new Comment() { CommentID = 2, UserID = 2, PostID = 2, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test2" });
            dbContext.Add(new Comment() { CommentID = 5, UserID = 2, PostID = 2, DateTime = new DateTime(2008, 3, 1, 7, 0, 0), Content = "test21" });
           

            dbContext.SaveChanges();
        }
        [Fact]
        public void GetAllComments_ValisCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                 .UseInMemoryDatabase(databaseName: "GetAllComments_ValidCall").Options;
            using (var dbContext = new DatabaseContext(options))
            {
                SeedComment(dbContext);

                int PostID = 2;

                var expected = dbContext.Comments.Where(x => x.PostID == PostID);          



                var cls = new PostRepository(dbContext);
                var actual = cls.GetAllComments(PostID);

                Assert.True(actual != null);
                Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
            }

        }

        
    }
}
