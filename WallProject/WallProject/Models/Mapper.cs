using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WallProject.Models.DTO;
using WallProject.Services;

namespace WallProject.Models.Mapper
{
    public class Mapper
    {
        public static PostViewModel Map(PostDTO postDTO, List<CommentViewModel> comments = null)
        {
            PostViewModel postVM = new PostViewModel
            {
                Title = postDTO.title,
                Content = postDTO.content,
                Datetime = postDTO.datetime,
                IsPromoted = postDTO.isPromoted,
                IsLikedByUser = postDTO.IsLikedByUser,
                Likes = postDTO.likesCount,
                OwnerName = postDTO.author, //TO DO: po implementacji User usunąć;
                Comments = comments
            };
            return postVM;
        }

        public static CommentViewModel Map(CommentDTO commentDTO, int? likes = null)
        {
            return new CommentViewModel
            {
                Content = commentDTO.content,
                Time = commentDTO.dateTime,
                OwnerName = commentDTO.userID.ToString(),
                Likes = likes ?? 0
            };
        }

    }
}
