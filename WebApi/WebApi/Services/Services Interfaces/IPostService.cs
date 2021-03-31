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
        IQueryable<PostDTO> GetAllOfUser(int userID);
        void DeletePost(int id);
        Task<Post> EditPost(int id, PostEditDTO body);
        PostLikesDTO GetLikes(int postID);
        void EditLikeStatus(int commentID, bool like);
    }
}
