using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;
using WebApi.Models.POCO;

namespace WebApi.Database.Mapper
{
    public class PostMapper
    {
        public static Post Map(PostDTO postDTO)
        {
            Post post = new Post();

            if (postDTO.id.HasValue) post.PostID = postDTO.id.Value;
            if (postDTO.authorID.HasValue) post.UserID = postDTO.authorID.Value;
            if (postDTO.category.HasValue) post.CategoryID = postDTO.category.Value;
            if (postDTO.datetime.HasValue) post.Date = postDTO.datetime.Value;
            if (postDTO.isPromoted.HasValue) post.IsPromoted = postDTO.isPromoted.Value;


            post.Title = postDTO.title;
            post.Content = postDTO.content;

            return post;
        }

        public static PostDTO Map(Post post)
        {
            if (post == null)
                return null;
            PostDTO postDTO = new PostDTO();

            postDTO.id = post.PostID;
            postDTO.authorID = post.UserID;
            postDTO.datetime = post.Date;
            postDTO.title = post.Title;
            postDTO.content = post.Content;
            postDTO.isPromoted = post.IsPromoted;
            postDTO.category = post.CategoryID;

            //TODO:
            //These fields should be found in database:
            postDTO.author = "Jan";
            postDTO.likesCount = 5;
            postDTO.isLikedByUser = false;
            //postDTO.comments = ....

            return postDTO;
        }

        public static IQueryable<PostDTO> Map(IQueryable<Post> posts)
        {
            if (posts == null)
                return null;

            List<PostDTO> list = new List<PostDTO>();
            foreach (var post in posts)
            {
                list.Add(Map(post));
            }

            return list.AsQueryable();
        }


        public static IQueryable<Post> Map(IQueryable<PostDTO> posts)
        {
            List<Post> list = new List<Post>();
            foreach (var post in posts)
            {
                list.Add(Map(post));
            }

            return list.AsQueryable();
        }
    }
}
