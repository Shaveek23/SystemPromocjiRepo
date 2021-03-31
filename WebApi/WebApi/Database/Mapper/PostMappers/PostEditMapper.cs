using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;

namespace WebApi.Database.Mapper.PostMappers
{
    public class PostEditMapper
    {
        public static Post Map(PostEditDTO postEditDTO)
        {
            Post post = new Post();

            post.CategoryID = postEditDTO.category;
            post.Content = postEditDTO.content;
            post.Date = postEditDTO.dateTime;
            post.Title = postEditDTO.title;
            post.IsPromoted = postEditDTO.isPromoted;

            //TODO:
            //post.CategoryID = search category name in database to find its ID?
            //post.Localization = "Miasto";
            //post.ShopName = "Sklep";

            return post;
        }
    }
}
