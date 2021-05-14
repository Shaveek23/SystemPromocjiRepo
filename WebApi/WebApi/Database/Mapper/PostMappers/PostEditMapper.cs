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
        public static Post Map(PostDTOEdit postDTOEdit)
        {
            Post post = new Post();
            
            if (postDTOEdit.category.HasValue) post.CategoryID = postDTOEdit.category.Value;
            if (postDTOEdit.isPromoted.HasValue) post.IsPromoted = postDTOEdit.isPromoted.Value;
            post.Title = postDTOEdit.title;
            post.Content = postDTOEdit.content;

            return post;
        }

        public static Post Map(PostDTOCreate postDTOCreate)
        {
            Post post = new Post();

            if (postDTOCreate.category.HasValue) post.CategoryID = postDTOCreate.category.Value;
            post.Title = postDTOCreate.title;
            post.Content = postDTOCreate.content;

            return post;
        }
    }
}
