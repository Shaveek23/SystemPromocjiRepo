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
      
        public static PostViewModel Map(PostDTO postDTO)
        {
            PostViewModel postVM = new PostViewModel
            {
                
                Id = postDTO.id,
                Title = postDTO.title,
                Content = postDTO.content,
                Category = postDTO.category,
                Datetime = postDTO.datetime,
                IsPromoted = postDTO.isPromoted,
                IsLikedByUser = postDTO.isLikedByUser,
                Likes = postDTO.likesCount,
                OwnerName = postDTO.author, //TO DO: po implementacji User usunąć;
                Comments = Map(postDTO.comments)
            };
            return postVM;
        }

        public static List<CommentViewModel> Map(List<CommentDTO> commentDTOs)
        {
            if (commentDTOs == null) return new List<CommentViewModel>();
            List<CommentViewModel> VMlist = new List<CommentViewModel>();
            foreach (var item in commentDTOs)
            {
                VMlist.Add(Map(item));
            }
            return VMlist;
        }

        public static CommentViewModel Map(CommentDTO commentDTO)
        {
            return new CommentViewModel
            {
                Id= commentDTO.id,
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
                UserID = userDTO.id,
                IsActive= userDTO.isActive,
                IsAdmin= userDTO.isAdmin,
                IsEnterprenuer= userDTO.isEnterprenuer,
                IsVerified=userDTO.isVerified,
                Timestamp=userDTO.timestamp,
                UserEmail=userDTO.userEmail,
                UserName=userDTO.userName
            };
        }

        public static List<UserViewModel> Map(IEnumerable<UserDTO> userDTOs)
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
                CategoryID = categoryDTO.categoryId,
                CategoryName = categoryDTO.name
            };
        }


        public static List<CategoryViewModel> Map(CategoriesDTO categoriesDTO)
        {
            List<CategoryViewModel> VMlist = new List<CategoryViewModel>();
            foreach(var item in categoriesDTO.categories)
            {
                VMlist.Add(Map(item));
            }
            return VMlist;
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
