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
        private readonly IUserRepository _userRepository;

        private readonly ICommentService _commentService;


        public PostService(IPostRepository postRepository, IUserRepository userRepository, ICommentService commentService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentService = commentService;
        }

        public ServiceResult<IQueryable<PostDTO>> GetAll(int userID)
        {
            var result = _postRepository.GetAll();
            if (result.Result == null)
            {
                return new ServiceResult<IQueryable<PostDTO>>(null, result.Code, result.Message);
            }
            var users = _userRepository.GetAll();
            var allComments = _commentService.GetAll(userID);

            List<PostDTO> postDTOs = new List<PostDTO>();
            foreach (var post in result.Result.ToList())
            {
                var postLikes = GetLikes(post.PostID);
                var user = users.Result?.Where(x => x.UserID == post.UserID).FirstOrDefault();
                var comments = allComments.Result?.Where(x => x.postId == post.PostID);

                var postDTO = PostMapper.Map(post);

                postDTO.author = user?.UserName ?? "Nie ma takiego użytkownika";
                postDTO.authorID = user?.UserID ?? 0;

                postDTO.likesCount = postLikes.Result?.Count() ?? 0;
                postDTO.isLikedByUser = postLikes.Result?.Any(x => x.id == userID) ?? false;
                postDTO.comments = comments;
               

                postDTOs.Add(postDTO);
            }

            return new ServiceResult<IQueryable<PostDTO>>(postDTOs.AsQueryable(), result.Code, result.Message);
        }

        public ServiceResult<PostDTO> GetById(int postID, int userID)
        {
            var result = _postRepository.GetById(postID);
            if (result.Result == null)
            {
                return new ServiceResult<PostDTO>(null, result.Code, result.Message);
            }

            var postLikes = GetLikes(postID);

            var user = _userRepository.GetById(userID);

            var comments = _commentService.GetAll(userID).Result?.Where(x => x.postId == postID);

            var postDTO = PostMapper.Map(result.Result);

            postDTO.author = user.Result?.UserName ?? "Nie ma takiego użytkownika";
            postDTO.authorID = user.Result?.UserID ?? 0;

            postDTO.likesCount = postLikes.Result?.Count() ?? 0;
            postDTO.isLikedByUser = postLikes.Result?.Any(x => x.id == userID) ?? false;

            postDTO.comments = comments;

            return new ServiceResult<PostDTO>(postDTO, result.Code, result.Message);
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
            if (serviceResult.Result == null)
            {
                return new ServiceResult<IQueryable<PostDTO>>(null, serviceResult.Code, serviceResult.Message);
            }
            var users = _userRepository.GetAll();
            var allComments = _commentService.GetAll(userID);

            var result = serviceResult.Result.Where(post => post.UserID == userID); // LINQ w repozytorium !!!

            List<PostDTO> postDTOs = new List<PostDTO>();
            foreach (var post in result.ToList())
            {
                var postLikes = GetLikes(post.PostID);

                var user = users.Result?.Where(x => x.UserID == post.UserID).FirstOrDefault();
                var comments = allComments.Result?.Where(x => x.postId == post.PostID);

                var postDTO = PostMapper.Map(post);

                postDTO.author = user?.UserName ?? "Nie ma takiego użytkownika";
                postDTO.authorID = user?.UserID ?? 0;

                postDTO.likesCount = postLikes.Result?.Count() ?? 0;
                postDTO.isLikedByUser = postLikes.Result?.Any(x => x.id == userID) ?? false;

                postDTO.comments = comments;

                postDTOs.Add(postDTO);
            }

            return new ServiceResult<IQueryable<PostDTO>>(postDTOs.AsQueryable(), serviceResult.Code, serviceResult.Message);
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

        public ServiceResult<IQueryable<LikerDTO>> GetLikes(int postID)
        {
            var result = _postRepository.GetLikes(postID);
            return new ServiceResult<IQueryable<LikerDTO>>(Mapper.Map(result.Result.Select(x => x.UserID)), result.Code, result.Message);
        }

        public async Task<ServiceResult<bool>> EditLikeStatusAsync(int userID, int postID, LikeDTO like)
        {
            var result = await _postRepository.UpdateLikeStatusAsync(userID, postID, like.like);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);

        }

        public ServiceResult<IQueryable<CommentDTOOutput>> GetAllComments(int postID, int userID)
        {
            var result = _commentService.GetAll(userID);
            result.Result = result.Result.Where(x => x.postId == postID);
            return new ServiceResult<IQueryable<CommentDTOOutput>>(result.Result, result.Code, result.Message);
        }

    }
}
