﻿using System;
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

        public ServiceResult<IQueryable<PostDTO>> GetAll()
        {
            var result = _postRepository.GetAll();
            return new ServiceResult<IQueryable<PostDTO>>(PostMapper.Map(result.Result), result.Code, result.Message);
        }

        public ServiceResult<PostDTO> GetById(int id)
        {
            var result = _postRepository.GetById(id);
            return new ServiceResult<PostDTO>(PostMapper.Map(result.Result), result.Code, result.Message);
        }

        public async Task<ServiceResult<int?>> AddPostAsync(PostEditDTO newPostDTO, int userID)
        {
            Post createdPost = PostEditMapper.Map(newPostDTO);
            createdPost.UserID = userID;
            var result = await _postRepository.AddAsync(createdPost);
            return new ServiceResult<int?>(result.Result?.PostID, result.Code, result.Message);
        }

        public ServiceResult<IQueryable<PostDTO>> GetAllOfUser(int userID)
        {
            var serviceResult = _postRepository.GetAll();
            var result = serviceResult.Result?.Where(post => post.UserID == userID); // LINQ w repozytorium !!!
            return new ServiceResult<IQueryable<PostDTO>>(PostMapper.Map(result), serviceResult.Code, serviceResult.Message);  
        }

        public async Task<ServiceResult<bool>> DeletePostAsync(int id)
        {
            var GetResult = _postRepository.GetById(id);

            if (!GetResult.IsOk())
                return new ServiceResult<bool>(false, GetResult.Code, GetResult.Message);

            var RemoveResult = await _postRepository.RemoveAsync(GetResult.Result);
            return new ServiceResult<bool>(RemoveResult.IsOk(), RemoveResult.Code, RemoveResult.Message);
        }

        public async Task<ServiceResult<bool>> EditPostAsync(int id, PostEditDTO body)
        {
            Post post = PostEditMapper.Map(body);
            post.PostID = id;
            var result = await _postRepository.UpdateAsync(post);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);
        }

        public ServiceResult<IQueryable<int>> GetLikes(int postID)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> EditLikeStatusAsync(int commentID, bool like)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<IQueryable<CommentDTOOutput>> GetAllComments(int postID, int userID)
        {
            var result = _postRepository.GetAllComments(postID);
            return new ServiceResult<IQueryable<CommentDTOOutput>>(Mapper.MapOutput(result.Result), result.Code, result.Message);
        }
    }
}