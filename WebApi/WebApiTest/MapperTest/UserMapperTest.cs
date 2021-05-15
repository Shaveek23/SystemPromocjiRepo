using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Database.Mapper;
using WebApi.Models.DTO.UserDTOs;
using WebApi.Models.POCO;
using Xunit;

namespace WebApiTest.MapperTest
{
    public class UserMapperTest
    {
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
                        UserEmail="konrad.gaw3@gmail.com",
                        UserName="gawezi"
                    },
                    new User() { UserID=12,
                        Active=true,
                        IsAdmin=false,
                        IsEnterprenuer=true,
                        IsVerified=true,
                        UserEmail="12@12.12",
                        UserName="12"
                    },
                    new User() { UserID=420,
                        Active=true,
                        IsAdmin=false,
                        IsEnterprenuer=true,
                        IsVerified=true,
                        UserEmail="test@test.test",
                        UserName="test"
                    },
                }

             };
        }
        public static IEnumerable<object[]> UserGetDTOList()
        {
            yield return new object[]
            {
                new List<UserGetDTO>
                {

                    new UserGetDTO() { id=0,
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="konrad.gaw3@gmail.com",
                        userName="gawezi"
                    },
                    new UserGetDTO() { id=12,
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="12@12.12",
                        userName="12"
                    },
                    new UserGetDTO() {id=420,
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="test@test.test",
                        userName="test"
                    },
                }

             };
        }
        public static IEnumerable<object[]> UserPostDTOList()
        {
            yield return new object[]
            {
                new List<UserPostDTO>
                {

                    new UserPostDTO() {
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="konrad.gaw3@gmail.com",
                        userName="gawezi"
                    },
                    new UserPostDTO() {
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="12@12.12",
                        userName="12"
                    },
                    new UserPostDTO() {
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="test@test.test",
                        userName="test"
                    },
                }

             };
        }
        public static IEnumerable<object[]> UserPutDTOList()
        {
            yield return new object[]
            {
                new List<UserPutDTO>
                {

                    new UserPutDTO() {
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="konrad.gaw3@gmail.com",
                        userName="gawezi"
                    },
                    new UserPutDTO() {
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="12@12.12",
                        userName="12"
                    },
                    new UserPutDTO() {
                        isActive=true,
                        isAdmin=false,
                        isEntrepreneur=true,
                        isVerified=true,
                        userEmail="test@test.test",
                        userName="test"
                    },
                }

             };
        }

        //GET
        [Theory]
        [InlineData(1, "test1", "test@test.test", true, true, true, true)]
        [InlineData(2, "test543", "xc@lol.pl", true, false, false, false)]
        [InlineData(1, "", "", false, false, false, true)]
        public void UserDTOGettoPOCOMapping(int id, string name, string email, bool active, bool entrepreneur, bool verified, bool admin)
        {
            UserGetDTO userDTO = new UserGetDTO { id = id, userName = name, isActive = active, isEntrepreneur = entrepreneur, userEmail = email, isAdmin = admin, isVerified = verified };
            User user = UserMapper.Map(userDTO);
            Assert.Equal(user.UserID, userDTO.id);
            Assert.Equal(user.UserName, userDTO.userName);
            Assert.Equal(user.UserEmail, userDTO.userEmail);
            Assert.Equal(user.Active, userDTO.isActive);
            Assert.Equal(user.IsEnterprenuer, userDTO.isEntrepreneur);
        }


        [Theory]
        [InlineData(1, "test1", "test@test.test", true, true)]
        [InlineData(2, "test543", "xc@lol.pl", true, false)]
        [InlineData(1, "", "", false, false)]
        public void UserPOCOtoGetDTOMapping(int id, string name, string email, bool active, bool entrepreneur)
        {
            User user = new User { UserID = id, UserName = name, Active = active, IsEnterprenuer = entrepreneur, UserEmail = email, Timestamp = new DateTime(2012, 12, 12, 12, 12, 12) };
            UserGetDTO userDTO = UserMapper.MapGet(user);
            Assert.Equal(user.UserID, userDTO.id);
            Assert.Equal(user.UserName, userDTO.userName);
            Assert.Equal(user.UserEmail, userDTO.userEmail);
            Assert.Equal(user.Active, userDTO.isActive);
            Assert.Equal(user.IsEnterprenuer, userDTO.isEntrepreneur);
        }

        [Theory]
        [MemberData(nameof(UserPOCOList))]
        public void UserPOCOToGetDTOMapping_List(List<User> input)
        {
            var result = UserMapper.MapGet(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                        isItem.UserID == result.id &&
                        isItem.UserName == result.userName &&
                        isItem.UserEmail == result.userEmail &&
                        isItem.Active == result.isActive &&
                        isItem.IsEnterprenuer == result.isEntrepreneur


            )));
        }

        [Theory]
        [MemberData(nameof(UserGetDTOList))]
        public void UserGetDTOToPOCOMapping_List(List<UserGetDTO> input)
        {
            var result = UserMapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                        isItem.id == result.UserID &&
                        isItem.userName == result.UserName &&
                        isItem.userEmail == result.UserEmail &&
                        isItem.isActive == result.Active &&
                        isItem.isEntrepreneur == result.IsEnterprenuer


            )));
        }


        //POST

        [Theory]
        [InlineData( "test1", "test@test.test", true, true, true, true)]
        [InlineData( "test543", "xc@lol.pl", true, false, false, false)]
        [InlineData( "", "", false, false, false, true)]
        public void UserDTOPosttoPOCOMapping( string name, string email, bool active, bool entrepreneur, bool verified, bool admin)
        {
            UserPostDTO userDTO = new UserPostDTO {  userName = name, isActive = active, isEntrepreneur = entrepreneur, userEmail = email, isAdmin = admin, isVerified = verified };
            User user = UserMapper.Map(userDTO);
            Assert.Equal(user.UserName, userDTO.userName);
            Assert.Equal(user.UserEmail, userDTO.userEmail);
            Assert.Equal(user.Active, userDTO.isActive);
            Assert.Equal(user.IsEnterprenuer, userDTO.isEntrepreneur);
        }


        [Theory]
        [InlineData(1, "test1", "test@test.test", true, true)]
        [InlineData(2, "test543", "xc@lol.pl", true, false)]
        [InlineData(1, "", "", false, false)]
        public void UserPOCOtoPostDTOMapping(int id, string name, string email, bool active, bool entrepreneur)
        {
            User user = new User { UserID = id, UserName = name, Active = active, IsEnterprenuer = entrepreneur, UserEmail = email, Timestamp = new DateTime(2012, 12, 12, 12, 12, 12) };
            UserPostDTO userDTO = UserMapper.MapPost(user);
            Assert.Equal(user.UserName, userDTO.userName);
            Assert.Equal(user.UserEmail, userDTO.userEmail);
            Assert.Equal(user.Active, userDTO.isActive);
            Assert.Equal(user.IsEnterprenuer, userDTO.isEntrepreneur);
        }

        [Theory]
        [MemberData(nameof(UserPOCOList))]
        public void UserPOCOToPostDTOMapping_List(List<User> input)
        {
            var result = UserMapper.MapPost(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                        isItem.UserName == result.userName &&
                        isItem.UserEmail == result.userEmail &&
                        isItem.Active == result.isActive &&
                        isItem.IsEnterprenuer == result.isEntrepreneur


            )));
        }

        [Theory]
        [MemberData(nameof(UserPostDTOList))]
        public void UserPostDTOToPOCOMapping_List(List<UserPostDTO> input)
        {
            var result = UserMapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                        isItem.userName == result.UserName &&
                        isItem.userEmail == result.UserEmail &&
                        isItem.isActive == result.Active &&
                        isItem.isEntrepreneur == result.IsEnterprenuer


            )));
        }

        //PUT
        [Theory]
        [InlineData("test1", "test@test.test", true, true, true, true)]
        [InlineData("test543", "xc@lol.pl", true, false, false, false)]
        [InlineData("", "", false, false, false, true)]
        public void UserDTOPuttoPOCOMapping(string name, string email, bool active, bool entrepreneur, bool verified, bool admin)
        {
            UserPutDTO userDTO = new UserPutDTO { userName = name, isActive = active, isEntrepreneur = entrepreneur, userEmail = email, isAdmin = admin, isVerified = verified };
            User user = UserMapper.Map(userDTO);
            Assert.Equal(user.UserName, userDTO.userName);
            Assert.Equal(user.UserEmail, userDTO.userEmail);
            Assert.Equal(user.Active, userDTO.isActive);
            Assert.Equal(user.IsEnterprenuer, userDTO.isEntrepreneur);
        }


        [Theory]
        [InlineData(1, "test1", "test@test.test", true, true)]
        [InlineData(2, "test543", "xc@lol.pl", true, false)]
        [InlineData(1, "", "", false, false)]
        public void UserPOCOtoPutDTOMapping(int id, string name, string email, bool active, bool entrepreneur)
        {
            User user = new User { UserID = id, UserName = name, Active = active, IsEnterprenuer = entrepreneur, UserEmail = email, Timestamp = new DateTime(2012, 12, 12, 12, 12, 12) };
            UserPutDTO userDTO = UserMapper.MapPut(user);
            Assert.Equal(user.UserName, userDTO.userName);
            Assert.Equal(user.UserEmail, userDTO.userEmail);
            Assert.Equal(user.Active, userDTO.isActive);
            Assert.Equal(user.IsEnterprenuer, userDTO.isEntrepreneur);
        }

        [Theory]
        [MemberData(nameof(UserPOCOList))]
        public void UserPOCOToPutDTOMapping_List(List<User> input)
        {
            var result = UserMapper.MapPut(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                        isItem.UserName == result.userName &&
                        isItem.UserEmail == result.userEmail &&
                        isItem.Active == result.isActive &&
                        isItem.IsEnterprenuer == result.isEntrepreneur


            )));
        }

        [Theory]
        [MemberData(nameof(UserPutDTOList))]
        public void UserPutDTOToPOCOMapping_List(List<UserPutDTO> input)
        {
            var result = UserMapper.Map(input.AsQueryable());


            Assert.True(result.All(result => input.Any(isItem =>
                        isItem.userName == result.UserName &&
                        isItem.userEmail == result.UserEmail &&
                        isItem.isActive == result.Active &&
                        isItem.isEntrepreneur == result.IsEnterprenuer


            )));
        }


    }
}
