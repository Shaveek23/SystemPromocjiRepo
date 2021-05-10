using System;
using Xunit;
using Moq;
using System.Net.Http;
using System.Net;
using WallProject.Models.DTO;
using System.Threading.Tasks;
using Moq.Protected;
using System.Threading;
using WallProject.Services.Serives_Implementations;
using Microsoft.AspNetCore.Mvc;
using AutoFixture;
using WallProject.Services.Services_Interfaces;
using WallProject.Services;
using WallProject.Models;
using System.Collections.Generic;
using WallProject.Models.MainView;
using Newtonsoft.Json;
using WallProject.Models.Mapper;
using System.Linq;

namespace WallProjectTest.ModelsTests
{
    public class MapperTest
    {
        
        DateTime date = new DateTime(2012, 12, 12, 12, 12, 12);
        [Theory]
        [InlineData(0,"tytul","content", true, true, 10, "gawezi")]
        [InlineData(0,"avon","siema chcesz cos z avonu",true, false, 23, "znajoma z gimnazjum")]
        [InlineData(0,"no fajnie","contencik",  false, true, 10, "test")]
        public void PostDTOtoViewModel_Test(int id, string title, string content,bool isPromoted, bool isLiked, int likeCount, string authorName )
        {
            PostDTO postDTO = new PostDTO { id = id, author = authorName, datetime = date, content = content, isLikedByUser = isLiked, isPromoted = isPromoted, title = title, likesCount = likeCount};
            PostViewModel postViewModel = Mapper.Map(postDTO);
            Assert.True(postDTO.isLikedByUser == postViewModel.IsLikedByUser &&
                postViewModel.IsPromoted == postDTO.isPromoted &&
                postViewModel.IsLikedByUser == postDTO.isLikedByUser &&
                postViewModel.Likes == postDTO.likesCount &&
                postViewModel.OwnerName == postDTO.author &&
                postViewModel.Id == postDTO.id &&
                postViewModel.Content == postDTO.content);
        }

        //Jak bedzie potrzebny mapper w druga strone to tu jest test do niego
        //[Theory]
        //[InlineData(0, "tytul", "content", true, true, 10, "gawezi")]
        //[InlineData(0, "avon", "siema chcesz cos z avonu", true, false, 23, "znajoma z gimnazjum")]
        //[InlineData(0, "no fajnie", "contencik xD", false, true, 10, "test")]
        //public void PostViewModeltoDTO_Test(int id, string title, string content, bool isPromoted, bool isLiked, int likeCount, string authorName)
        //{
        //    PostViewModel postViewModel = new PostViewModel { Id = id, OwnerName = authorName, Datetime = date, Content = content, IsLikedByUser = isLiked, IsPromoted = isPromoted, Title = title, Likes = likeCount };
        //    PostDTO postDTO = Mapper.Map(postViewModel);
        //    Assert.True(postDTO.isLikedByUser == postViewModel.IsLikedByUser &&
        //        postViewModel.IsPromoted == postDTO.isPromoted &&
        //        postViewModel.IsLikedByUser == postDTO.isLikedByUser &&
        //        postViewModel.Likes == postDTO.likesCount &&
        //        postViewModel.OwnerName == postDTO.author &&
        //        postViewModel.Id == postDTO.id &&
        //        postViewModel.Content == postDTO.content);
        //}


        [Theory]
        [InlineData(0,  "content", true,  10, "gawezi")]
        [InlineData(0,  "siema chcesz cos z avonu", true, 23, "znajoma z gimnazjum")]
        [InlineData(0,  "contencik", false, 10, "test")]
        public void CommentDTOtoViewModel_Test(int id, string content, bool isLiked, int likeCount, string authorName)
        {
            CommentDTO postDTO = new CommentDTO { id = id, authorName = authorName, date = date, content = content, isLikedByUser = isLiked,  likesCount = likeCount };
            CommentViewModel postViewModel = Mapper.Map(postDTO);
            Assert.True(postDTO.isLikedByUser == postViewModel.IsLikedByUser &&
                postViewModel.IsLikedByUser == postDTO.isLikedByUser &&
                postViewModel.Likes == postDTO.likesCount &&
                postViewModel.OwnerName == postDTO.authorName &&
                postViewModel.Id == postDTO.id &&
                postViewModel.Content == postDTO.content);
        }

