using WebApi.Database.Repositories.Implementations;
using Moq;
using Autofac.Extras.Moq;
using WebApi.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.POCO;
using WebApi.Database;
using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Exceptions;

namespace WebApiTest
{
    public class RepositoryTest
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
        public void GetById_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "GetById_ValidCall").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                SeedPerson(dbContext);
                var expected = new Person { PersonID = 6, FirstName = "Ida", LastName = "Mazur", Address = "Kwiatowa 6", City = "Warszawa" };
                dbContext.Add(expected);
                dbContext.SaveChanges();

                var cls = new Repository<Person>(dbContext);
                var actual = cls.GetById(expected.PersonID).Result;

                Assert.True(actual != null);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.Address, actual.Address);
                Assert.Equal(expected.City, actual.City);
            }
        }
        [Fact]
        public void GetById_InvalidCall_NoId()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "GetById_InvalidCall_NoId").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                SeedPerson(dbContext);
                var expected = new Person { PersonID = 6, FirstName = "Ida", LastName = "Mazur", Address = "Kwiatowa 6", City = "Warszawa" };
                dbContext.Add(expected);
                dbContext.SaveChanges();

                var cls = new Repository<Person>(dbContext);

                var actual = cls.GetById(0);

                Assert.Null(actual.Result);
                Assert.Equal(400, (int)(actual.Code));
            }
        }

        [Fact]
        public void GetAll_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "GetAll_ValidCall").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                SeedPerson(dbContext);
                var expected = dbContext.Persons.ToList().Count;
                var cls = new Repository<Person>(dbContext);
                var actual = cls.GetAll().Result;
                Assert.True(actual != null);
                Assert.True(actual.ToList().Count == expected);
            }
        }

        [Fact]
        public void GetAll_ValidCall_WithEmpty()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "GetAll_ValidCall_WithEmpty").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                var cls = new Repository<Person>(dbContext);
                var actual = cls.GetAll().Result;
                Assert.True(actual != null);
                Assert.True(actual.ToList().Count == 0);
            }
        }
        [Fact]
        public void AddAsync_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "AddAsync_ValidCall").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                var expected = new Person { PersonID = 1, FirstName = "Ida", LastName = "Mazur", Address = "Kwiatowa 6", City = "Warszawa" };

                var cls = new Repository<Person>(dbContext);
                _ = cls.AddAsync(expected);
                var actual = dbContext.Persons.Where(x => x.PersonID == expected.PersonID).FirstOrDefault();
                Assert.True(dbContext.Persons.ToList().Count == 1);
                Assert.Equal(expected, actual);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.Address, actual.Address);
                Assert.Equal(expected.City, actual.City);
            }
        }

        [Fact]
        public void AddAsync_InvalidCall_Null()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "AddAsync_InvalidCall_Null").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                var cls = new Repository<Person>(dbContext);
                var actual = cls.AddAsync(null);
                Assert.True(dbContext.Persons.ToList().Count == 0);
            }
        }

        [Fact]
        public void AddAsync_InvalidCall_AlreadyExistingId()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "AddAsync_InvalidCall_MissingData").Options;

            using (var dbContext = new DatabaseContext(options))
            {
                var expected = new Person { PersonID = 1, FirstName = "Ida", LastName = "Mazur", Address = "Kwiatowa 6", City = "Warszawa" };
                dbContext.Add(expected);
                dbContext.SaveChanges();
                var tried = new Person { PersonID = 1, FirstName = "Ola", LastName = "Nowak", Address = "Kwiatowa 6", City = "Warszawa"};
                var cls = new Repository<Person>(dbContext);
                _ = cls.AddAsync(tried);
                var actual = dbContext.Persons.Where(x => x.PersonID == expected.PersonID).FirstOrDefault();

                Assert.True(dbContext.Persons.ToList().Count == 1);
                Assert.Equal(expected, actual);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.Address, actual.Address);
                Assert.Equal(expected.City, actual.City);
            }
        }


        [Fact]
        public async void UpdateAsync_ValidCall()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "UpdateAsync_ValidCall").EnableSensitiveDataLogging().Options;

            using (var dbContext = new DatabaseContext(options))
            {
                var input = new Person { PersonID = 1, FirstName = "Ida", LastName = "Mazur", Address = "Kwiatowa 6", City = "Warszawa"};

                dbContext.Add(input);
                dbContext.SaveChanges();
            }

            using (var dbContext = new DatabaseContext(options))
            {
                var expected = new Person { PersonID = 1, FirstName = "Ola", LastName = "Nowak", Address = "Kwiatowa 6", City = "Warszawa"};

                var cls = new Repository<Person>(dbContext);
                _ = await cls.UpdateAsync(expected);
                var actual = dbContext.Persons.Where(x => x.PersonID == expected.PersonID).FirstOrDefault();

                Assert.True(dbContext.Persons.ToList().Count == 1);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.Address, actual.Address);
                Assert.Equal(expected.City, actual.City);
            }
        }

        [Fact]
        public void UpdateAsync_InvalidCall_Null()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "UpdateAsync_InvalidCall_Null").Options;
            var first = new Person { PersonID = 1, FirstName = "Ida", LastName = "Mazur", Address = "Kwiatowa 6", City = "Warszawa"};

            using (var dbContext = new DatabaseContext(options))
            {

                dbContext.Add(first);
                dbContext.SaveChanges();
            }

            using (var dbContext = new DatabaseContext(options))
            {
                var cls = new Repository<Person>(dbContext);
                _ = cls.UpdateAsync(null);
                var actual = dbContext.Persons.Where(x => x.PersonID == first.PersonID).FirstOrDefault();

                Assert.True(dbContext.Persons.ToList().Count == 1);
                Assert.Equal(first.PersonID, actual.PersonID);
                Assert.Equal(first.FirstName, actual.FirstName);
                Assert.Equal(first.LastName, actual.LastName);
                Assert.Equal(first.Address, actual.Address);
                Assert.Equal(first.City, actual.City);
            }
        }


    }
}