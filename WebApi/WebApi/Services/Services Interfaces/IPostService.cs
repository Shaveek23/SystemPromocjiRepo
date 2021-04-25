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
        public ServiceResult<IQueryable<PostDTO>> GetAll();

        public ServiceResult<PostDTO> GetById(int id, int userID);
        public Task<ServiceResult<int?>> AddPostAsync(PostEditDTO newPostDTO, int userID);
        public ServiceResult<IQueryable<PostDTO>> GetAllOfUser(int userID);
        public Task<ServiceResult<bool>> DeletePostAsync(int id);
        public Task<ServiceResult<bool>> EditPostAsync(int id, PostEditDTO body);
        public ServiceResult<IQueryable<int>> GetLikes(int postID);
        public Task<ServiceResult<bool>> EditLikeStatusAsync(int userID, int postID, LikeDTO like);
       public ServiceResult<IQueryable<CommentDTOOutput>> GetAllComments(int postID,int  userID);
    }
}
