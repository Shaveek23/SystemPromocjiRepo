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

namespace WebApiTest
{
    public class UserRepositoryTest
    {
        void SeedUser(DatabaseContext dbContext)
        {
            dbContext.Add(new User { UserID = 1, UserEmail = "iojestsuper@mini.pw.edu.pl", UserName = "student", Timestamp = new DateTime(2021, 4, 16, 8, 4, 12), IsEnterprenuer = false, IsAdmin = false, IsVerified = false });
            dbContext.Add(new User { UserID = 2, UserEmail = "cokolwiek@ck.pl", UserName = "ktokolwiek", Timestamp = new DateTime(2021, 2, 6, 4, 2, 12), IsEnterprenuer = false, IsAdmin = true, IsVerified = false });
            dbContext.Add(new User { UserID = 3, UserEmail = "xd@xd.pl", UserName = "jakikowliek", Timestamp = new DateTime(2020, 1, 11, 5, 4, 12), IsEnterprenuer = true, IsAdmin = false, IsVerified = false });
            dbContext.SaveChanges();
        }
        //[Fact]
        //public void GetUserByIdAsync_ValidCall()
        //{
        //    var options = new DbContextOptionsBuilder<DatabaseContext>()
        //        .UseInMemoryDatabase(databaseName: "GetUserByIdAsync_ValidCall").Options;

        //    using (var dbContext = new DatabaseContext(options))
        //    {
        //        SeedUser(dbContext);
        //        int expectedID = 1;
        //        var expected = dbContext.Users.Where(x => x.UserID == expectedID).FirstOrDefault();

        //        var cls = new UserRepository(dbContext);
        //        var actual = cls.GetUserByIdAsync(expectedID).Result.Result;

        //        Assert.True(actual != null);
        //        Assert.Equal(expected.UserID, actual.UserID);
        //        Assert.Equal(expected.UserName, actual.UserName);
        //        Assert.Equal(expected.UserEmail, actual.UserEmail);
        //        Assert.Equal(expected.Active, actual.Active);
        //        Assert.Equal(expected.IsEnterprenuer, actual.IsEnterprenuer);
        //    }
        //}

    //    [Fact]
    //    public void GetUserByIdAsync_InvalidCall_NoId()
    //    {
    //        var options = new DbContextOptionsBuilder<DatabaseContext>()
    //            .UseInMemoryDatabase(databaseName: "GetUserByIdAsync_InvalidCall_NoId").Options;

    //        using (var dbContext = new DatabaseContext(options))
    //        {
    //            SeedUser(dbContext);
    //            int expectedID = 0;
    //            var expected = dbContext.Users.Where(x => x.UserID == expectedID).FirstOrDefault();

    //            var cls = new UserRepository(dbContext);
    //            var actual = cls.GetUserByIdAsync(expectedID);

    //            Assert.True(actual != null);
    //            Assert.True(actual.Result.Result == null);
    //        }
    //    }
    //}
}
