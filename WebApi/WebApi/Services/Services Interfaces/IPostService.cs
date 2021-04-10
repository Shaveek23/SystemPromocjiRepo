using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;

namespace WebApi.Services.Services_Interfaces
{
    public interface IPostService
    {
        public IQueryable<PostDTO> GetAll();

        public PostDTO GetById(int id);
        public Task<int> AddPostAsync(PostEditDTO newPostDTO, int userID);
        public IQueryable<PostDTO> GetAllOfUser(int userID);
        public Task DeletePostAsync(int id);
        public Task<Post> EditPostAsync(int id, PostEditDTO body);
        public IQueryable<int> GetLikes(int postID);
        public Task EditLikeStatusAsync(int commentID, bool like);
       public IQueryable<CommentDTOOutput> GetAllComments(int postID,int  userID);
    }
}
