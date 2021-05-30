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
        private readonly IUserRepository _userRepository;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;



        public PostService(IPostRepository postRepository, IUserRepository userRepository, ICommentService commentService, ICategoryService categoryService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentService = commentService;
            _categoryService = categoryService;
        }

        public ServiceResult<IQueryable<PostGetDTO>> GetAll(int userID)
        {
            var result = _postRepository.GetAll();
            if (result.Result == null)
            {
                return new ServiceResult<IQueryable<PostGetDTO>>(null, result.Code, result.Message);
            }
            var users = _userRepository.GetAll();

            List<PostGetDTO> postDTOs = new List<PostGetDTO>();
            foreach (var post in result.Result.ToList())
            {
                var postLikes = GetLikes(post.PostID);
                var user = users.Result?.Where(x => x.UserID == post.UserID).FirstOrDefault();

                var postDTO = PostMapper.Map(post);

                postDTO.authorName = user?.UserName ?? "Nie ma takiego użytkownika";
                postDTO.authorID = user?.UserID ?? 0;
                postDTO.category = _categoryService.GetById(post.CategoryID).Result?.Name ?? "Category with given Id does not exist.";

                postDTO.likesCount = postLikes.Result?.Count() ?? 0;
                postDTO.isLikedByUser = postLikes.Result?.Any(x => x.id == userID) ?? false;

                postDTOs.Add(postDTO);
            }

            return new ServiceResult<IQueryable<PostGetDTO>>(postDTOs.AsQueryable(), result.Code, result.Message);
        }

        public ServiceResult<PostGetDTO> GetById(int postID, int userID)
        {
            var result = _postRepository.GetById(postID);
            if (result.Result == null)
            {
                return new ServiceResult<PostGetDTO>(null, result.Code, result.Message);
            }

            var postLikes = GetLikes(postID);
            var user = _userRepository.GetById(result.Result.UserID);

            var postDTO = PostMapper.Map(result.Result);

            postDTO.authorName = user.Result?.UserName ?? "Nie ma takiego użytkownika";
            postDTO.authorID = user.Result?.UserID ?? 0;
            postDTO.category = _categoryService.GetById(result.Result.CategoryID).Result?.Name ?? "Category with given Id does not exist.";

            postDTO.likesCount = postLikes.Result?.Count() ?? 0;
            postDTO.isLikedByUser = postLikes.Result?.Any(x => x.id == userID) ?? false;

            return new ServiceResult<PostGetDTO>(postDTO, result.Code, result.Message);
        }

        public async Task<ServiceResult<idDTO>> AddPostAsync(int userID, PostPostDTO newPostDTO)
        {
            Post createdPost = PostEditMapper.Map(newPostDTO);
            createdPost.UserID = userID;
            createdPost.Date = DateTime.Now;
            createdPost.IsPromoted = false;
            var result = await _postRepository.AddAsync(createdPost);
            return new ServiceResult<idDTO>(new idDTO { id = result.Result?.PostID }, result.Code, result.Message);
        }

        public ServiceResult<IQueryable<PostGetDTO>> GetAllOfUser(int userID)
        {
            var serviceResult = _postRepository.GetAll();
            if (serviceResult.Result == null)
            {
                return new ServiceResult<IQueryable<PostGetDTO>>(null, serviceResult.Code, serviceResult.Message);
            }
            var users = _userRepository.GetAll();

            var result = serviceResult.Result.Where(post => post.UserID == userID); // LINQ w repozytorium !!!

            List<PostGetDTO> postDTOs = new List<PostGetDTO>();
            foreach (var post in result.ToList())
            {
                var postLikes = GetLikes(post.PostID);

                var user = users.Result?.Where(x => x.UserID == post.UserID).FirstOrDefault();

                var postDTO = PostMapper.Map(post);

                postDTO.authorName = user?.UserName ?? "Nie ma takiego użytkownika";
                postDTO.authorID = user?.UserID ?? 0;

                postDTO.category = _categoryService.GetById(post.CategoryID).Result?.Name ?? "Category with given Id does not exist.";

                postDTO.likesCount = postLikes.Result?.Count() ?? 0;
                postDTO.isLikedByUser = postLikes.Result?.Any(x => x.id == userID) ?? false;

                postDTOs.Add(postDTO);
            }

            return new ServiceResult<IQueryable<PostGetDTO>>(postDTOs.AsQueryable(), serviceResult.Code, serviceResult.Message);
        }

        public async Task<ServiceResult<bool>> DeletePostAsync(int id)
        {
            var GetResult = _postRepository.GetById(id);

            if (!GetResult.IsOk())
                return new ServiceResult<bool>(false, GetResult.Code, GetResult.Message);

            var RemoveResult = await _postRepository.RemoveAsync(GetResult.Result);
            return new ServiceResult<bool>(RemoveResult.IsOk(), RemoveResult.Code, RemoveResult.Message);
        }

        public async Task<ServiceResult<bool>> EditPostAsync(int id, PostPutDTO body)
        {
            Post post = PostEditMapper.Map(body);
            post.PostID = id;
            post.Date = DateTime.Now;
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