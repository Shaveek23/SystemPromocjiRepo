using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database.Mapper;
using WebApi.Database.Repositories.Interfaces;
using WebApi.Models.DTO;
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

        public async Task<PostDTO> AddPersonAsync(PostDTO newPostDTO)
        {
            Post newPost = PostMapper.Map(newPostDTO);
            Post createdPost = await _postRepository.AddAsync(newPost);
            return PostMapper.Map(createdPost);
        }

    }
}
