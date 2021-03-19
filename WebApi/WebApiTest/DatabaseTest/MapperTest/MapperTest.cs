using System;
using WebApi.Database.Mapper;
using WebApi.Models.POCO;
using WebApi.Models.DTO;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace WebApiTest.DatabaseTest.MapperTest
{
    public class MapperTest
    {
       
        [Fact]
        public void POCOToDTOMapping1()
        {
            Person input = new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };
            PersonDTO expectedResult = new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };

            PersonDTO result = Mapper.Map(input);


            Assert.Equal(result.PersonID, expectedResult.PersonID);
            Assert.Equal(result.FirstName, expectedResult.FirstName);
            Assert.Equal(result.LastName, expectedResult.LastName);
            Assert.Equal(result.Address, expectedResult.Address);
            Assert.Equal(result.City, expectedResult.City);
        }

        [Fact]
        public void POCOToDTOMapping2()
        {
            Person input = new Person { PersonID = 3, Address = "Skaryszewska 12"};
            PersonDTO expectedResult = new PersonDTO { PersonID = 3, Address = "Skaryszewska 12" };

            PersonDTO result = Mapper.Map(input);


            Assert.Equal(result.PersonID, expectedResult.PersonID);
            Assert.Equal(result.FirstName, expectedResult.FirstName);
            Assert.Equal(result.LastName, expectedResult.LastName);
            Assert.Equal(result.Address, expectedResult.Address);
            Assert.Equal(result.City, expectedResult.City);
        }

        [Fact]
        public void POCOToDTOMapping3()
        {
            Person input = new Person ();
            PersonDTO expectedResult = new PersonDTO ();

            PersonDTO result = Mapper.Map(input);


            Assert.Equal(result.PersonID, expectedResult.PersonID);
            Assert.Equal(result.FirstName, expectedResult.FirstName);
            Assert.Equal(result.LastName, expectedResult.LastName);
            Assert.Equal(result.Address, expectedResult.Address);
            Assert.Equal(result.City, expectedResult.City);
        }

        [Fact]
        public void POCOToDTOMapping4()
        {
            Person input = new Person { PersonID = 4, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = " Bis" };
            PersonDTO expectedResult = new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };

            PersonDTO result = Mapper.Map(input);


            Assert.NotEqual(result.PersonID, expectedResult.PersonID);
            Assert.Equal(result.FirstName, expectedResult.FirstName);
            Assert.NotEqual(result.LastName, expectedResult.LastName);
            Assert.Equal(result.Address, expectedResult.Address);
            Assert.Equal(result.City, expectedResult.City);
        }

        [Fact]
        public void DTOToPOCOMapping1()
        {
            PersonDTO input = new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };
            Person expectedResult = new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };

            Person result = Mapper.Map(input);


            Assert.Equal(result.PersonID, expectedResult.PersonID);
            Assert.Equal(result.FirstName, expectedResult.FirstName);
            Assert.Equal(result.LastName, expectedResult.LastName);
            Assert.Equal(result.Address, expectedResult.Address);
            Assert.Equal(result.City, expectedResult.City);
        }

        [Fact]
        public void DTOToPOCOMapping2()
        {
            PersonDTO input = new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };
            Person expectedResult = new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };

            Person result = Mapper.Map(input);


            Assert.Equal(result.PersonID, expectedResult.PersonID);
            Assert.Equal(result.FirstName, expectedResult.FirstName);
            Assert.Equal(result.LastName, expectedResult.LastName);
            Assert.Equal(result.Address, expectedResult.Address);
            Assert.Equal(result.City, expectedResult.City);
        }

        [Fact]
        public void DTOToPOCOMapping3()
        {
            PersonDTO input = new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };
            Person expectedResult = new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" };

            Person result = Mapper.Map(input);

            Assert.Equal(result.PersonID, expectedResult.PersonID);
            Assert.Equal(result.FirstName, expectedResult.FirstName);
            Assert.Equal(result.LastName, expectedResult.LastName);
            Assert.Equal(result.Address, expectedResult.Address);
            Assert.Equal(result.City, expectedResult.City);
        }



        [Fact]
        public void DTOToPOCOMapping4()
        {
            Person input = new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Dam ian", LastName = "Bis" };
            PersonDTO expectedResult = new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warssszawa", FirstName = "Damian", LastName = "Bis" };

            PersonDTO result = Mapper.Map(input);


            Assert.Equal(result.PersonID, expectedResult.PersonID);
            Assert.NotEqual(result.FirstName, expectedResult.FirstName);
            Assert.Equal(result.LastName, expectedResult.LastName);
            Assert.Equal(result.Address, expectedResult.Address);
            Assert.NotEqual(result.City, expectedResult.City);
        }



        [Fact]
        public void POCOToDTOMapping1_List()
        {
            List<Person> input = new List<Person>
                {
                    new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" },
                    new Person { PersonID = 3, Address = "Koszykowa 8", City = "Warszawa", FirstName = "Daniel", LastName = "Dis" },
                    new Person { PersonID = 8, Address = " Koszykowa 123  ", City = "Warszawa", FirstName = "Dawid", LastName = "Cis" },
                };
            List<PersonDTO> expectedResult =new List<PersonDTO>
                {
                    new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" },
                    new PersonDTO { PersonID = 3, Address = "Koszykowa 8", City = "Warszawa", FirstName = "Daniel", LastName = "Dis" },
                    new PersonDTO { PersonID = 8, Address = " Koszykowa 123  ", City = "Warszawa", FirstName = "Dawid", LastName = "Cis" },
                };



            List<PersonDTO> result = Mapper.Map(input.AsQueryable()).ToList();

            for (int i=0; i<input.Count; i++)
            {
                Assert.Equal(result[i].PersonID, expectedResult[i].PersonID);
                Assert.Equal(result[i].FirstName, expectedResult[i].FirstName);
                Assert.Equal(result[i].LastName, expectedResult[i].LastName);
                Assert.Equal(result[i].Address, expectedResult[i].Address);
                Assert.Equal(result[i].City, expectedResult[i].City);
            }
        }


        [Fact]
        public void DTOToPOCOMapping1_List()
        {
            List<PersonDTO> input = new List<PersonDTO>
                {
                    new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" },
                    new PersonDTO { PersonID = 3, Address = "Koszykowa 8", City = "Warszawa", FirstName = "Daniel", LastName = "Dis" },
                    new PersonDTO { PersonID = 8, Address = " Koszykowa 123  ", City = "Warszawa", FirstName = "Dawid", LastName = "Cis" }
                };
            List<Person> expectedResult = new List<Person>
                {
                    new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" },
                    new Person { PersonID = 3, Address = "Koszykowa 8", City = "Warszawa", FirstName = "Daniel", LastName = "Dis" },
                    new Person { PersonID = 8, Address = " Koszykowa 123  ", City = "Warszawa", FirstName = "Dawid", LastName = "Cis" }
                };



            List<Person> result = Mapper.Map(input.AsQueryable()).ToList();

            for (int i = 0; i < input.Count; i++)
            {
                Assert.Equal(result[i].PersonID, expectedResult[i].PersonID);
                Assert.Equal(result[i].FirstName, expectedResult[i].FirstName);
                Assert.Equal(result[i].LastName, expectedResult[i].LastName);
                Assert.Equal(result[i].Address, expectedResult[i].Address);
                Assert.Equal(result[i].City, expectedResult[i].City);
            }
        }
    }
}
