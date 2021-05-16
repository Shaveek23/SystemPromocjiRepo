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
        public ServiceResult<IQueryable<PostGetDTO>> GetAll(int userID);

        public ServiceResult<PostGetDTO> GetById(int id, int userID);
        public Task<ServiceResult<idDTO>> AddPostAsync(int userID, PostPostDTO newPostDTO);
        public ServiceResult<IQueryable<PostGetDTO>> GetAllOfUser(int userID);
        public Task<ServiceResult<bool>> DeletePostAsync(int id);
        public Task<ServiceResult<bool>> EditPostAsync(int id, PostPutDTO body);
        public ServiceResult<IQueryable<LikerDTO>> GetLikes(int postID);
        public Task<ServiceResult<bool>> EditLikeStatusAsync(int userID, int postID, LikeDTO like);
       public ServiceResult<IQueryable<CommentDTOOutput>> GetAllComments(int postID,int  userID);
    }
}
