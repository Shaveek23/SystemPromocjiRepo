﻿using System;
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

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<ServiceResult<int?>> AddCommentAsync(int userId, CommentDTO comment)
        {
            Comment newComment = Mapper.Map(comment);
            ServiceResult<Comment> result = await _commentRepository.AddAsync(newComment);
            return new ServiceResult<int?>(result.Result?.CommentID, result.Code, result.Message);
        }

        public ServiceResult<bool> DeleteComment(int commentId, int userId)
        {
            var result = _commentRepository.Delete(commentId, userId);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);
        }

        public async Task<ServiceResult<bool>> EditCommentAsync(int commentId, int userId, CommentDTO comment)
        {
            Comment newComment = Mapper.Map(comment);
            newComment.CommentID = commentId;
            var result = await _commentRepository.UpdateAsync(newComment);
            return new ServiceResult<bool>(result.IsOk(), result.Code, result.Message);
        }

        public Task<ServiceResult<bool>> EditLikeOnCommentAsync(int commentId, int userId)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<IQueryable<CommentDTOOutput>> GetAll(int userId)
        {
            var result = _commentRepository.GetAll();
            return new ServiceResult<IQueryable<CommentDTOOutput>>(Mapper.MapOutput(result.Result), result.Code, result.Message);
        }

        public ServiceResult<CommentDTOOutput> GetById(int commentId, int userId)
        {

            var result = _commentRepository.GetById(commentId);
            return new ServiceResult<CommentDTOOutput>(Mapper.MapOutput(result.Result), result.Code, result.Message);

        }

        public ServiceResult<IQueryable<int>> GetLikedUsers(int commentId)
        {
            throw new NotImplementedException();
        }
    }
}