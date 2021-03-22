using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using WebApi.Controllers;
using WebApi.Models.POCO;
using Microsoft.Extensions.Logging;
using WebApi.Services;
using WebApi.Services.Serives_Implementations;
using WebApi.Services.Services_Interfaces;
using WebApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using WebApi.Models.DTO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using NuGet.Frameworks;
using Microsoft.VisualBasic;

namespace WebApiTest.ControllerTest
{
    public class PersonControllerTest
    {

        [Fact]
        public void Get_Test()
        {
            //Arrange
            var mockService = new Mock<IPersonService>();
            mockService.Setup(x => x.GetById(0)).Returns(new PersonDTO
            {
                PersonID = 0,
                Address = "Czekoladowa 12",
                City = "Warszawa",
                FirstName = "Kuba",
                LastName = "Kubowski"
            });
            var mockLogger = new Mock<ILogger<PersonController>>();
            var controller = new PersonController(mockLogger.Object, mockService.Object);

            var expected = new PersonDTO
            {
                PersonID = 0,
                Address = "Czekoladowa 12",
                City = "Warszawa",
                FirstName = "Kuba",
                LastName = "Kubowski"
            };

            //Act
            var actual = controller.Get(0);

            //Assert
            Assert.Equal(expected.PersonID, actual.PersonID);
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.City, actual.City);
        }

        [Fact]
        public void GetAll_Test()
        {
            List<PersonDTO> people =new List<PersonDTO>();
            people.Add(new PersonDTO
            {
                PersonID = 0,
                Address = "Czekoladowa 12",
                City = "Warszawa",
                FirstName = "Kuba",
                LastName = "Kubowski"
            });

            people.Add(new PersonDTO
            {
                PersonID = 1,
                Address = "Cokolwiek 3",
                City = "Poznan",
                FirstName = "Pawel",
                LastName = "Golik"
            });

            //Arrange
            var mockService = new Mock<IPersonService>();
            mockService.Setup(x => x.GetAll()).Returns(people.AsQueryable());
            var mockLogger = new Mock<ILogger<PersonController>>();
            var controller = new PersonController(mockLogger.Object, mockService.Object);

            var expected = people;
            //Act
            var actual = controller.GetAll().ToList();

            //Asset
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }

        [Fact]
        public void AddPersonDTO_Test()
        {
            //Arrange
            var mockService = new Mock<IPersonService>();
                mockService.Setup(x => x.AddPersonAsync(new PersonDTO {PersonID=0, Address="Czekoladowa 12",
                    City="Warszawa", FirstName="Kuba", LastName="Kubowski"})).Returns(AddPerson(new PersonDTO
                    {
                        PersonID = 0,
                        Address = "Czekoladowa 12",
                        City = "Warszawa",
                        FirstName = "Kuba",
                        LastName = "Kubowski"
                    }, mockService.Object));
            var mockLogger = new Mock<ILogger<PersonController>>();
            var controller = new PersonController(mockLogger.Object, mockService.Object);

            var expected = AddPerson(new PersonDTO
            {
                PersonID = 0,
                Address = "Czekoladowa 12",
                City = "Warszawa",
                FirstName = "Kuba",
                LastName = "Kubowski"
            }, mockService.Object).Result;

            //Act
            var actual = controller.AddPerson(new PersonDTO
            {
                PersonID = 0,
                Address = "Czekoladowa 12",
                City = "Warszawa",
                FirstName = "Kuba",
                LastName = "Kubowski"
            }).Result;

            //Assert
            Assert.Equal(expected.PersonID, actual.PersonID);
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.Address, actual.Address);
            Assert.Equal(expected.City, actual.City);

        }

        public async Task<PersonDTO> AddPerson(PersonDTO person, IPersonService service)
        {
            return await service.AddPersonAsync(person);
        }



    }
}
