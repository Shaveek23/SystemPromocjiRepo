using System;
using WebApi.Database.Mapper;
using WebApi.Models.POCO;
using WebApi.Models.DTO;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Eventing.Reader;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;

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
                }

             };

        }

        public static IEnumerable<object[]> PersonDTOData()
        {
            yield return new object[] { new PersonDTO { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" } };
            yield return new object[] { new PersonDTO { PersonID = 3, Address = "Skaryszewska 12" } };
            yield return new object[] { new PersonDTO { PersonID = 4, Address = "Koszykowa   343", City = "Kraków", FirstName = "Daniel", LastName = "Cis " } };
        }

        public static IEnumerable<object[]> PersonPOCOData()
        {
            yield return new object[] { new Person { PersonID = 1, Address = "Koszykowa 3", City = "Warszawa", FirstName = "Damian", LastName = "Bis" } };
            yield return new object[] { new Person { PersonID = 3, Address = "Skaryszewska 12" } };
            yield return new object[] { new Person { PersonID = 4, Address = "Koszykowa   343", City = "Kraków", FirstName = "Daniel", LastName = "Cis " } };
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

        public static IEnumerable<object[]> UserDTOList()
        {
            yield return new object[]
            {
                new List<UserDTO>
                {

                    new UserDTO() { ID=0,
                        IsActive=true,
                        IsAdmin=false,
                        IsEntrepreneur=true,
                        IsVerified=true,
                        Timestamp=new DateTime(1999,12,12,12,12,12),
                        UserEmail="konrad.gaw3@gmail.com",
                        UserName="gawezi"
                    },
                    new UserDTO() { ID=12,
                        IsActive=true,
                        IsAdmin=false,
                        IsEntrepreneur=true,
                        IsVerified=true,
                        Timestamp=new DateTime(2012,12,12,12,12,12),
                        UserEmail="12@12.12",
                        UserName="12"
                    },
                    new UserDTO() { ID=420,
                        IsActive=true,
                        IsAdmin=false,
                        IsEntrepreneur=true,
                        IsVerified=true,
                        Timestamp=new DateTime(1932,11,11,11,11,11),
                        UserEmail="test@test.test",
                        UserName="test"
                    },
                }

             };
        }
        public static IEnumerable<object[]> UserPOCOList()
        {
            yield return new object[]
            {
                new List<User>
                {

                    new User() { UserID=0,
                        Active=true,
                        IsAdmin=false,
                        IsEnterprenuer=true,
                        IsVerified=true,
                        Timestamp=new DateTime(1999,12,12,12,12,12),
                        UserEmail="konrad.gaw3@gmail.com",
                        UserName="gawezi"
                    },
                    new User() { UserID=12,
                        Active=true,
                        IsAdmin=false,
                        IsEnterprenuer=true,
                        IsVerified=true,
                        Timestamp=new DateTime(2012,12,12,12,12,12),
                        UserEmail="12@12.12",
                        UserName="12"
                    },
                    new User() { UserID=420,
                        Active=true,
                        IsAdmin=false,
                        IsEnterprenuer=true,
                        IsVerified=true,
                        Timestamp=new DateTime(1932,11,11,11,11,11),
                        UserEmail="test@test.test",
                        UserName="test"
                    },
                }

             };
        }



        [Theory]
        [InlineData(1, "test1", "test@test.test", true, true, true, true)]
        [InlineData(2, "test543", "xc@lol.pl", true, false, false, false)]
        [InlineData(1, "", "", false, false, false, true)]
        public void UserDTOtoPOCOMapping(int id, string name, string email, bool active, bool entrepreneur, bool verified, bool admin)
        {
            UserDTO userDTO = new UserDTO { ID = id, UserName = name, IsActive = active, IsEntrepreneur = entrepreneur, UserEmail = email, Timestamp = new DateTime(2012, 12, 12, 12, 12, 12), IsAdmin = admin, IsVerified = verified };
            User user = Mapper.Map(userDTO);
            Assert.Equal(user.UserID, userDTO.ID);
            Assert.Equal(user.UserName, userDTO.UserName);
            Assert.Equal(user.UserEmail, userDTO.UserEmail);
            Assert.Equal(user.Active, userDTO.IsActive);
            Assert.Equal(user.IsEnterprenuer, userDTO.IsEntrepreneur);
        }


        [Theory]
        [InlineData(1, "test1", "test@test.test", true, true)]
        [InlineData(2, "test543", "xc@lol.pl", true, false)]
        [InlineData(1, "", "", false, false)]
        public void UserPOCOtoDROMapping(int id, string name, string email, bool active, bool entrepreneur)
        {
            User user = new User { UserID = id, UserName = name, Active = active, IsEnterprenuer = entrepreneur, UserEmail = email, Timestamp = new DateTime(2012, 12, 12, 12, 12, 12) };
            UserDTO userDTO = Mapper.Map(user);
            Assert.Equal(user.UserID, userDTO.ID);
            Assert.Equal(user.UserName, userDTO.UserName);
            Assert.Equal(user.UserEmail, userDTO.UserEmail);
            Assert.Equal(user.Active, userDTO.IsActive);
            Assert.Equal(user.IsEnterprenuer, userDTO.IsEntrepreneur);
        }

        [Theory]
        [MemberData(nameof(UserPOCOList))]
        public void UserPOCOToDTOMapping_List(List<User> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                        isItem.UserID == result.ID &&
                        isItem.UserName == result.UserName &&
                        isItem.UserEmail == result.UserEmail &&
                        isItem.Active == result.IsActive &&
                        isItem.Timestamp == result.Timestamp &&
                        isItem.IsEnterprenuer == result.IsEntrepreneur


            )));
        }

        [Theory]
        [MemberData(nameof(UserDTOList))]
        public void UserDTOToPOCOMapping_List(List<UserDTO> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                        isItem.ID == result.UserID &&
                        isItem.UserName == result.UserName &&
                        isItem.UserEmail == result.UserEmail &&
                        isItem.IsActive == result.Active &&
                        isItem.Timestamp == result.Timestamp &&
                        isItem.IsEntrepreneur == result.IsEnterprenuer


            )));
        }



        public static IEnumerable<object[]> CategoryDTOData()
        {
            yield return new object[] { new CategoryDTO { ID = 1, Name = "test1" } };
            yield return new object[] { new CategoryDTO { ID = 0, Name = "" } };
            yield return new object[] { new CategoryDTO { ID = int.MaxValue, Name = "2" } };
        }

        public static IEnumerable<object[]> CategoryDTOList()
        {
            yield return new object[]
            {
                new List<CategoryDTO>
                {

                    new CategoryDTO() { ID=0,Name="test1" },
                    new CategoryDTO() { ID=int.MaxValue, Name="test2" },
                    new CategoryDTO() {  ID=1, Name="" },
                }

             };
        }
        public static IEnumerable<object[]> CategoryPOCOList()
        {
            yield return new object[]
            {
                new List<Category>
                {

                    new Category() { CategoryID=0,Name="test1" },
                    new Category() { CategoryID=int.MaxValue, Name="test2" },
                    new Category() {  CategoryID=1, Name="" },
                }

             };
        }

        [Theory]
        [InlineData(1, "test1")]
        [InlineData(0, "")]
        [InlineData(5, "Jakas kategoria")]
        public void CategoryDTOtoPOCOMapping(int id, string name)
        {
            CategoryDTO categoryDTO = new CategoryDTO { ID = id, Name = name };
            Category category = Mapper.Map(categoryDTO);
            Assert.Equal(categoryDTO.ID, category.CategoryID);
            Assert.Equal(categoryDTO.Name, category.Name);
        }

        [Theory]
        [InlineData(1, "test1")]
        [InlineData(0, "")]
        [InlineData(5, "Jakas kategoria")]
        public void CategoryPOCOtoDROMapping(int id, string name)
        {
            Category category = new Category { CategoryID = id, Name = name };
            CategoryDTO categoryDTO = Mapper.Map(category);
            Assert.Equal(categoryDTO.ID, category.CategoryID);
            Assert.Equal(categoryDTO.Name, category.Name);
        }

        [Theory]
        [MemberData(nameof(CategoryPOCOList))]
        public void CategoryPOCOToDTOMapping_List(List<Category> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                isItem.CategoryID == result.ID &&
                isItem.Name == result.Name
            )));
        }

        [Theory]
        [MemberData(nameof(CategoryDTOList))]
        public void CategoryDTOToPOCOMapping_List(List<CategoryDTO> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                isItem.ID == result.CategoryID &&
                isItem.Name == result.Name
            )));
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
                }

             };
        }

        public static IEnumerable<object[]> CommentDTOOutputDataList()
        {
            yield return new object[]
            {
                new List<CommentDTOOutput>
                {
                    new CommentDTOOutput() { id=1},
                    new CommentDTOOutput() { id = 2, authorID = 2, postId = 2, date = DateTime.Today, content = "test2" },
                    new CommentDTOOutput() { id = 3, authorID = 3, postId = 3, date = DateTime.Today, content = "test3"}
                }

             };
        }

        public static IEnumerable<object[]> CommentDTONewDataList()
        {
            yield return new object[]
            {
                new List<CommentDTONew>
                {

                    new CommentDTONew() {  PostID = 1,  Content = "test" },
                    new CommentDTONew() {  PostID = 2,  Content = "test2" },
                    new CommentDTONew() {   PostID = 3,  Content = "test3" },
                }

             };
        }
        public static IEnumerable<object[]> CommentDTOEditDataList()
        {
            yield return new object[]
            {
                new List<CommentDTOEdit>
                {

                    new CommentDTOEdit() {    Content = "test" },
                    new CommentDTOEdit() {   Content = "test2" },
                    new CommentDTOEdit() {     Content = "test3" },
                }

             };
        }
        public static IEnumerable<object[]> CommentDTONewData()
        {

            yield return new object[] { new CommentDTONew() { PostID = 1, Content = "test" } };
            yield return new object[] { new CommentDTONew() { PostID = 2, Content = "test2" } };
            yield return new object[] { new CommentDTONew() { PostID = 3, Content = "test3" } };

        }
        public static IEnumerable<object[]> CommentDTOEditData()
        {

            yield return new object[] { new CommentDTOEdit() { Content = "test" } };
            yield return new object[] { new CommentDTOEdit() { Content = "test2" } };
            yield return new object[] { new CommentDTOEdit() { Content = "test3" } };
        }

        public static IEnumerable<object[]> CommentPOCOData()
        {
            yield return new object[] { new Comment() { CommentID = 1, UserID = 1, PostID = 1, DateTime = DateTime.Today, Content = "test" } };
            yield return new object[] { new Comment() { CommentID = 2, UserID = 2, PostID = 2, DateTime = DateTime.Today, Content = "test2" } };
            yield return new object[] { new Comment() { CommentID = 3, UserID = 3, PostID = 3, DateTime = DateTime.Today, Content = "test3" } };
        }

        public static IEnumerable<object[]> CommentDTOOutputData()
        {
            yield return new object[] { new CommentDTOOutput() { id = 1 } };
            yield return new object[] { new CommentDTOOutput() { id = 2, authorID = 2, postId = 2, date = DateTime.Today, content = "test2" } };
            yield return new object[] { new CommentDTOOutput() { id = 3, authorID = 3, postId = 3, date = DateTime.Today, content = "test3" } };
        }



      
        [Theory]
        [MemberData(nameof(CommentPOCOData))]
        public void CommentPOCOToDTOOutputMapping(Comment input)
        {

            CommentDTOOutput result = Mapper.MapOutput(input);


            Assert.Equal(input.CommentID, result.id);
            Assert.Equal(input.Content, result.content);
            Assert.Equal(input.DateTime, result.date);
            Assert.Equal(input.PostID, result.postId);
            Assert.Equal(input.UserID, result.authorID);


        }

        [Theory]
        [MemberData(nameof(CommentDTONewData))]
        public void CommentDTONewToPOCOMapping(CommentDTONew input)
        {

            Comment result = Mapper.Map(input);
            Assert.Equal(input.Content, result.Content);
            Assert.Equal(input.PostID, result.PostID);

        }

        [Theory]
        [MemberData(nameof(CommentDTOEditData))]
        public void CommentDTOEditToPOCOMapping(CommentDTOEdit input)
        {

            Comment result = Mapper.Map(input);
            Assert.Equal(input.Content, result.Content);


        }

      

        [Theory]
        [MemberData(nameof(CommentPOCODataList))]
        public void CommentPOCOToDTOOutputMapping_List(List<Comment> input)
        {
            var result = Mapper.MapOutput(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>

                isItem.CommentID == result.id &&
                isItem.Content == result.content &&
                isItem.DateTime == result.date &&
                isItem.PostID == result.postId &&
                isItem.UserID == result.authorID

            )));
        }

   

        public static IEnumerable<object[]> CommentLikePOCODataList()
        {
            yield return new object[]
            {
                new List<CommentLike>
                {
                    new CommentLike() { CommentID=1, CommentLikeID=0, UserID=2 },
                    new CommentLike() { CommentID=2, CommentLikeID=1, UserID=3 },
                    new CommentLike() { CommentID=1, CommentLikeID=1, UserID=1 },
                }

             };
        }
        public static IEnumerable<object[]> CommentLikeDTODataList()
        {
            yield return new object[]
            {
                new List<CommentLikeDTO>
                {
                    new CommentLikeDTO() { CommentID=1, CommentLikeID=0, UserID=2 },
                    new CommentLikeDTO() { CommentID=2, CommentLikeID=1, UserID=3 },
                    new CommentLikeDTO() { CommentID=1, CommentLikeID=1, UserID=1 },
                }

             };
        }

        public static IEnumerable<object[]> PostLikePOCODataList()
        {
            yield return new object[]
            {
                new List<PostLike>
                {
                    new PostLike() { PostID=1, PostLikeID=0, UserID=2 },
                    new PostLike() { PostID=2, PostLikeID=1, UserID=3 },
                    new PostLike() { PostID=1, PostLikeID=1, UserID=1 },
                }

             };
        }
        public static IEnumerable<object[]> PostLikeDTODataList()
        {
            yield return new object[]
            {
                new List<PostLikeDTO>
                {
                    new PostLikeDTO() { PostID=1, PostLikeID=0, UserID=2 },
                    new PostLikeDTO() { PostID=2, PostLikeID=1, UserID=3 },
                    new PostLikeDTO() { PostID=1, PostLikeID=1, UserID=1 },
                }

             };
        }

        [Theory]
        [MemberData(nameof(CommentLikeDTODataList))]
        public void CommentLikeDTOtoPOCOMapping_List(List<CommentLikeDTO> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>




                isItem.CommentID == result.CommentID &&
                isItem.CommentLikeID == result.CommentLikeID &&
                isItem.UserID == result.UserID

            )));
        }
        [Theory]
        [MemberData(nameof(CommentLikePOCODataList))]
        public void CommentLikePOCOtoDTOMapping_List(List<CommentLike> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>




                isItem.CommentID == result.CommentID &&
                isItem.CommentLikeID == result.CommentLikeID &&
                isItem.UserID == result.UserID

            )));
        }
        [Theory]
        [MemberData(nameof(PostLikeDTODataList))]
        public void PostLikeDTOtoPOCOMapping_List(List<PostLikeDTO> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>




                isItem.PostID == result.PostID &&
                isItem.PostLikeID == result.PostLikeID &&
                isItem.UserID == result.UserID

            )));
        }
        [Theory]
        [MemberData(nameof(PostLikePOCODataList))]
        public void PostLikePOCOtoDTOMapping_List(List<PostLike> input)
        {
            var result = Mapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>




                isItem.PostID == result.PostID &&
                isItem.PostLikeID == result.PostLikeID &&
                isItem.UserID == result.UserID

            )));
        }

        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 2)]
        [InlineData(5, int.MaxValue, 3)]
        public void CommentLikeDTOtoPOCOMapping(int id1, int id2, int id3)
        {
            CommentLikeDTO commentLikeDTO = new CommentLikeDTO { CommentID = id1, CommentLikeID = id2, UserID = id3 };
            CommentLike commentLike = Mapper.Map(commentLikeDTO);
            Assert.True(commentLikeDTO.CommentID == commentLike.CommentID && commentLikeDTO.CommentLikeID == commentLike.CommentLikeID && commentLikeDTO.UserID == commentLike.UserID);
        }
        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 2)]
        [InlineData(5, int.MaxValue, 3)]
        public void CommentLikePOCOtoDTOMapping(int id1, int id2, int id3)
        {
            CommentLike commentLike = new CommentLike { CommentID = id1, CommentLikeID = id2, UserID = id3 };
            CommentLikeDTO commentLikeDTO = Mapper.Map(commentLike);
            Assert.True(commentLikeDTO.CommentID == commentLike.CommentID && commentLikeDTO.CommentLikeID == commentLike.CommentLikeID && commentLikeDTO.UserID == commentLike.UserID);
        }

        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 2)]
        [InlineData(5, int.MaxValue, 3)]
        public void PostLikeDTOtoPOCOMapping(int id1, int id2, int id3)
        {
            PostLikeDTO commentLikeDTO = new PostLikeDTO { PostID = id1, PostLikeID = id2, UserID = id3 };
            PostLike commentLike = Mapper.Map(commentLikeDTO);
            Assert.True(commentLikeDTO.PostID == commentLike.PostID && commentLikeDTO.PostLikeID == commentLike.PostLikeID && commentLikeDTO.UserID == commentLike.UserID);
        }
        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 2)]
        [InlineData(5, int.MaxValue, 3)]
        public void PostLikePOCOtoDTOMapping(int id1, int id2, int id3)
        {
            PostLike commentLike = new PostLike { PostID = id1, PostLikeID = id2, UserID = id3 };
            PostLikeDTO commentLikeDTO = Mapper.Map(commentLike);
            Assert.True(commentLikeDTO.PostID == commentLike.PostID && commentLikeDTO.PostLikeID == commentLike.PostLikeID && commentLikeDTO.UserID == commentLike.UserID);
        }


    }
}
