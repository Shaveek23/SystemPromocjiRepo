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

            post.PostID = postDTO.id;
            post.UserID = postDTO.authorID;
            post.Date = postDTO.datetime;
            post.Title = postDTO.title;
            post.Content = postDTO.content;
            post.IsPromoted = postDTO.isPromoted;

            //TODO:
            //post.CategoryID = search category name in database to find its ID?
            //post.Localization = "Miasto";
            //post.ShopName = "Sklep";

            return post;
        }

        public static PostDTO Map(Post post)
        {
            PostDTO postDTO = new PostDTO();

            postDTO.id = post.PostID;
            postDTO.authorID = post.UserID;
            postDTO.datetime = post.Date;
            postDTO.title = post.Title;
            postDTO.content = post.Content;
            postDTO.isPromoted = post.IsPromoted;

            //TODO:
            //These fields should be found in database:
            //postDTO.author = "Jan";
            //postDTO.category = "kategoria pod indeksem 1";
            //postDTO.likesCount = 5;
            //postDTO.isLikedByUser = false;
            //postDTO.comments = ....

            return postDTO;
        }

        public static IQueryable<PostDTO> Map(IQueryable<Post> posts)
        {
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
