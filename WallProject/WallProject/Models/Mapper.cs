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
      
        public static PostViewModel Map(PostGetDTO postDTO)
        {
            PostViewModel postVM = new PostViewModel
            {
                
                Id = postDTO.ID,
                Title = postDTO.Title,
                Content = postDTO.Content,
                Category = postDTO.Category,
                Datetime = postDTO.Datetime,
                IsPromoted = postDTO.IsPromoted,
                IsLikedByUser = postDTO.IsLikedByUser,
                Likes = postDTO.LikesCount,
                OwnerName = postDTO.AuthorName, 
            };
            return postVM;
        }

        public static List<CommentViewModel> Map(List<CommentGetDTO> commentDTOs)
        {
            if (commentDTOs == null) return new List<CommentViewModel>();
            List<CommentViewModel> VMlist = new List<CommentViewModel>();
            foreach (var item in commentDTOs)
            {
                VMlist.Add(Map(item));
            }
            return VMlist;
        }

        public static CommentViewModel Map(CommentGetDTO commentDTO)
        {
            return new CommentViewModel
            {
                Id= commentDTO.ID,
                IsLikedByUser = commentDTO.IsLikedByUser,
                Content = commentDTO.Content,
                Time = commentDTO.Date,
                OwnerName = commentDTO.AuthorName.ToString(),
                Likes = commentDTO.LikesCount,
                OwnerMode = commentDTO.OwnerMode,
                PostID = commentDTO.PostID
            };
        }


        public static UserViewModel Map(UserGetDTO userDTO)
        {
            return new UserViewModel
            {
                UserID = userDTO.ID,
                IsActive= userDTO.IsActive,
                IsAdmin= userDTO.IsAdmin,
                IsEnterprenuer= userDTO.IsEnterprenuer,
                IsVerified=userDTO.IsVerified,
                UserEmail=userDTO.UserEmail,
                UserName=userDTO.UserName
            };
        }

        public static List<UserViewModel> Map(IEnumerable<UserGetDTO> userDTOs)
        {
            List<UserViewModel> VMlist = new List<UserViewModel>();
            foreach (var item in userDTOs)
            {
                VMlist.Add(Map(item));
            }
            return VMlist;
        }


        public static CategoryViewModel Map(CategoryDTO categoryDTO)
        {
            return new CategoryViewModel
            {
                CategoryID = categoryDTO.ID,
                CategoryName = categoryDTO.Name
            };
        }

        public static List<CategoryViewModel> Map(List<CategoryDTO> categoryDTOs)
        {
            List<CategoryViewModel> VMlist = new List<CategoryViewModel>();
            foreach (var item in categoryDTOs)
            {
                VMlist.Add(Map(item));
            }
            return VMlist;
        }
    }
}
