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

    public class CommentService : ICommentService

    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<idDTO>> AddCommentAsync(int userId, CommentDTONew comment)
        {
            Comment newComment = Mapper.Map(comment);
            newComment.UserID = userId;
            newComment.DateTime = DateTime.Now;
            ServiceResult<Comment> result = await _commentRepository.AddAsync(newComment);
            return new ServiceResult<idDTO>(Mapper.Map(result.Result?.CommentID), result.Code, result.Message);
        }

        public ServiceResult<bool> DeleteComment(int commentId, int userId)
        {
            var result = _commentRepository.Delete(commentId, userId);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);
        }

        public async Task<ServiceResult<bool>> EditCommentAsync(int commentId, int userId, CommentDTOEdit comment)
        {
            Comment currentComment = _commentRepository.GetById(commentId).Result;
            currentComment.Content = comment.Content;
            var result = await _commentRepository.UpdateAsync(currentComment);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);
        }

        public async Task<ServiceResult<bool>> EditLikeOnCommentAsync(int commentId, int userID, LikeDTO like)
        {
            var result = await _commentRepository.UpdateLikeStatusAsync(userID, commentId, like.like);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);
        }

        public ServiceResult<IQueryable<CommentDTOOutput>> GetAll(int userId)
        {
            var result = _commentRepository.GetAll();
            if (result.Result == null)
            {
                return new ServiceResult<IQueryable<CommentDTOOutput>>(null, result.Code, result.Message);
            }


            List<CommentDTOOutput> outputDTOlist = new List<CommentDTOOutput>();
            foreach (var comment in result.Result.ToList())
            {
                var author = _userRepository.GetById(comment.UserID);

                var commentLikes = GetLikedUsers(comment.CommentID);


                var outputDTO = Mapper.MapOutput(comment);

                outputDTO.authorName = author.Result?.UserName ?? "";
                outputDTO.ownerMode = userId == (author.Result?.UserID ?? -1);

                outputDTO.likesCount = commentLikes.Result?.Count() ?? 0;
                outputDTO.isLikedByUser = commentLikes.Result?.Any(x => x.id == userId) ?? false;

                outputDTOlist.Add(outputDTO);
            }

            return new ServiceResult<IQueryable<CommentDTOOutput>>(outputDTOlist.AsQueryable(), result.Code, result.Message);
        }

        public ServiceResult<CommentDTOOutput> GetById(int commentId, int userId)
        {
            var result = _commentRepository.GetById(commentId);
            if (result.Result == null)
            {
                return new ServiceResult<CommentDTOOutput>(null, result.Code, result.Message);
            }

            var author = _userRepository.GetById(result.Result.UserID);

            var commentLikes = GetLikedUsers(result.Result.CommentID);


            var outputDTO = Mapper.MapOutput(result.Result);

            outputDTO.authorName = author.Result?.UserName ?? "";
            outputDTO.ownerMode = userId == (author.Result?.UserID ?? -1);

            outputDTO.likesCount = commentLikes.Result?.Count() ?? 0;
            outputDTO.isLikedByUser = commentLikes.Result?.Any(x => x.id == userId) ?? false;

            return new ServiceResult<CommentDTOOutput>(outputDTO, result.Code, result.Message);
        }

        public ServiceResult<IQueryable<LikerDTO>> GetLikedUsers(int commentId)
        {
            var result = _commentRepository.GetLikes(commentId);

            return new ServiceResult<IQueryable<LikerDTO>>(Mapper.Map(result.Result.Select(x => x.UserID)), result.Code, result.Message);
        }

        public ServiceResult<IQueryable<CommentDTOOutput>> GetAllOfUser(int userID)
        {
            var serviceResult = _commentRepository.GetAll();
            var userInfo = _userRepository.GetById(userID);
            if (serviceResult.Result == null || !userInfo.IsOk())
            {
                return new ServiceResult<IQueryable<CommentDTOOutput>>(null, serviceResult.Code, serviceResult.Message);
            }

            var result = serviceResult.Result.Where(comment => comment.UserID == userID); // LINQ w repozytorium !!!

            List<CommentDTOOutput> commentsDTOs = new List<CommentDTOOutput>();
            foreach (Comment comment in result.ToList())
            {
                CommentDTOOutput commentDTO = Mapper.MapOutput(comment);

                commentDTO.authorID = userID;
                commentDTO.authorName = userInfo.Result.UserName;

                commentDTO.ownerMode = true;

                var likes = this.GetLikedUsers(commentDTO.id.Value);

                commentDTO.likesCount = likes.Result?.Count() ?? 0;
                commentDTO.isLikedByUser = likes.Result?.Any(x => x.id == userID) ?? false;

                commentsDTOs.Add(commentDTO);
            }

            return new ServiceResult<IQueryable<CommentDTOOutput>>(commentsDTOs.AsQueryable<CommentDTOOutput>());
        }
    }
}