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

namespace WebApiTest
{
    public class PersonServiceTest
    {
        List<Person> persons = new List<Person> {
            new Person { PersonID = 1, FirstName = "Anna", LastName = "Krajewska", Address = "Kwiatowa 1", City = "Warszawa" },
            new Person { PersonID = 2, FirstName = "Daniel", LastName = "Kwiatkowski", Address = "Kwiatowa 2", City = "Warszawa" },
            new Person { PersonID = 3, FirstName = "Ola", LastName = "Nowak", Address = "Kwiatowa 3", City = "Warszawa" },
            new Person { PersonID = 4, FirstName = "Maciej", LastName = "Lis", Address = "Kwiatowa 4", City = "Warszawa" },
            new Person { PersonID = 5, FirstName = "Adam", LastName = "Kowalski", Address = "Kwiatowa 5", City = "Warszawa" }};
        [Fact]
        public void GetAll_ValidCall() 
        {
            var expected = persons;
            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(x => x.GetAll())
                .Returns(persons.AsQueryable());

            var personService = new PersonService(mockIPersonRepository.Object);
            var actual = personService.GetAll().ToList();

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetById_ValidCall()
        {
            var expectedId = 1;
            var expected = persons.Where(x => x.PersonID == expectedId).FirstOrDefault();
            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(x => x.GetById(expectedId))
                .Returns(expected);

            var personService = new PersonService(mockIPersonRepository.Object);
            var actual = personService.GetById(expectedId);

            Assert.True(actual != null);
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.City, actual.City);
        }

        [Fact]
        public void GetById_InvalidCall_NoId()
        {
            var expectedId = 0;
            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(x => x.GetById(expectedId))
                .Returns(new Person { });

            var personService = new PersonService(mockIPersonRepository.Object);
            var actual = personService.GetById(expectedId);

            Assert.True(actual.PersonID == 0);
            Assert.Null(actual.FirstName);
            Assert.Null(actual.LastName);
            Assert.Null(actual.Address);
            Assert.Null(actual.City);
        }

    }
}
