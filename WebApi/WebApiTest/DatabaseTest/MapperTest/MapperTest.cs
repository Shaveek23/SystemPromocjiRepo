using System;
using WebApi.Database.Mapper;
using WebApi.Models.POCO;
using WebApi.Models.DTO;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio;

namespace WebApiTest.DatabaseTest.MapperTest
{
    public class MapperTest
    {

        List<Person> getMockedPersonList()
        {
            return new List<Person>
            {
                    new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" },
                    new Person { PersonID = 3, Address = "Skaryszewska 12" },
                    new Person { PersonID = 4, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Cis " },
                    new Person()
             };
        }
        List<PersonDTO> getMockedPersonDTOList()
        {
            return new List<PersonDTO>
            {
                    new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" },
                    new PersonDTO { PersonID = 3, Address = "Skaryszewska 12" },
                    new PersonDTO { PersonID = 4, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Cis " },
                    new PersonDTO ()
             };

        }


        [Fact]
        public void POCOToDTOMapping1()
        {
            Person input = getMockedPersonList()[0];
            PersonDTO result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }

        [Fact]
        public void POCOToDTOMapping2()
        {
            Person input = getMockedPersonList()[1];
            PersonDTO result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }

        [Fact]
        public void POCOToDTOMapping3()
        {
            Person input = getMockedPersonList()[2];
            PersonDTO result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }

        [Fact]
        public void POCOToDTOMapping4()
        {
            Person input = getMockedPersonList()[3];
            PersonDTO result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }

        [Fact]
        public void DTOToPOCOMapping1()
        {
            PersonDTO input = getMockedPersonDTOList()[0];
            Person result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }

        [Fact]
        public void DTOToPOCOMapping2()
        {
            PersonDTO input = getMockedPersonDTOList()[1];
            Person result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }

        [Fact]
        public void DTOToPOCOMapping3()
        {
            PersonDTO input = getMockedPersonDTOList()[2];
            Person result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }



        [Fact]
        public void DTOToPOCOMapping4()
        {
            PersonDTO input = getMockedPersonDTOList()[3];
            Person result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }



        [Fact]
        public void POCOToDTOMapping1_List()
        {
            var input = getMockedPersonList();
            var result = getMockedPersonDTOList();
            var expectedResult = Mapper.Map(input.AsQueryable());

            Assert.True(result.All(expectedResult => result.Any(isItem => isItem == expectedResult)));
        }


        [Fact]
        public void DTOToPOCOMapping1_List()
        {
            var input = getMockedPersonDTOList();
            var result = getMockedPersonList();
            var expectedResult = Mapper.Map(input.AsQueryable());

            Assert.True(result.All(expectedResult => result.Any(isItem => isItem == expectedResult)));
        }
    }
}
