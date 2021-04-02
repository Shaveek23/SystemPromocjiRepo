using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Database.Mapper.PostMappers;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;
using Xunit;

namespace WebApiTest.MapperTest.PostMappersTest
{
    public class PostEditMapperTest
    {
        public static IEnumerable<object[]> PostDTOData()
        {
            yield return new object[] { new PostEditDTO { title="Titleeee1", content="swietna oferta", category =3, dateTime= new DateTime(2000, 10, 10, 11, 4, 41), isPromoted=true } };
            yield return new object[] { new PostEditDTO { title = "Titleeee2", content = "Hi, I am new here....", dateTime = new DateTime(2020, 10, 13, 11, 4, 41), isPromoted = false } };
            yield return new object[] { new PostEditDTO() };
        }

        [Theory]
        [MemberData(nameof(PostDTOData))]
        public void DTOToPOCOMapping(PostEditDTO input)
        {
            Post result = PostEditMapper.Map(input);

            Assert.Equal(input.title, result.Title);
            Assert.Equal(input.content, result.Content);
            Assert.Equal(input.dateTime, result.Date);
            Assert.Equal(input.category, result.CategoryID);
            Assert.Equal(input.isPromoted, result.IsPromoted);
        }

    }
}
