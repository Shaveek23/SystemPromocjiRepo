using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Mapper;
using WebApi.Database.Mapper.PostMappers;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
using WebApi.Models.DTO.PostDTOs;
using WebApi.Models.POCO;
using WebApi.Services.Services_Interfaces;

namespace WebApi.Services.Serives_Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public IQueryable<PostDTO> GetAll()
        {
            return PostMapper.Map(_postRepository.GetAll());
        }

        public PostDTO GetById(int id)
        {
            return PostMapper.Map(_postRepository.GetById(id));
        }

        public async Task<int> AddPostAsync(PostEditDTO newPostDTO, int userID)
        {
            Post createdPost = PostEditMapper.Map(newPostDTO);
            createdPost.UserID = userID;
            createdPost = await _postRepository.AddAsync(createdPost);
            return createdPost.PostID;
        }

        public IQueryable<PostDTO> GetAllOfUser(int userID)
        {
            return PostMapper.Map(_postRepository.GetAll().Where(post => post.UserID == userID)); // takie rzeczy w repozytorium
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.RemoveAsync(_postRepository.GetById(id));
        }

        public async Task<Post> EditPostAsync(int id, PostEditDTO body)
        {
            Post post = PostEditMapper.Map(body);
            return await _postRepository.UpdateAsync(post);
        }

        public IQueryable<int> GetLikes(int postID)
        {
            throw new NotImplementedException();
        }

        public async Task EditLikeStatusAsync(int commentID, bool like)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CommentDTOOutput> GetAllComments(int postID, int userID)
        {
            var result = _postRepository.GetAllComments(postID);
            return Mapper.MapOutput(result);
        }
    }
}