        [Theory]
        [InlineData(0, true, true, true, true, "konrad.gaw3@gmail.com", "gawezi")]
        [InlineData(0, true, true, true, true, "cokolwiek@test.pl", "cos")]
        [InlineData(0, true, true, true, true, "testy@wp.pl", "test")]
        public void UserDTOtoViewModel_Test(int id, bool active, bool admin, bool enterpreneur, bool verified, string email, string name)
        {
            UserDTO userDTO = new UserDTO { id = id, isActive = active, isAdmin = admin, isEnterprenuer = enterpreneur, isVerified = verified, timestamp = date, userEmail = email, userName = name };
            UserViewModel user = Mapper.Map(userDTO);
            Assert.True(user.IsActive == userDTO.isActive &&
                user.IsAdmin == userDTO.isAdmin &&
                user.IsEnterprenuer == userDTO.isEnterprenuer &&
                user.IsVerified == userDTO.isVerified &&
                user.Timestamp == userDTO.timestamp &&
                user.UserEmail == userDTO.userEmail &&
                user.UserName == userDTO.userName);
        }

        [Theory]
        [InlineData(0, "cokolwiek")]
        [InlineData(3, "kategoria")]
        [InlineData(int.MaxValue, "kategoria o maksymalnym category id")]
        public void CategoryDTOToViewModel_Test(int id, string name)
        {
            CategoryDTO categoryDTO = new CategoryDTO { id = id, name = name };
            CategoryViewModel category = Mapper.Map(categoryDTO);
            Assert.True(category.CategoryID == categoryDTO.id && category.CategoryName == categoryDTO.name);
        }

        public static IEnumerable<object[]> UserDTODataList()
        {
            yield return new object[]
            {
                new List<UserDTO>
                {
                    new UserDTO() { id=0, isActive=true, isAdmin=true, isEnterprenuer=true, isVerified=true, timestamp=new DateTime(2012,12,12,12,12,12), userEmail="cokowliek@cokolwiek.pl", userName="test"},
                    new UserDTO() { id=1, isActive=true, isAdmin=true, isEnterprenuer=true, isVerified=true, timestamp=new DateTime(2012,12,12,12,12,12), userEmail="cokowlqewrewrwiek.pl", userName="test"},
                    new UserDTO() { id=3, isActive=true, isAdmin=true, isEnterprenuer=true, isVerified=true, timestamp=new DateTime(2012,12,12,12,12,12), userEmail="hehe", userName="lol"},
                }

             };
        }

        public static IEnumerable<object[]> CategoryDTODataList()
        {
            yield return new object[]
            {
                new List<CategoryDTO>
                {
                    new CategoryDTO{id=0, name="Poziomki"},
                    new CategoryDTO{id=1, name="po"},
                    new CategoryDTO{id=2, name="ziomki"},
                }

             };
        }

        [Theory]
        [MemberData(nameof(UserDTODataList))]
        public void UserDTOListToViewModel(List<UserDTO> userDTOs)
        {
            List<UserViewModel> users = Mapper.Map(userDTOs);
            
            Assert.True(users.AsQueryable().All(result => userDTOs.AsQueryable().Any(isItem =>
                result.IsActive== isItem.isActive &&
                result.IsEnterprenuer == isItem.isEnterprenuer &&
                result.IsVerified == isItem.isVerified &&
                result.UserName == isItem.userName &&
                result.UserID == isItem.id

                )));
        }

        [Theory]
        [MemberData(nameof(CategoryDTODataList))]
        public void CategoryDTOListToViewModel(List<CategoryDTO> categoryDTOs)
        {
            CategoriesDTO categoriesDTO = new CategoriesDTO { categories = categoryDTOs };
            List<CategoryViewModel> categories= Mapper.Map(categoriesDTO);
            Assert.True(categories.AsQueryable().All(result => categoryDTOs.AsQueryable().Any(isItem =>
            result.CategoryID == isItem.id &&
            result.CategoryName == isItem.name
            )));
        }

    }
}
