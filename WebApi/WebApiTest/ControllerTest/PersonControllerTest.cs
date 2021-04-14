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
using WebApi.Database.Mapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace WebApiTest.ControllerTest
{
    public class PersonControllerTest
    {
        [Theory]
        [InlineData(0, "Konrad", "Gaweda", "Zlota 23", "Warszawa")]
        [InlineData(1, "Pawel", "Golik", "Zelazna 21", "Poznan")]
        [InlineData(2, "Hosia", "Gadasz", "Sadyba 1", "Katowice noca")]
        [InlineData(3, "Cokolwiek", "Jakiekolwiek", "Jaroslawa Kaczynskiego 12", "Trybunal Konstytucyjny")]
        public void Get_Test(int id, string firstName, string lastName, string address, string city)
        {
            //Arrange
            var mockService = new Mock<IPersonService>();
            mockService.Setup(x => x.GetById(0)).Returns(new ServiceResult<PersonDTO>(new PersonDTO
            {
                PersonID = id,
                Address = address,
                City = city,
                FirstName = firstName,
                LastName = lastName
            }));
            var mockLogger = new Mock<ILogger<PersonController>>();
            var controller = new PersonController(mockLogger.Object, mockService.Object);

            var expected = new PersonDTO
            {
                PersonID = id,
                Address = address,
                City = city,
                FirstName = firstName,
                LastName = lastName
            };

            //Act
            var actual = controller.Get(0);
            int idActual = actual.Value;

            //Assert
            Assert.Equal(expected.PersonID, idActual);

        }

        [Theory]
        [InlineData(0, "Konrad", "Gaweda", "Zlota 23", "Warszawa")]
        [InlineData(1, "Pawel", "Golik", "Zelazna 21", "Poznan")]
        [InlineData(2, "Hosia", "Gadasz", "Sadyba 1", "Katowice noca")]
        [InlineData(3, "Cokolwiek", "Jakiekolwiek", "Jaroslawa Kaczynskiego 12", "Trybunal Konstytucyjny")]
        public void GetAll_Test(int id, string firstName, string lastName, string address, string city)
        {
            List<PersonDTO> people = new List<PersonDTO>();
            people.Add(new PersonDTO
            {
                PersonID = id,
                Address = address,
                City = city,
                FirstName = firstName,
                LastName = lastName
            });

            people.Add(new PersonDTO
            {
                PersonID = id+1,
                Address = address+"cokolwiek",
                City = city+"cokolwiek",
                FirstName = firstName+"cokolwiek",
                LastName = lastName+"cokolwiek"
            });

            //Arrange
            var mockService = new Mock<IPersonService>();
            mockService.Setup(x => x.GetAll()).Returns(new ServiceResult<IQueryable<PersonDTO>>(people.AsQueryable()));
            var mockLogger = new Mock<ILogger<PersonController>>();
            var controller = new PersonController(mockLogger.Object, mockService.Object);

            var expected = people;
            //Act
            var actual = controller.GetAll().Value.ToList();

            //Asset
            Assert.True(expected.All(shouldItem => actual.Any(isItem => isItem == shouldItem)));
        }

        [Theory]
        [InlineData(0, "Konrad", "Gaweda", "Zlota 23", "Warszawa")]
        [InlineData(1, "Pawel", "Golik", "Zelazna 21", "Poznan")]
        [InlineData(2, "Hosia", "Gadasz", "Sadyba 1", "Katowice noca")]
        [InlineData(3, "Cokolwiek", "Jakiekolwiek", "Jaroslawa Kaczynskiego 12", "Trybunal Konstytucyjny")]
        public void AddPersonDTO_Test(int id, string firstName, string lastName, string address, string city)
        {
            //Arrange
            var mockService = new Mock<IPersonService>();
            mockService.Setup(x => x.AddPersonAsync(It.IsAny<PersonDTO>())).Returns(Task.Run(()=>
            {
                return new ServiceResult<int?>(id);
            }));

            var mockLogger = new Mock<ILogger<PersonController>>();
            var controller = new PersonController(mockLogger.Object, mockService.Object);

            var expected = new PersonDTO
            {
                PersonID = id,
                Address = address,
                City = city,
                FirstName = firstName,
                LastName = lastName
            };

            //Act
            var actual = controller.AddPerson(new PersonDTO
            {
                PersonID = id,
                Address = address,
                City = city,
                FirstName = firstName,
                LastName = lastName
            }).Result.Value;

            //Assert
            Assert.Equal(expected.PersonID, actual);

        }


    }
}
