using System;
using WebApi.Database.Mapper;
using WebApi.Models.POCO;
using WebApi.Models.DTO;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio;

namespace WebApiTest.MapperTest
{
    public class MapperTest
    {

        public static IEnumerable<object[]> PersonPOCODataList()
        {
            yield return new object[]
            {
                new List<Person>
                {
                    new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" },
                    new Person { PersonID = 3, Address = "Skaryszewska 12" },
                    new Person { PersonID = 4, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Cis " },
                    new Person()
                }

             };
        }
        public static IEnumerable<object[]> PersonDTODataList()
        {
            yield return new object[]
            {
                new List<PersonDTO>
                {
                    new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" },
                    new PersonDTO { PersonID = 3, Address = "Skaryszewska 12" },
                    new PersonDTO { PersonID = 4, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Cis " },
                    new PersonDTO ()
                }

             };

        }

        public static IEnumerable<object[]> PersonDTOData()
        {
            yield return new object[] { new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" } };
            yield return new object[] { new PersonDTO { PersonID = 3, Address = "Skaryszewska 12" } };
            yield return new object[] { new PersonDTO { PersonID = 4, Address = "Koszykowa   343", City = "Kraków", FirstName = "Daniel", LastName = "Cis " } };
            yield return new object[] { new PersonDTO() };
        }

        public static IEnumerable<object[]> PersonPOCOData()
        {
            yield return new object[] { new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" } };
            yield return new object[] { new Person { PersonID = 3, Address = "Skaryszewska 12" } };
            yield return new object[] { new Person { PersonID = 4, Address = "Koszykowa   343", City = "Kraków", FirstName = "Daniel", LastName = "Cis " } };
            yield return new object[] { new Person() };
        }


        [Theory]
        [MemberData(nameof(PersonPOCOData))]
        public void POCOToDTOMapping(Person input)
        {

            PersonDTO result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }


        [Theory]
        [MemberData(nameof(PersonDTOData))]
        public void DTOToPOCOMapping(PersonDTO input)
        {
            Person result = Mapper.Map(input);


            Assert.Equal(input.PersonID, result.PersonID);
            Assert.Equal(input.FirstName, result.FirstName);
            Assert.Equal(input.LastName, result.LastName);
            Assert.Equal(input.Address, result.Address);
            Assert.Equal(input.City, result.City);
        }




        [Theory]
        [MemberData(nameof(PersonPOCODataList))]
        public void POCOToDTOMapping_List(List<Person> input)
        {
            var result = Mapper.Map(input.AsQueryable());

            Assert.True(result.All(result => input.Any(isItem =>
            isItem.PersonID == result.PersonID &&
            isItem.Address == result.Address &&
            isItem.City == result.City &&
            isItem.FirstName == result.FirstName &&
            isItem.LastName == result.LastName)));
        }


        [Theory]
        [MemberData(nameof(PersonDTODataList))]
        public void DTOToPOCOMapping_List(List<PersonDTO> input)
        {
            var result = Mapper.Map(input.AsQueryable());

            Assert.True(result.All(result => input.Any(isItem =>
            isItem.PersonID == result.PersonID &&
            isItem.Address == result.Address &&
            isItem.City == result.City &&
            isItem.FirstName == result.FirstName &&
            isItem.LastName == result.LastName)));
        }
        public static IEnumerable<object[]> CommentPOCODataList()
        {
            yield return new object[]
            {
                new List<Comment>
                {
                    new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "test" },
                    new Comment() { CommentID = 2, UserID = 2, PostID = 2, DateTime = DateTime.Today, Content = "test2" },
                    new Comment() { CommentID = 3, UserID = 3, PostID = 3, DateTime = DateTime.Today, Content = "test3" },
                    new Comment()
                }

             };
        }
        public static IEnumerable<object[]> CommentDTODataList()
        {
            yield return new object[]
            {
                new List<CommentDTO>
                {
                    new CommentDTO() { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "test" },
                    new CommentDTO() { CommentID = 2, UserID = 2, PostID = 2, DateTime = DateTime.Today, Content = "test2" },
                    new CommentDTO() { CommentID = 3, UserID = 3, PostID = 3, DateTime = DateTime.Today, Content = "test3" },
                    new CommentDTO()
                }

             };
        }
        public static IEnumerable<object[]> CommentDTOData()
        {
            yield return new object[] { new CommentDTO() { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "test" } };
            yield return new object[] { new CommentDTO() { CommentID = 2, UserID = 2, PostID = 2, DateTime = DateTime.Today, Content = "test2" } };
            yield return new object[] { new CommentDTO() { CommentID = 3, UserID = 3, PostID = 3, DateTime = DateTime.Today, Content = "test3" } };
            yield return new object[] { new CommentDTO() };
        }
        public static IEnumerable<object[]> CommentPOCOData()
        {
            yield return new object[] { new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "test" } };
            yield return new object[] { new Comment() { CommentID = 2, UserID = 2, PostID = 2, DateTime = DateTime.Today, Content = "test2" } };
            yield return new object[] { new Comment() { CommentID = 3, UserID = 3, PostID = 3, DateTime = DateTime.Today, Content = "test3" } };
            yield return new object[] { new Comment() };
        }

        [Theory]
        [MemberData(nameof(CommentPOCOData))]
        public void CommentPOCOToDTOMapping(Comment input)
        {

            CommentDTO result = Mapper.Map(input);


            Assert.Equal(input.CommentID, result.CommentID);
            Assert.Equal(input.Content, result.Content);
            Assert.Equal(input.DateTime, result.DateTime);
            Assert.Equal(input.PostID, result.PostID);
            Assert.Equal(input.UserID, result.UserID);
        }
        [Theory]
        [MemberData(nameof(CommentDTOData))]
        public void CommentDTOToPOCOMapping(CommentDTO input)
        {

            Comment result = Mapper.Map(input);


            Assert.Equal(input.CommentID, result.CommentID);
            Assert.Equal(input.Content, result.Content);
            Assert.Equal(input.DateTime, result.DateTime);
            Assert.Equal(input.PostID, result.PostID);
            Assert.Equal(input.UserID, result.UserID);
        }

        [Theory]
        [MemberData(nameof(CommentPOCODataList))]
        public void CommentPOCOToDTOMapping_List(List<Comment> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>

                isItem.CommentID == result.CommentID &&
                isItem.Content == result.Content &&
                isItem.DateTime == result.DateTime &&
                isItem.PostID == result.PostID &&
                isItem.UserID == result.UserID

            )));
        }
        [Theory]
        [MemberData(nameof(CommentDTODataList))]
        public void CommentDTOTOPOCOMapping_List(List<CommentDTO> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>

                isItem.CommentID == result.CommentID &&
                isItem.Content == result.Content &&
                isItem.DateTime == result.DateTime &&
                isItem.PostID == result.PostID &&
                isItem.UserID == result.UserID

            )));
        }


    }
}
