using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WallProject.Models.DTO;
using WallProject.Models.MainView;
using WallProject.Services;

namespace WallProject.Models.Mapper
{
    public class Mapper
    {
      
        public static PostViewModel Map(PostDTO postDTO, List<CommentViewModel> comments = null)
        {
            PostViewModel postVM = new PostViewModel
            {
                
                Id = postDTO.id,
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

        public static CommentViewModel Map(CommentDTO commentDTO)
        {
            return new CommentViewModel
            {
                IsLikedByUser = commentDTO.isLikedByUser,
                Content = commentDTO.content,
                Time = commentDTO.date,
                OwnerName = commentDTO.authorName.ToString(),
                Likes = commentDTO.likesCount,
                OwnerMode = commentDTO.ownerMode
            };
        }


        public static UserViewModel Map(UserDTO userDTO)
        {
            return new UserViewModel
            {
                UserID = userDTO.userId,
                IsActive= userDTO.isActive,
                IsAdmin= userDTO.isAdmin,
                IsEnterprenuer= userDTO.isEnterprenuer,
                IsVerified=userDTO.isVerified,
                Timestamp=userDTO.timestamp,
                UserEmail=userDTO.userEmail,
                UserName=userDTO.userName
            };
        }

        public static List<UserViewModel> Map(IEnumerable<UserDTO> categoryDTOs)
        {
            List<UserViewModel> VMlist = new List<UserViewModel>();
            foreach (var item in categoryDTOs)
            {
                VMlist.Add(Map(item));
            }
            return VMlist;
        }


        public static CategoryViewModel Map(CategoryDTO categoryDTO)
        {
            return new CategoryViewModel
            {
                CategoryID = categoryDTO.id,
                CategoryName = categoryDTO.name
            };
        }


        public static List<CategoryViewModel> Map(IEnumerable<CategoryDTO> categoryDTOs)
        {
            List<CategoryViewModel> VMlist = new List<CategoryViewModel>();
            foreach(var item in categoryDTOs)
            {
                VMlist.Add(Map(item));
            }
            return VMlist;
        }




        //public static UserViewModel Map(UserDTO userDTO, int? likes = null)
        //{
        //    return new CommentViewModel
        //    {
        //        Content = commentDTO.content,
        //        Time = commentDTO.dateTime,
        //        OwnerName = commentDTO.userID.ToString(),
        //        Likes = likes ?? 0
        //    };
        //}

    }
}
