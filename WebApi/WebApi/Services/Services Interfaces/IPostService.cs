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
        public ServiceResult<IQueryable<PostDTOOutput>> GetAll(int userID);

        public ServiceResult<PostDTOOutput> GetById(int id, int userID);
        public Task<ServiceResult<int?>> AddPostAsync(PostDTOCreate newPostDTO, int userID);
        public ServiceResult<IQueryable<PostDTOOutput>> GetAllOfUser(int userID);
        public Task<ServiceResult<bool>> DeletePostAsync(int id);
        public Task<ServiceResult<bool>> EditPostAsync(int id, PostDTOEdit body);
        public ServiceResult<IQueryable<LikerDTO>> GetLikes(int postID);
        public Task<ServiceResult<bool>> EditLikeStatusAsync(int userID, int postID, LikeDTO like);
       public ServiceResult<IQueryable<CommentDTOOutput>> GetAllComments(int postID,int  userID);
    }
}
