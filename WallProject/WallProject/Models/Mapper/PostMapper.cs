using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallProject.Models.DTO;

namespace WallProject.Models.Mapper
{
    public class PostMapper
    {
        public static PostViewModel Map(PostDTO postDTO)
        {
            PostViewModel postVM = new PostViewModel
            {
                Title = postDTO.title,
                Content = postDTO.content,
                Datetime = postDTO.datetime,
                IsPromoted = postDTO.isPromoted,
                IsLikedByUser = postDTO.IsLikedByUser,
                Likes = postDTO.likesCount,
                OwnerName = postDTO.author //TO DO: po implementacji User usunąć;
            };

            return postVM;
        }
    }
}
