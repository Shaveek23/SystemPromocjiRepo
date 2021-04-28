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
    public class PersonRepositoryTest
    {
        void SeedPerson(DatabaseContext dbContext)
        {
            dbContext.Add(new Person { PersonID = 1, FirstName = "Anna", LastName = "Krajewska", Address = "Kwiatowa 1", City = "Warszawa" });
            dbContext.Add(new Person { PersonID = 2, FirstName = "Daniel", LastName = "Kwiatkowski", Address = "Kwiatowa 2", City = "Warszawa" });
            dbContext.Add(new Person { PersonID = 3, FirstName = "Ola", LastName = "Nowak", Address = "Kwiatowa 3", City = "Warszawa" });
            dbContext.Add(new Person { PersonID = 4, FirstName = "Maciej", LastName = "Lis", Address = "Kwiatowa 4", City = "Warszawa" });
            dbContext.Add(new Person { PersonID = 5, FirstName = "Adam", LastName = "Kowalski", Address = "Kwiatowa 5", City = "Warszawa" });
            dbContext.SaveChanges();
        }
        [Fact]
        public void GetPersonByIdAsync_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "GetPersonByIdAsync_ValidCall").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                SeedPerson(dbContext);
                int expectedID = 1;
                var expected = dbContext.Persons.Where(x => x.PersonID == expectedID).FirstOrDefault();

                var cls = new PersonRepository(dbContext);
                var actual = cls.GetPersonByIdAsync(expectedID).Result;

                Assert.True(actual != null);
                Assert.Equal(expected.FirstName, actual.Result.FirstName);
                Assert.Equal(expected.LastName, actual.Result.LastName);
                Assert.Equal(expected.Address, actual.Result.Address);
                Assert.Equal(expected.City, actual.Result.City);
            }
        }

        [Fact]
        public void GetPersonByIdAsync_InvalidCall_NoId()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "GetPersonByIdAsync_InvalidCall_NoId").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                SeedPerson(dbContext);
                int expectedID = 0;
                var expected = dbContext.Persons.Where(x => x.PersonID == expectedID).FirstOrDefault();

                var cls = new PersonRepository(dbContext);
                var actual = cls.GetPersonByIdAsync(expectedID);

                Assert.True(actual != null);
                Assert.True(actual.Result.Result == null);
            }
        }
    }
}
