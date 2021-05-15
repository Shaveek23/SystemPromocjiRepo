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
        public static IEnumerable<object[]> PostDTOEditData()
        {
            yield return new object[] { new PostDTOEdit { title="Titleeee1", content="swietna oferta", category =3,  isPromoted=true } };
            yield return new object[] { new PostDTOEdit { title = "Titleeee2", content = "Hi, I am new here....",category = 1, isPromoted = false } };
            yield return new object[] { new PostDTOEdit {category=5, isPromoted =false } };
        }


        public static IEnumerable<object[]> PostDTOCreateData()
        {
            yield return new object[] { new PostDTOCreate { title = "Titleeee1", content = "swietna oferta", category = 3} };
            yield return new object[] { new PostDTOCreate { title = "Titleeee2", content = "Hi, I am new here....", category = 1} };
            yield return new object[] { new PostDTOCreate { category = 5 } };
        }

        [Theory]
        [MemberData(nameof(PostDTOEditData))]
        public void DTOToPOCOMapping_Edit(PostDTOEdit input)
        {
            Post result = PostEditMapper.Map(input);

            Assert.Equal(input.title, result.Title);
            Assert.Equal(input.content, result.Content);
            Assert.Equal(input.category.Value, result.CategoryID);
            Assert.Equal(input.isPromoted.Value, result.IsPromoted);
        }

        [Theory]
        [MemberData(nameof(PostDTOCreateData))]
        public void DTOToPOCOMapping_Create(PostDTOCreate input)
        {
            Post result = PostEditMapper.Map(input);

            Assert.Equal(input.title, result.Title);
            Assert.Equal(input.content, result.Content);
            Assert.Equal(input.category.Value, result.CategoryID);
        }

    }
}
