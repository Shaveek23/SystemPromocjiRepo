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

        [InlineData(0, "tytul", "content", true, true, 10, "gawezi")]
        [InlineData(0, "avon", "siema chcesz cos z avonu", true, false, 23, "znajoma z gimnazjum")]
        [InlineData(0, "no fajnie", "contencik xD", false, true, 10, "test")]
        public void PostDTOtoViewModel_Test(int id, string title, string content, bool isPromoted, bool isLiked, int likeCount, string authorName)

        {
            PostGetDTO postDTO = new PostGetDTO { ID = id, AuthorName = authorName, Datetime = date, Content = content, IsLikedByUser = isLiked, IsPromoted = isPromoted, Title = title, LikesCount = likeCount};
            PostViewModel postViewModel = Mapper.Map(postDTO);
            Assert.True(postDTO.IsLikedByUser == postViewModel.IsLikedByUser &&
                postViewModel.IsPromoted == postDTO.IsPromoted &&
                postViewModel.IsLikedByUser == postDTO.IsLikedByUser &&
                postViewModel.Likes == postDTO.LikesCount &&
                postViewModel.OwnerName == postDTO.AuthorName &&
                postViewModel.Id == postDTO.ID &&
                postViewModel.Content == postDTO.Content);
        }

        //Jak bedzie potrzebny mapper w druga strone to tu jest test do niego
        //[Theory]
        //[InlineData(0, "tytul", "Content", true, true, 10, "gawezi")]
        //[InlineData(0, "avon", "siema chcesz cos z avonu", true, false, 23, "znajoma z gimnazjum")]
        //[InlineData(0, "no fajnie", "contencik xD", false, true, 10, "test")]
        //public void PostViewModeltoDTO_Test(int id, string title, string Content, bool isPromoted, bool isLiked, int likeCount, string authorName)
        //{
        //    PostViewModel postViewModel = new PostViewModel { Id = id, OwnerName = authorName, Datetime = date, Content = Content, IsLikedByUser = isLiked, IsPromoted = isPromoted, Title = title, Likes = likeCount };
        //    PostDTO postDTO = Mapper.Map(postViewModel);
        //    Assert.True(postDTO.isLikedByUser == postViewModel.IsLikedByUser &&
        //        postViewModel.IsPromoted == postDTO.isPromoted &&
        //        postViewModel.IsLikedByUser == postDTO.isLikedByUser &&
        //        postViewModel.Likes == postDTO.likesCount &&
        //        postViewModel.OwnerName == postDTO.author &&
        //        postViewModel.Id == postDTO.id &&
        //        postViewModel.Content == postDTO.Content);
        //}


        [Theory]

        [InlineData(0, "content", true, 10, "gawezi")]
        [InlineData(0, "siema chcesz cos z avonu", true, 23, "znajoma z gimnazjum")]
        [InlineData(0, "contencik xD", false, 10, "test")]

        public void CommentDTOtoViewModel_Test(int id, string content, bool isLiked, int likeCount, string authorName)
        {
            CommentGetDTO postDTO = new CommentGetDTO { ID = id, AuthorName = authorName, Date = date, Content = content, IsLikedByUser = isLiked, LikesCount = likeCount };
            CommentViewModel postViewModel = Mapper.Map(postDTO);
            Assert.True(postDTO.IsLikedByUser == postViewModel.IsLikedByUser &&
                postViewModel.IsLikedByUser == postDTO.IsLikedByUser &&
                postViewModel.Likes == postDTO.LikesCount &&
                postViewModel.OwnerName == postDTO.AuthorName &&
                postViewModel.Id == postDTO.ID &&
                postViewModel.Content == postDTO.Content);
        }

        [Theory]
        [InlineData(0, true, true, true, true, "konrad.gaw3@gmail.com", "gawezi")]
        [InlineData(0, true, true, true, true, "cokolwiek@test.pl", "cos")]
        [InlineData(0, true, true, true, true, "testy@wp.pl", "test")]
        public void UserDTOtoViewModel_Test(int id, bool active, bool admin, bool enterpreneur, bool verified, string email, string name)
        {
            UserGetDTO userDTO = new UserGetDTO { ID = id, IsActive = active, IsAdmin = admin, IsEnterprenuer = enterpreneur, IsVerified = verified, UserEmail = email, UserName = name };
            UserViewModel user = Mapper.Map(userDTO);
            Assert.True(user.IsActive == userDTO.IsActive &&
                user.IsAdmin == userDTO.IsAdmin &&
                user.IsEnterprenuer == userDTO.IsEnterprenuer &&
                user.IsVerified == userDTO.IsVerified &&
                user.UserEmail == userDTO.UserEmail &&
                user.UserName == userDTO.UserName);
        }

        [Theory]
        [InlineData(0, "cokolwiek")]
        [InlineData(3, "kategoria")]
        [InlineData(int.MaxValue, "kategoria o maksymalnym CategoryID id")]
        public void CategoryDTOToViewModel_Test(int id, string name)
        {
            CategoryDTO categoryDTO = new CategoryDTO { ID = id, Name = name };
            CategoryViewModel category = Mapper.Map(categoryDTO);
            Assert.True(category.CategoryID == categoryDTO.ID && category.CategoryName == categoryDTO.Name);
        }

        public static IEnumerable<object[]> UserDTODataList()
        {
            yield return new object[]
            {
                new List<UserGetDTO>
                {
                    new UserGetDTO() { ID=0, IsActive=true, IsAdmin=true, IsEnterprenuer=true, IsVerified=true, UserEmail="cokowliek@cokolwiek.pl", UserName="test"},
                    new UserGetDTO() { ID=1, IsActive=true, IsAdmin=true, IsEnterprenuer=true, IsVerified=true, UserEmail="cokowlqewrewrwiek.pl", UserName="test"},
                    new UserGetDTO() { ID=3, IsActive=true, IsAdmin=true, IsEnterprenuer=true, IsVerified=true, UserEmail="hehe", UserName="lol"},
                }

             };
        }

        public static IEnumerable<object[]> CategoryDTODataList()
        {
            yield return new object[]
            {
                new List<CategoryDTO>
                {
                    new CategoryDTO{ID=0, Name="Poziomki"},
                    new CategoryDTO{ID=1, Name="po"},
                    new CategoryDTO{ID=2, Name="ziomki"},
                }

             };
        }

        [Theory]
        [MemberData(nameof(UserDTODataList))]
        public void UserDTOListToViewModel(List<UserGetDTO> userDTOs)
        {
            List<UserViewModel> users = Mapper.Map(userDTOs);

            Assert.True(users.AsQueryable().All(result => userDTOs.AsQueryable().Any(isItem =>
                result.IsActive == isItem.IsActive &&
                result.IsEnterprenuer == isItem.IsEnterprenuer &&
                result.IsVerified == isItem.IsVerified &&
                result.UserName == isItem.UserName &&
                result.UserID == isItem.ID

                )));
        }

        [Theory]
        [MemberData(nameof(CategoryDTODataList))]
        public void CategoryDTOListToViewModel(List<CategoryDTO> categoryDTOs)
        {
            var mappedCategories = Mapper.Map(categoryDTOs);
            Assert.True(categoryDTOs.AsQueryable().All(result => mappedCategories.AsQueryable().Any(isItem =>
            result.ID == isItem.CategoryID &&
            result.Name == isItem.CategoryName
            )));
        }

    }
}
