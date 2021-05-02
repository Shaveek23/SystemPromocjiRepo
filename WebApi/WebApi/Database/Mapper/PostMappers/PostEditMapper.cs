using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;
using WebApi.Models.POCO;

namespace WebApi.Database.Mapper.PostMappers
{
    public class PostEditMapper
    {
        public static Post Map(PostEditDTO postEditDTO)
        {
            Post post = new Post();

            if (postEditDTO.category.HasValue) post.CategoryID = postEditDTO.category.Value;
            if (postEditDTO.dateTime.HasValue) post.Date = postEditDTO.dateTime.Value;
            if (postEditDTO.isPromoted.HasValue) post.IsPromoted = postEditDTO.isPromoted.Value;

            post.Title = postEditDTO.title;
            post.Content = postEditDTO.content;



            //TODO:
            //post.CategoryID = search category name in database to find its ID?
            //post.Localization = "Miasto";
            //post.ShopName = "Sklep";

            return post;
        }
    }
}
