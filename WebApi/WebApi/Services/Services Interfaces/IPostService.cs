using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;

namespace WebApi.Services.Services_Interfaces
{
    public interface IPostService
    {
        public IQueryable<PostDTO> GetAll();

        public PostDTO GetById(int id);
        public Task<PostDTO> AddPersonAsync(PostDTO newPostDTO);
        IQueryable<PostDTO> GetAllOfUser(int userID);
        void DeletePost(int id);
        void EditPost(int id, PostEditDTO body);
        int CreatePost(PostEditDTO body);
        PostLikesDTO GetLikes(int postID);
        void EditLikeStatus(int commentID, bool like);
    }
}
