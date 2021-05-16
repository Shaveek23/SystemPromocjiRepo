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
        public static Post Map(PostPutDTO postDTOEdit)
        {
            Post post = new Post();
            
            if (postDTOEdit.categoryID.HasValue) post.CategoryID = postDTOEdit.categoryID.Value;
            if (postDTOEdit.isPromoted.HasValue) post.IsPromoted = postDTOEdit.isPromoted.Value;
            post.Title = postDTOEdit.title;
            post.Content = postDTOEdit.content;

            return post;
        }

        public static Post Map(PostPostDTO postDTOCreate)
        {
            Post post = new Post();

            if (postDTOCreate.categoryID.HasValue) post.CategoryID = postDTOCreate.categoryID.Value;
            post.Title = postDTOCreate.title;
            post.Content = postDTOCreate.content;

            return post;
        }
    }
}
