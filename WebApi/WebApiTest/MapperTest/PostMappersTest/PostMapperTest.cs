using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.Database.Mapper;
using WebApi.Models.DTO;
using WebApi.Models.POCO;
using Xunit;

namespace WebApiTest.MapperTest.PostMappersTest
{
    public class PostMapperTest
    { 
        public static IEnumerable<object[]> PostPOCODataList()
        {
            yield return new object[]
            {
                new List<Post>
                {
                    new Post { PostID = 1, UserID = 1, CategoryID = 1, Title= "Title1", Content = "Content1 ", Date = new DateTime(2020, 1, 1), IsPromoted = false},
                    new Post { PostID = 12, UserID = 8, CategoryID = 3, Date = new DateTime(3213123), IsPromoted = true,},
                    new Post { PostID = 2, UserID = 5, CategoryID = 5, Title= "Title32321", Date = new DateTime(2000, 10, 10, 11, 4, 41), IsPromoted = false},
                    new Post()
                }

             };
        }
        public static IEnumerable<object[]> PostDTODataList()
        {
            yield return new object[]
            {
                new List<PostDTO>
                {
                    new PostDTO { title ="Title123", author = "Janek", authorID =5, category = "RTV", content="Cześć 320", datetime=new DateTime(2020, 1, 1), id=321, isLikedByUser=false, isPromoted=false, likesCount=15 },
                    new PostDTO { title ="Title123", author = "Janek", authorID =5, datetime=new DateTime(3213123), id=321, isLikedByUser=false, isPromoted=false, likesCount=15 },
                    new PostDTO { author = "Janek", authorID =5, category = "RTV", datetime=new DateTime(2000, 10, 10, 11, 4, 41), isLikedByUser=false, isPromoted=false, likesCount=15 },
                    new PostDTO ()
                }

             };

        }

        public static IEnumerable<object[]> PostDTOData()
        {
            yield return new object[] { new PostDTO { title = "Title123", author = "Janek", authorID = 5, category = "RTV", content = "Cześć 320", datetime = new DateTime(2020, 1, 1), id = 321, isLikedByUser = false, isPromoted = false, likesCount = 15 } };
            yield return new object[] { new PostDTO { title = "Title123", author = "Janek", authorID = 5, datetime = new DateTime(3213123), id = 321, isLikedByUser = false, isPromoted = false, likesCount = 15 }, };
            yield return new object[] { new PostDTO { author = "Janek", authorID = 5, category = "RTV", datetime = new DateTime(2000, 10, 10, 11, 4, 41), isLikedByUser = false, isPromoted = false, likesCount = 15 } };
            yield return new object[] { new PostDTO() };
        }

        public static IEnumerable<object[]> PostPOCOData()
        {
            yield return new object[] { new Post { PostID = 1, UserID = 1, CategoryID = 1, Title = "Title1", Content = "Content1 ", Date = new DateTime(2020, 1, 1), IsPromoted = false} };
            yield return new object[] { new Post { PostID = 12, UserID = 8, CategoryID = 3, Date = new DateTime(3213123), IsPromoted = true, } };
            yield return new object[] { new Post { PostID = 2, UserID = 5, CategoryID = 5, Title = "Title32321", Date = new DateTime(2000, 10, 10, 11, 4, 41), IsPromoted = false } };
            yield return new object[] { new Post() };
        }


        [Theory]
        [MemberData(nameof(PostPOCOData))]
        public void POCOToDTOMapping(Post input)
        {

            PostDTO result = PostMapper.Map(input);

            //THere is lack of category
            Assert.Equal(input.PostID, result.id);
            Assert.Equal(input.UserID, result.authorID);
            Assert.Equal(input.Title, result.title);
            Assert.Equal(input.Content, result.content);
            Assert.Equal(input.IsPromoted, result.isPromoted);
            Assert.Equal(input.Date, result.datetime);
        }


        [Theory]
        [MemberData(nameof(PostDTOData))]
        public void DTOToPOCOMapping(PostDTO input)
        {
            Post result = PostMapper.Map(input);

            Assert.Equal(input.id, result.PostID);
            Assert.Equal(input.authorID, result.UserID);
            Assert.Equal(input.datetime, result.Date);
            Assert.Equal(input.title, result.Title);
            Assert.Equal(input.content, result.Content);
            Assert.Equal(input.isPromoted, result.IsPromoted);
        }


        [Theory]
        [MemberData(nameof(PostPOCODataList))]
        public void POCOToDTOMapping_List(List<Post> input)
        {
            var result = PostMapper.Map(input.AsQueryable());

            Assert.True(result.All(result => input.Any(isItem =>
            isItem.PostID == result.id &&
            isItem.UserID == result.authorID &&
            isItem.Date == result.datetime &&
            isItem.Title == result.title &&
            isItem.Content == result.content &&
            isItem.IsPromoted == result.isPromoted)));
        }


        [Theory]
        [MemberData(nameof(PostDTODataList))]
        public void DTOToPOCOMapping_List(List<PostDTO> input)
        {
            var result = PostMapper.Map(input.AsQueryable());

            Assert.True(result.All(result => input.Any(isItem =>
            isItem.id == result.PostID &&
            isItem.authorID == result.UserID &&
            isItem.datetime == result.Date &&
            isItem.title == result.Title &&
            isItem.content == result.Content &&
            isItem.isPromoted == result.IsPromoted)));
        }
    }
}
